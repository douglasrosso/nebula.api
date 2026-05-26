using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nebula.api.src.Common.Controllers;
using nebula.api.src.DTOs;
using nebula.api.src.Services;

namespace nebula.api.src.Controllers
{
    public class GamesController
        : BaseController<IGameService, GameDto, CreateGameDto, UpdateGameDto, GameQueryDto>
    {
        public GamesController(IGameService service) : base(service) { }

        [AllowAnonymous]
        public override async Task<ActionResult<PaginatedResultDto<GameDto>>> Get([FromQuery] GameQueryDto query)
        {
            return await base.Get(query);
        }

        [AllowAnonymous]
        public override async Task<ActionResult<GameDto>> GetById(Guid id)
        {
            var game = await _service.GetByIdWithGenres(id);

            if (game is null)
                return NotFound(new { message = "Jogo não encontrado." });

            return Ok(game);
        }

        [AllowAnonymous]
        [HttpGet("genres")]
        public async Task<ActionResult<List<GenreDto>>> GetGenres()
        {
            var genres = await _service.GetAllGenres();
            return Ok(genres);
        }
    }
}
