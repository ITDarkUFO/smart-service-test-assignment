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

            var taskCatJoinTaskUser = await _context.UserTasksListCategories
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

            var userDistrict2 = await _context.UserDistricts
                .Where(ud => ud.Deleted == null).ToListAsync();

            var taskUserCacheRaw = taskCatJoinTaskUser
                .Where(tctu => tctu.TaskListCategoryID == _districtAvailable &&
                    userDistrict1.Where(
                        ud => tctu.UserID == ud.UserID &&
                        userDistrict2.Where(
                            ud1 => ud1.TenantID == ud.TenantID &&
                            ud1.DistrictID == ud.DistrictID &&
                            ud1.UserID == tctu.UserID)
                        .Any())
                    .Any());

            var taskUserCache = taskUserCacheRaw
                .Select(tuc => new TaskUserCache()
                {
                    TaskID = tuc.TaskID,
                    UserID = tuc.UserID,
                    TaskListCategoryID = tuc.TaskListCategoryID
                }).ToList();

            return new OkObjectResult(taskUserCache);
        }
    }
}
