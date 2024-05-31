using Application.Interfaces;
using Application.Models.DTOs;
using Application.Services.Admin;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Work
{
    public class WorkTaskUserCacheAggregateService(
        IUserRolesRepository userRolesRepository,
        IRolesPermissionExtRepository rolesPermissionExtRepository,
        ITasksOnlineAssignedRepository tasksOnlineAssignedRepository,
        IUserDistrictRepository userDistrictRepository,
        ITenantMemberRepository tenantMemberRepository,
        ITaskListCategoryRepository taskListCategoryRepository,
        IWorkTypeRepository workTypeRepository,
        IUserWorkTypeRepository userWorkTypeRepository
        )
    {
        private readonly IUserRolesRepository _userRolesRepository
            = userRolesRepository;

        private readonly IRolesPermissionExtRepository _rolesPermissionExtRepository
            = rolesPermissionExtRepository;

        private readonly ITasksOnlineAssignedRepository _tasksOnlineAssignedRepository
            = tasksOnlineAssignedRepository;

        private readonly IUserDistrictRepository _userDistrictRepository
            = userDistrictRepository;

        private readonly ITenantMemberRepository _tenantMemberRepository
            = tenantMemberRepository;

        private readonly ITaskListCategoryRepository _taskListCategoryRepository
            = taskListCategoryRepository;

        private readonly IWorkTypeRepository _workTypeRepository = workTypeRepository;

        private readonly IUserWorkTypeRepository _userWorkTypeRepository = userWorkTypeRepository;

        //INFO: _isOwnerOnly не используется 
        //private readonly bool _isOwnerOnly = false;
        //INFO: _assignedTo не используется
        //private readonly byte _assignedTo = 2;

        private readonly byte _districtAvailable = 13;
        private readonly byte _userWorkType = 16;
        private readonly byte _allTaskAvailable = 19;
        private readonly byte _createdBy = 20;

        public async Task<List<TaskUserCacheDTO>> TaskUserCacheAggregate
            (short tenantID, List<TaskDTO> tasks, List<UserDTO> users,
            List<TaskUserCacheDTO> taskUserCaches)
        {
            //INFO: DateTime now = DateTime.UtcNow не используется 
            //DateTime now = DateTime.UtcNow;

            List<ListCategoryDTO> categories = [];
            List<EmploymentDTO> employments = [];
            List<TaskResponsibleUserDTO> taskResponsibleUsers = [];
            List<UserTaskListCategoryDTO> userTaskListCategories = [];

            var taskListCategories = await _taskListCategoryRepository
                .GetTaskListCategoriesWhereIdInList([_districtAvailable, _userWorkType, _allTaskAvailable]);

            categories = taskListCategories
                .Select(tlc => new ListCategoryDTO
                {
                    ID = tlc.ID,
                    Permissionextid = tlc.Permissionextid
                })
                .ToList();

            userTaskListCategories = await new UserListCategoryService
                (_userRolesRepository, _rolesPermissionExtRepository)
                .UserListCategoryGet(tenantID, categories, users);

            if (userTaskListCategories.Any(tlc => tlc.TaskListCategoryID == _allTaskAvailable))
            {
                var newTaskUserCaches1 = userTaskListCategories
                    .SelectMany(tlc => tasks
                        .Select(t => new
                        {
                            t.ID,
                            tlc.UserID,
                            tlc.TaskListCategoryID
                        }))
                    .Where(tlc => tlc.TaskListCategoryID == _allTaskAvailable)
                    .Select(tlc => new TaskUserCacheDTO
                    {
                        TaskID = tlc.ID,
                        UserID = tlc.UserID,
                        TaskListCategoryID = tlc.TaskListCategoryID
                    });

                taskUserCaches.AddRange(newTaskUserCaches1);

                // TODO Проверить работает ли правильно
                users.RemoveAll(u => taskUserCaches
                        .Where(tuc => tuc.TaskListCategoryID == _allTaskAvailable)
                        .Select(tuc => tuc.UserID)
                        .Contains(u.ID));
            }

            if (users.Count != 0)
                return [];

            var tenantMembers = await _tenantMemberRepository.GetAllTenantMembers();

            var newTaskUserCaches2 = tasks
                .SelectMany(t => tenantMembers
                    .Where(tm => tm.ID == t.CreatedBy)
                    .Select(tm => new { tm.TenantID, tm.UserID })
                    .Where(g => g.TenantID == tenantID &&
                        users.Any(u => u.ID == g.UserID))
                    .Select(g => new TaskUserCacheDTO
                    {
                        TaskID = t.ID,
                        UserID = g.UserID,
                        TaskListCategoryID = _createdBy
                    }));

            taskUserCaches.AddRange(newTaskUserCaches2);

            if (userTaskListCategories.Any(tlc => tlc.TaskListCategoryID == _userWorkType))
            {
                var userWorkTypes = await _userWorkTypeRepository.GetAllUserWorkTypes();
                var workTypes = await _workTypeRepository.GetAllWorkTypes();

                var newTaskUserCaches3 = userTaskListCategories
                    .SelectMany(tlc => tasks
                        .Select(t => new { tlc, t })
                        .Where(g1 => g1.tlc.TaskListCategoryID == _userWorkType &&
                            userWorkTypes.SelectMany(uwt => workTypes
                                .Where(wt => wt.TenantID == uwt.TenantID && wt.ID == uwt.WorkTypeID)
                                .Select(wt => new { uwt, wt })
                                .Where(g2 => g2.uwt.TenantID == tenantID &&
                                    g2.uwt.UserID == tlc.UserID &&
                                    g2.uwt.WorkTypeID == g1.t.WorkTypeID &&
                                    g2.uwt.Deleted != null &&
                                    g2.wt.Deleted != null)
                                .Select(g1 => new { g1.uwt, g1.wt }))
                            .Any()
                        ))
                    .Select(g1 => new TaskUserCacheDTO
                    {
                        TaskID = g1.t.ID,
                        UserID = g1.tlc.UserID,
                        TaskListCategoryID = g1.tlc.TaskListCategoryID
                    });

                taskUserCaches.AddRange(newTaskUserCaches3);
            }

            taskResponsibleUsers = await new TaskUserCacheAggregateResponsibilityService(_tasksOnlineAssignedRepository)
                .TaskUserCacheAggregateResponsibility(tenantID, userTaskListCategories, users, tasks);

            taskUserCaches = await new AdminTaskUserCacheAggregateService(_userDistrictRepository)
                .TaskUserCacheAggregate(tenantID, userTaskListCategories, taskResponsibleUsers);

            return taskUserCaches;
        }
    }
}
