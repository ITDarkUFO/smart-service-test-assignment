using Application.Interfaces;
using Application.Models.Contexts;
using Application.Models.DTOs;
using Application.Services;
using Application.Services.Admin;
using Application.Services.Work;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController(
        IUserRolesRepository userRolesRepository,
        IRolesPermissionExtRepository rolesPermissionExtRepository,
        IUserDistrictRepository userDistrictRepository,
        ITasksOnlineAssignedRepository tasksOnlineAssignedRepository,
        ITenantMemberRepository tenantMemberRepository,
        ITaskListCategoryRepository taskListCategoryRepository,
        IWorkTypeRepository workTypeRepository,
        IUserWorkTypeRepository userWorkTypeRepository
        ) : Controller
    {
        private readonly IUserRolesRepository _userRolesRepository
            = userRolesRepository;

        private readonly IRolesPermissionExtRepository _rolesPermissionExtRepository
            = rolesPermissionExtRepository;

        private readonly IUserDistrictRepository _userDistrictRepository = userDistrictRepository;

        private readonly ITasksOnlineAssignedRepository _tasksOnlineAssignedRepository 
            = tasksOnlineAssignedRepository;

        private readonly ITenantMemberRepository _tenantMemberRepository = tenantMemberRepository;

        private readonly ITaskListCategoryRepository _taskListCategoryRepository = taskListCategoryRepository;

        private readonly IWorkTypeRepository _workTypeRepository = workTypeRepository;

        private readonly IUserWorkTypeRepository _userWorkTypeRepository = userWorkTypeRepository;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("UserListCategoryGet")]
        public async Task<IActionResult> UserListCategoryGet
            ([FromForm] short? tenantId, [FromForm] List<ListCategoryDTO> listCategories, [FromForm] List<UserDTO> users)
        {
            if (!tenantId.HasValue)
                return BadRequest("tenantId не может быть пустым");

            try
            {
                var userCategories = await new UserListCategoryService
                    (_userRolesRepository, _rolesPermissionExtRepository)
                    .UserListCategoryGet(tenantId.Value, listCategories, users);

                return Ok(userCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AdminTaskUserCacheAggregate")]
        public async Task<IActionResult> AdminTaskUserCacheAggregate
            ([FromForm] short? tenantId, [FromForm] List<UserTaskListCategoryDTO> userTaskListCategories, [FromForm] List<TaskResponsibleUserDTO> taskResponsibleUsers)
        {
            if (!tenantId.HasValue)
                return BadRequest("ID не может быть пустым");

            try
            {
                var taskUserCaches = await new AdminTaskUserCacheAggregateService(_userDistrictRepository)
                    .TaskUserCacheAggregate
                    (tenantId.Value, userTaskListCategories, taskResponsibleUsers);

                return Ok(taskUserCaches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("TaskUserCacheAggregateResponsibility")]
        public async Task<IActionResult> TaskUserCacheAggregateResponsibility
            ([FromForm] short? tenantId, [FromForm] List<UserTaskListCategoryDTO> userTaskListCategories,
            [FromForm] List<UserDTO> users, [FromForm] List<TaskDTO> tasks)
        {
            if (!tenantId.HasValue)
                return BadRequest("ID не может быть пустым");

            try
            {
                var taskResponsibleUsers = await new TaskUserCacheAggregateResponsibilityService(_tasksOnlineAssignedRepository)
                    .TaskUserCacheAggregateResponsibility
                    (tenantId.Value, userTaskListCategories, users, tasks);

                return Ok(taskResponsibleUsers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("WorkTaskUserCacheAggregate")]
        public async Task<IActionResult> WorkTaskUserCacheAggregate
            ([FromForm] short? tenantId, [FromForm] List<TaskDTO> tasks, [FromForm] List<UserDTO> users, [FromForm] List<TaskUserCacheDTO> taskUserCaches)
        {
            if (!tenantId.HasValue)
                return BadRequest("ID не может быть пустым");

            try
            {
                var newTaskUserCaches = await new WorkTaskUserCacheAggregateService
                    (_userRolesRepository, _rolesPermissionExtRepository, _tasksOnlineAssignedRepository, _userDistrictRepository, _tenantMemberRepository, _taskListCategoryRepository, _workTypeRepository, _userWorkTypeRepository)
                    .TaskUserCacheAggregate(tenantId.Value, tasks, users, taskUserCaches);

                return Ok(newTaskUserCaches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
