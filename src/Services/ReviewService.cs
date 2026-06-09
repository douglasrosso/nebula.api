using nebula.api.src.DTOs;
using nebula.api.src.Entities;
using nebula.api.src.Repositories;

namespace nebula.api.src.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<List<ReviewDto>> GetByGameId(Guid gameId)
        {
            var reviews = await _reviewRepository.GetByGameId(gameId);
            return reviews.Select(MapReviewToDto).ToList();
        }

        private static ReviewDto MapReviewToDto(ReviewEntity r) => new()
        {
            Id = r.Id,
            GameId = r.GameId,
            UserId = r.UserId,
            UserName = r.User?.DisplayName ?? r.User?.Name ?? "Usuário",
            UserAvatar = r.User?.Avatar ?? string.Empty,
            Rating = r.IsPositive ? "positive" : "negative",
            HoursPlayed = r.HoursPlayed,
            Content = r.Content,
            Date = r.CreatedAt.ToString("yyyy-MM-dd"),
            Helpful = r.HelpfulCount,
            Funny = r.FunnyCount
        };
    }
}
