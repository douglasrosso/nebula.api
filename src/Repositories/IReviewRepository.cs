using nebula.api.src.Common.Repositories;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;

namespace nebula.api.src.Repositories
{
    public interface IReviewRepository : IBaseRepository<ReviewEntity, ReviewQueryDto>
    {
        Task<ReviewEntity?> GetByIdWithUser(Guid id);
        Task<List<ReviewEntity>> GetByGameId(Guid gameId);
        Task<bool> UserAlreadyReviewed(Guid userId, Guid gameId);
        Task RecalculateGameStats(Guid gameId);
    }
}
