using Microsoft.AspNetCore.Mvc;
using nebula.api.src.DTOs;
using nebula.api.src.Services;

namespace nebula.api.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly IUserService _userService;

        public AuthController(TokenService tokenService, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userService.Authenticate(dto);

            if (user is null)
                return Unauthorized(new { message = "Credenciais inválidas" });

            _tokenService.GenerateToken(user.Id.ToString(), user.Email);

            return Ok(new { message = "Login realizado com sucesso" });
        }
    }
}
