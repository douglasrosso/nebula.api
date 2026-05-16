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
    public class ReviewsController : AuthorizedController
    {
        private readonly IReviewService _service;

        public ReviewsController(IReviewService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet("game/{gameId:guid}")]
        public async Task<ActionResult<List<ReviewDto>>> GetByGame(Guid gameId)
        {
            return Ok(await _service.GetByGameId(gameId));
        }

        [HttpPost]
        public async Task<ActionResult<ReviewDto>> Create([FromBody] CreateReviewDto dto)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            try
            {
                var review = await _service.CreateReview(userId.Value, dto);
                return CreatedAtAction(nameof(GetByGame), new { gameId = dto.GameId }, review);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ReviewDto>> Update(Guid id, [FromBody] UpdateReviewDto dto)
        {
            var updated = await _service.Update(id, dto);
            if (updated is null) return NotFound(new { message = "Review não encontrada." });
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleted = await _service.Delete(id);
            if (!deleted) return NotFound(new { message = "Review não encontrada." });
            return NoContent();
        }

        [HttpPost("{id:guid}/helpful")]
        public async Task<ActionResult> MarkHelpful(Guid id)
        {
            var result = await _service.MarkHelpful(id);
            if (!result) return NotFound(new { message = "Review não encontrada." });
            return NoContent();
        }

        [HttpPost("{id:guid}/funny")]
        public async Task<ActionResult> MarkFunny(Guid id)
        {
            var result = await _service.MarkFunny(id);
            if (!result) return NotFound(new { message = "Review não encontrada." });
            return NoContent();
        }
    }
}
