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
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CartItemDto>>> Get()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            return Ok(await _service.GetCart(userId.Value));
        }

        [HttpPost("{gameId:guid}")]
        public async Task<ActionResult<CartItemDto>> Add(Guid gameId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            try
            {
                var item = await _service.AddToCart(userId.Value, gameId);
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

            var removed = await _service.RemoveFromCart(userId.Value, gameId);
            if (!removed) return NotFound(new { message = "Item não encontrado no carrinho." });
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Clear()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            await _service.ClearCart(userId.Value);
            return NoContent();
        }

        private Guid? GetUserId()
        {
            var value = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(value, out var id) ? id : null;
        }
    }
}
