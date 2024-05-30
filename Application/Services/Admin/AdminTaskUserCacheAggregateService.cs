using Application.Models;
using Application.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Admin
{
    public class AdminTaskUserCacheAggregateService(ApplicationDbContext context)
    {
        private readonly byte _districtAvailable = 13;

        private readonly ApplicationDbContext _context = context;

        public async Task<List<TaskUserCacheDTO>> TaskUserCacheAggregate
            (short tenantID,
            List<UserTaskListCategoryDTO> userTaskListCategories,
            List<TaskResponsibleUserDTO> taskResponsibleUsers)
        {
            if (!userTaskListCategories
                .Any(ut => ut.TaskListCategoryID == _districtAvailable))
                return [];

            var taskCatJoinTaskUser = userTaskListCategories
                .SelectMany(tlc => taskResponsibleUsers
                    .Select(tu => new
                    {
                        tu.TaskID,
                        tlc.UserID,
                        tlc.TaskListCategoryID
                    })).ToList();

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
                .Select(tuc => new TaskUserCacheDTO()
                {
                    TaskID = tuc.TaskID,
                    UserID = tuc.UserID,
                    TaskListCategoryID = tuc.TaskListCategoryID
                }).ToList();

            return taskUserCache;
        }
    }
}
