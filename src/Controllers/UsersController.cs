using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using nebula.api.src.Common.Controllers;
using nebula.api.src.DTOs;
using nebula.api.src.Services;

namespace nebula.api.src.Controllers
{
    public class UsersController
        : BaseController<IUserService, UserDto, CreateUserDto, UpdateUserDto, UserQueryDto>
    {
        public UsersController(IUserService service) : base(service) { }

        [AllowAnonymous]
        [HttpPost]
        public override async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto dto)
        {
            try
            {
                return await base.Create(dto);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> Me()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null || !Guid.TryParse(userId, out var id))
                return Unauthorized();

            var user = await _service.GetById(id);

            if (user is null)
                return NotFound();

            return Ok(user);
        }
    }
}
