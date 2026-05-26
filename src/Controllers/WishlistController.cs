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
    public class WishlistController : AuthorizedController
    {
        private readonly IWishlistService _service;

        public WishlistController(IWishlistService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<WishlistItemDto>>> Get()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            return Ok(await _service.GetWishlist(userId.Value));
        }

        [HttpPost("{gameId:guid}")]
        public async Task<ActionResult<WishlistItemDto>> Add(Guid gameId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            try
            {
                var item = await _service.AddToWishlist(userId.Value, gameId);
                return Ok(item);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("{gameId:guid}")]
        public async Task<ActionResult> Remove(Guid gameId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            var removed = await _service.RemoveFromWishlist(userId.Value, gameId);
            if (!removed) return NotFound(new { message = "Item não encontrado na lista de desejos." });
            return NoContent();
        }

    }
}
