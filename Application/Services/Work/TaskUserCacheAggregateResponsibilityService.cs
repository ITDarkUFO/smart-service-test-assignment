using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Work
{
    public class TaskUserCacheAggregateResponsibilityService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;
        private readonly byte _assignedTo = 2;
        private readonly byte _districtAvailable = 13;

        //INFO: Неясно откуда поступают Tasks и Users, тк таблица #Task и #User временная
        public async Task<List<TaskResponsibleUserDTO>> TaskUserCacheAggregateResponsibility
            (short tenantID, List<UserTaskListCategoryDTO> userTaskListCategories)
        {
            List<TaskAssignedDTO> taskAssigned = await _context.TaskOnlineAssigneds
                .Where(
                    toa => toa.TenantID == tenantID &&
                    toa.AssignedTo != null &&
                    _context.Tasks.Where(t => t.ID == toa.TaskID).Any())
                .Select(toa => new TaskAssignedDTO
                {
                    TaskID = toa.TaskID,
                    AssignedTo = toa.AssignedTo
                })
                .ToListAsync();

            List<TaskUserCacheDTO> taskUserCaches = taskAssigned
                .Where(ta => _context.Users
                    .Where(u => u.ID == ta.AssignedTo)
                    .Any())
                .Select(ta => new TaskUserCacheDTO
                {
                    TaskID = ta.TaskID,
                    UserID = (int)ta.AssignedTo!,
                    TaskListCategoryID = _assignedTo
                })
                .ToList();

            if (userTaskListCategories
                .Any(utlc => utlc.TaskListCategoryID == _districtAvailable))
            {
                var tasksApproved = await _context.Tasks
                    .Where(t => t.ApprovalWith != null)
                    .Select(t => new
                    {
                        TaskID = t.ID,
                        UserID = t.ApprovalWith
                    })
                    .ToListAsync();

                var tasksEscalated = await _context.Tasks
                    .Where(t => t.EscalatedTo != null)
                    .Select(t => new
                    {
                        TaskID = t.ID,
                        UserID = t.EscalatedTo
                    })
                    .ToListAsync();

                var collectedTasks = tasksApproved
                    .Union(tasksEscalated)
                    .Union(taskAssigned.Select(ta => new
                    {
                        ta.TaskID,
                        UserID = ta.AssignedTo
                    }));


                List<TaskResponsibleUserDTO> taskResponsibleUsers = collectedTasks
                    .GroupBy(ct => ct.TaskID)
                    .Select(ctg => new TaskResponsibleUserDTO
                    {
                        TaskID = ctg.First().TaskID,
                        UserID = (int)ctg.First().UserID!
                    }).ToList();

                return taskResponsibleUsers;
            }

            return [];
        }
    }
}
