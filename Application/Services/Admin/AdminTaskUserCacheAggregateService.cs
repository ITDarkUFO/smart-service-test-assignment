using Application.Interfaces;
using Application.Models.DTOs;

namespace Application.Services.Admin
{
    public class AdminTaskUserCacheAggregateService
        (IUserDistrictRepository userDistrictRepository)
    {
        private readonly byte _districtAvailable = 13;

        private readonly IUserDistrictRepository _userDistrictRepository = userDistrictRepository;

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

            var userDistrict1 = await _userDistrictRepository.GetUserDistrictsWithTenant(tenantID);

            var userDistrict2 = await _userDistrictRepository.GetAllUserDistricts();


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
