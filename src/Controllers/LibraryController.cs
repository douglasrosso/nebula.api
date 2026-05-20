using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using nebula.api.src.DTOs;
using nebula.api.src.Services;

namespace nebula.api.src.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _service;

        public LibraryController(ILibraryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<LibraryItemDto>>> Get()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            return Ok(await _service.GetLibrary(userId.Value));
        }

        [HttpGet("{gameId:guid}/owned")]
        public async Task<ActionResult<bool>> IsOwned(Guid gameId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            return Ok(await _service.IsInLibrary(userId.Value, gameId));
        }

        private Guid? GetUserId()
        {
            var value = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(value, out var id) ? id : null;
        }
    }
}
