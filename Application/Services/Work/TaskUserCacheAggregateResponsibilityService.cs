using Application.Models;
using Application.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Work
{
    public class TaskUserCacheAggregateResponsibilityService(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;
        private readonly byte _assignedTo = 2;
        private readonly byte _districtAvailable = 13;

        public async Task<List<TaskResponsibleUserDTO>> TaskUserCacheAggregateResponsibility
            (short tenantID, List<UserTaskListCategoryDTO> userTaskListCategories, List<UserDTO> users, List<TaskDTO> tasks)
        {
            List<TaskAssignedDTO> taskAssigned = await _context.TasksOnlineAssigned
                .Where(
                    toa => toa.TenantID == tenantID &&
                    toa.AssignedTo != null &&
                    tasks.Where(t => t.ID == toa.TaskID).Any())
                .Select(toa => new TaskAssignedDTO
                {
                    TaskID = toa.TaskID,
                    AssignedTo = toa.AssignedTo
                })
                .ToListAsync();

            List<TaskUserCacheDTO> taskUserCaches = taskAssigned
                .Where(ta => users
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
                var tasksApproved = tasks
                    .Where(t => t.ApprovalWith != null)
                    .Select(t => new
                    {
                        TaskID = t.ID,
                        UserID = t.ApprovalWith
                    })
                    .ToList();

                var tasksEscalated = tasks
                    .Where(t => t.EscalatedTo != null)
                    .Select(t => new
                    {
                        TaskID = t.ID,
                        UserID = t.EscalatedTo
                    })
                    .ToList();

                var collectedTasks = tasksApproved
                    .Union(tasksEscalated)
                    .Union(taskAssigned.Select(ta => new
                    {
                        ta.TaskID,
                        UserID = ta.AssignedTo
                    }));

                List<TaskResponsibleUserDTO> taskResponsibleUsers = collectedTasks
                    .GroupBy(ct => ct.TaskID)
                    .Select(g => new TaskResponsibleUserDTO
                    {
                        TaskID = g.First().TaskID,
                        UserID = (int)g.First().UserID!
                    }).ToList();

                return taskResponsibleUsers;
            }

            return [];
        }
    }
}
