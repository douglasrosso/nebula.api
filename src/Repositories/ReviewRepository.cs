using Microsoft.EntityFrameworkCore;
using nebula.api.src.Common.Repositories;
using nebula.api.src.Data;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;

namespace nebula.api.src.Repositories
{
    public class ReviewRepository : BaseRepository<ReviewEntity, ReviewQueryDto>, IReviewRepository
    {
        private readonly NebulaDbContext _nebulaContext;

        public ReviewRepository(NebulaDbContext context) : base(context)
        {
            _nebulaContext = context;
        }

        protected override IQueryable<ReviewEntity> ApplyFilters(IQueryable<ReviewEntity> query, ReviewQueryDto queryDto)
        {
            query = query.Include(r => r.User);

            if (queryDto.GameId.HasValue)
                query = query.Where(r => r.GameId == queryDto.GameId.Value);

            if (queryDto.UserId.HasValue)
                query = query.Where(r => r.UserId == queryDto.UserId.Value);

            return query.OrderByDescending(r => r.CreatedAt);
        }

        public async Task<ReviewEntity?> GetByIdWithUser(Guid id)
        {
            return await _nebulaContext.Reviews
                .AsNoTracking()
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<ReviewEntity>> GetByGameId(Guid gameId)
        {
            return await _nebulaContext.Reviews
                .AsNoTracking()
                .Include(r => r.User)
                .Where(r => r.GameId == gameId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> UserAlreadyReviewed(Guid userId, Guid gameId)
        {
            return await _nebulaContext.Reviews
                .AnyAsync(r => r.UserId == userId && r.GameId == gameId);
        }

        public async Task RecalculateGameStats(Guid gameId)
        {
            var game = await _nebulaContext.Games.FindAsync(gameId);
            if (game is null) return;

            var reviews = await _nebulaContext.Reviews
                .Where(r => r.GameId == gameId)
                .ToListAsync();

            game.ReviewCount = reviews.Count;
            game.PositivePercentage = reviews.Count == 0
                ? 0
                : (int)Math.Round(reviews.Count(r => r.IsPositive) * 100.0 / reviews.Count);
            game.UpdatedAt = DateTime.UtcNow;

            await _nebulaContext.SaveChangesAsync();
        }
    }
}
