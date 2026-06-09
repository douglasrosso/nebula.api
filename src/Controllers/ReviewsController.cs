using Microsoft.AspNetCore.Mvc;
using nebula.api.src.DTOs;
using nebula.api.src.Services;

namespace nebula.api.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewsController(IReviewService service)
        {
            _service = service;
        }

        [HttpGet("game/{gameId:guid}")]
        public async Task<ActionResult<List<ReviewDto>>> GetByGame(Guid gameId)
        {
            return Ok(await _service.GetByGameId(gameId));
        }
    }
}
