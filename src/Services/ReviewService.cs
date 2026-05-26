using AutoMapper;
using Microsoft.EntityFrameworkCore;
using nebula.api.src.Common.Services;
using nebula.api.src.Data;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;
using nebula.api.src.Repositories;

namespace nebula.api.src.Services
{
    public class ReviewService
        : BaseService<ReviewEntity, ReviewDto, CreateReviewDto, UpdateReviewDto, ReviewQueryDto>,
          IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly NebulaDbContext _context;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper, NebulaDbContext context)
            : base(reviewRepository, mapper)
        {
            _reviewRepository = reviewRepository;
            _context = context;
        }

        public async Task<List<ReviewDto>> GetByGameId(Guid gameId)
        {
            var reviews = await _reviewRepository.GetByGameId(gameId);
            return reviews.Select(MapReviewToDto).ToList();
        }

        public async Task<ReviewDto> CreateReview(Guid userId, CreateReviewDto dto)
        {
            if (await _reviewRepository.UserAlreadyReviewed(userId, dto.GameId))
                throw new InvalidOperationException("Você já avaliou este jogo.");

            var entity = new ReviewEntity
            {
                Id = Guid.NewGuid(),
                GameId = dto.GameId,
                UserId = userId,
                IsPositive = dto.Rating!.Equals("positive", StringComparison.OrdinalIgnoreCase),
                HoursPlayed = dto.HoursPlayed,
                Content = dto.Content!,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Reviews.Add(entity);
            await _context.SaveChangesAsync();
            await _reviewRepository.RecalculateGameStats(dto.GameId);

            var withUser = await _reviewRepository.GetByIdWithUser(entity.Id);
            return MapReviewToDto(withUser!);
        }

        protected override async Task BeforeUpdate(ReviewEntity entity, UpdateReviewDto dto)
        {
            entity.IsPositive = dto.Rating!.Equals("positive", StringComparison.OrdinalIgnoreCase);
            await _reviewRepository.RecalculateGameStats(entity.GameId);
        }

        public override async Task<bool> Delete(Guid id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review is null) return false;

            var gameId = review.GameId;
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            await _reviewRepository.RecalculateGameStats(gameId);
            return true;
        }

        public async Task<bool> MarkHelpful(Guid reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review is null) return false;

            review.HelpfulCount++;
            review.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkFunny(Guid reviewId)
        {
            var review = await _context.Reviews.FindAsync(reviewId);
            if (review is null) return false;

            review.FunnyCount++;
            review.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
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
