using nebula.api.src.Common.DTOs;
using nebula.api.src.Common.Services;
using nebula.api.src.DTOs;

namespace nebula.api.src.Services
{
    public interface IGameService : IBaseService<GameDto, CreateGameDto, UpdateGameDto, GameQueryDto>
    {
        Task<PaginatedResultDto<GameSummaryDto>> GetSummaries(GameQueryDto query);
        Task<GameDto?> GetByIdWithGenres(Guid id);
        Task<List<GenreDto>> GetAllGenres();
    }
}
