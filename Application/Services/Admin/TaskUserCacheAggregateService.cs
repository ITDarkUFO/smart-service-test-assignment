using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Admin
{
    public class TaskUserCacheAggregateService(ApplicationDbContext context)
    {
        private readonly byte _districtAvailable = 13;

        private readonly ApplicationDbContext _context = context;

        public async Task<IActionResult> TaskUserCacheAggregate(short tenantID)
        {
            if (!_context.UserTasksListCategories
                .Any(ut => ut.TaskListCategoryID == _districtAvailable))
                return new NotFoundResult();

            var taskCatJointaskUser = await _context.UserTasksListCategories
                .SelectMany(tlc => _context.TaskResponsibleUsers
                    .Select(tu => new
                    {
                        tu.TaskID,
                        tlc.UserID,
                        tlc.TaskListCategoryID
                    })).ToListAsync();

            var userDistrict1 = await _context.UserDistricts
                .Where(ud => ud.TenantID == tenantID && ud.Deleted == null)
                .ToListAsync();

            taskCatJointaskUser
                .Where(tctu => tctu.TaskListCategoryID == _districtAvailable);

            return null;
        }
    }
}
