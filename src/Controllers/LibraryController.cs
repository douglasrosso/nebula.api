using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nebula.api.src.Common.Controllers;
using nebula.api.src.DTOs;
using nebula.api.src.Services;

namespace nebula.api.src.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : AuthorizedController
    {
        private readonly ILibraryService _libraryService;
        private readonly IWishlistService _wishlistService;

        public LibraryController(ILibraryService libraryService, IWishlistService wishlistService)
        {
            _libraryService = libraryService;
            _wishlistService = wishlistService;
        }

        [HttpGet]
        public async Task<ActionResult<List<LibraryItemDto>>> Get()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            return Ok(await _libraryService.GetLibrary(userId.Value));
        }

        [HttpGet("{gameId:guid}/owned")]
        public async Task<ActionResult<bool>> IsOwned(Guid gameId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            return Ok(await _libraryService.IsInLibrary(userId.Value, gameId));
        }

        [HttpPost("{gameId:guid}")]
        public async Task<IActionResult> Purchase(Guid gameId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            await _libraryService.AddToLibrary(userId.Value, gameId);

            try { await _wishlistService.AddToWishlist(userId.Value, gameId); }
            catch (InvalidOperationException) { }

            return NoContent();
        }
    }
}
