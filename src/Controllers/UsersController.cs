using Microsoft.AspNetCore.Mvc;
using nebula.api.src.DTOs;
using nebula.api.src.Services;

namespace nebula.api.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> Get()
        {
            var users = await _userService.Get();
            return Ok(users);
        }
    }
}