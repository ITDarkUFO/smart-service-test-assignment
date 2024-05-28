using Application.Models;
using Application.Services;
using Application.Services.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("UserListCategoryGet")]
        public async Task<IActionResult> UserListCategoryGet([FromQuery(Name = "id")] short? tenantId)
        {
            if (!tenantId.HasValue)
            {
                return BadRequest("ID не может быть пустым");
            }

            var userCategories = await new UserListCategoryService(_context).UserListCategoryGet(tenantId.Value);

            return View(userCategories);
        }

    }
}
