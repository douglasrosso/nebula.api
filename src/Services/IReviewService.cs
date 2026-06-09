using nebula.api.src.DTOs;

namespace nebula.api.src.Services
{
    public interface IReviewService
    {
        Task<List<ReviewDto>> GetByGameId(Guid gameId);
    }
}
