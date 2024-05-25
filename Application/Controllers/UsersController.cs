using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        //private readonly UserService _userService;

        public UsersController(/*UserService userService*/)
        {
            //_userService = userService;
        }


    }
}
