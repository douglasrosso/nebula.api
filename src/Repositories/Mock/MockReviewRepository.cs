using nebula.api.src.Common.Repositories;
using nebula.api.src.Data.Mock;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;

namespace nebula.api.src.Repositories.Mock
{
    public class MockReviewRepository : IReviewRepository
    {
        private static readonly List<ReviewEntity> _store = MockData.Reviews;

        public Task<PaginatedResultDto<ReviewEntity>> Get(ReviewQueryDto query)
        {
            var items = _store.AsEnumerable();

            if (query.GameId.HasValue)
                items = items.Where(r => r.GameId == query.GameId.Value);

            if (query.UserId.HasValue)
                items = items.Where(r => r.UserId == query.UserId.Value);

            if (!string.IsNullOrWhiteSpace(query.Search))
                items = items.Where(r => r.Content.Contains(query.Search, StringComparison.OrdinalIgnoreCase));

            items = query.SortDirection.ToLower() == "asc"
                ? items.OrderBy(r => r.CreatedAt)
                : items.OrderByDescending(r => r.CreatedAt);

            var list = items.ToList();
            var total = list.Count;

            return Task.FromResult(new PaginatedResultDto<ReviewEntity>
            {
                Items      = list.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize),
                Page       = query.Page,
                PageSize   = query.PageSize,
                TotalItems = total,
                TotalPages = (int)Math.Ceiling((double)total / query.PageSize),
            });
        }

        public Task<ReviewEntity?> GetById(Guid id) =>
            Task.FromResult(_store.FirstOrDefault(r => r.Id == id));

        public Task<ReviewEntity?> GetByIdWithUser(Guid id) =>
            Task.FromResult(_store.FirstOrDefault(r => r.Id == id));

        public Task<List<ReviewEntity>> GetByGameId(Guid gameId) =>
            Task.FromResult(_store.Where(r => r.GameId == gameId).ToList());

        public Task<bool> UserAlreadyReviewed(Guid userId, Guid gameId) =>
            Task.FromResult(_store.Any(r => r.UserId == userId && r.GameId == gameId));

        public Task<ReviewEntity> Create(ReviewEntity entity)
        {
            entity.Id        = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            entity.User = MockData.Users.First(u => u.Id == entity.UserId);
            entity.Game = MockData.Games.First(g => g.Id == entity.GameId);

            _store.Add(entity);
            RecalculateInMemory(entity.GameId);
            return Task.FromResult(entity);
        }

        public Task<ReviewEntity?> Update(ReviewEntity entity)
        {
            var existing = _store.FirstOrDefault(r => r.Id == entity.Id);
            if (existing is null) return Task.FromResult<ReviewEntity?>(null);

            existing.IsPositive   = entity.IsPositive;
            existing.HoursPlayed  = entity.HoursPlayed;
            existing.Content      = entity.Content;
            existing.UpdatedAt    = DateTime.UtcNow;

            RecalculateInMemory(existing.GameId);
            return Task.FromResult<ReviewEntity?>(existing);
        }

        public Task<bool> Delete(Guid id)
        {
            var entity = _store.FirstOrDefault(r => r.Id == id);
            if (entity is null) return Task.FromResult(false);
            var gameId = entity.GameId;
            _store.Remove(entity);
            RecalculateInMemory(gameId);
            return Task.FromResult(true);
        }

        public Task RecalculateGameStats(Guid gameId)
        {
            RecalculateInMemory(gameId);
            return Task.CompletedTask;
        }

        private static void RecalculateInMemory(Guid gameId)
        {
            var game = MockData.Games.FirstOrDefault(g => g.Id == gameId);
            if (game is null) return;

            var reviews = _store.Where(r => r.GameId == gameId).ToList();
            game.ReviewCount = reviews.Count;

            if (reviews.Count == 0)
            {
                game.Rating             = 0;
                game.PositivePercentage = 0;
                return;
            }

            game.PositivePercentage = (int)Math.Round(reviews.Count(r => r.IsPositive) * 100.0 / reviews.Count);
            game.Rating             = Math.Round((decimal)game.PositivePercentage / 20, 2);
        }
    }
}
