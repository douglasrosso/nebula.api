using nebula.api.src.Common.Services;
using nebula.api.src.DTOs;

namespace nebula.api.src.Services
{
    public interface IReviewService : IBaseService<ReviewDto, CreateReviewDto, UpdateReviewDto, ReviewQueryDto>
    {
        Task<List<ReviewDto>> GetByGameId(Guid gameId);
        Task<ReviewDto> CreateReview(Guid userId, CreateReviewDto dto);
        Task<bool> MarkHelpful(Guid reviewId);
        Task<bool> MarkFunny(Guid reviewId);
    }
}
