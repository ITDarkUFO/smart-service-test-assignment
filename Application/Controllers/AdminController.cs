using Application.Models;
using Application.Services;
using Application.Services.Admin;
using Application.Services.Work;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController(ApplicationDbContext context) : Controller
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

        [HttpGet("UserListCategoryGet")]
        public async Task<IActionResult> UserListCategoryGet([FromQuery] short? tenantId)
        {
            if (!tenantId.HasValue)
                return BadRequest("ID не может быть пустым");

            //var userCategories = await new UserListCategoryService(_context)
            //    .UserListCategoryGet(tenantId.Value);

            return View();
        }

        [HttpGet("AdminTaskUserCacheAggregate")]
        public async Task<IActionResult> AdminTaskUserCacheAggregate([FromQuery] short? tenantId)
        {
            if (!tenantId.HasValue)
                return BadRequest("ID не может быть пустым");

            //var resultWithList = await new AdminTaskUserCacheAggregateService(_context)
            //    .AdminTaskUserCacheAggregate(tenantId.Value);

            return View();
        }

        [HttpGet("TaskUserCacheAggregateResponsibility")]
        public async Task<IActionResult> TaskUserCacheAggregateResponsibility([FromQuery] short? tenantId)
        {
            if (!tenantId.HasValue)
                return BadRequest("ID не может быть пустым");

            //var results = await new TaskUserCacheAggregateResponsibilityService(_context)
            //    .TaskUserCacheAggregateResponsibility(tenantId.Value);

            return View();
        }

        [HttpGet("WorkTaskUserCacheAggregate")]
        public async Task<IActionResult> WorkTaskUserCacheAggregate([FromQuery] short? tenantId)
        {
            if (!tenantId.HasValue)
                return BadRequest("ID не может быть пустым");

            //await new WorkTaskUserCacheAggregateService(_context)
            //    .TaskUserCacheAggregate(tenantId.Value);

            return View();
        }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

    }
}
