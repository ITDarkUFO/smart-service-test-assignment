using Application.Models;
using Application.Services;
using Application.Services.Admin;
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

        [HttpGet("UserListCategoryGet")]
        public async Task<IActionResult> UserListCategoryGet([FromQuery] short? tenantId)
        {
            if (!tenantId.HasValue)
            {
                return BadRequest("ID не может быть пустым");
            }

            var userCategories = await new UserListCategoryService(_context)
                .UserListCategoryGet(tenantId.Value);

            return View(userCategories);
        }

        [HttpGet("TaskUserCacheAggregate")]
        public async Task<IActionResult> TaskUserCacheAggregate([FromQuery] short? tenantId)
        {
            if (!tenantId.HasValue)
            {
                return BadRequest("ID не может быть пустым");
            }

            var resultWithList = await new TaskUserCacheAggregateService(_context)
                .TaskUserCacheAggregate(tenantId.Value);

            return View(resultWithList);
        }
    }
}
