using nebula.api.src.Common.Repositories;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;

namespace nebula.api.src.Repositories
{
    public interface IGameRepository : IBaseRepository<GameEntity, GameQueryDto>
    {
        Task<GameEntity?> GetByIdWithGenres(Guid id);
        Task<List<GenreEntity>> GetOrCreateGenres(string[] genreNames);
    }
}
