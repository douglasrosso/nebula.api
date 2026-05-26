using nebula.api.src.Common.Repositories;
using nebula.api.src.Data.Mock;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;

namespace nebula.api.src.Repositories.Mock
{
    public class MockGameRepository : IGameRepository
    {
        private static readonly List<GameEntity> _store = MockData.Games;

        public Task<PaginatedResultDto<GameEntity>> Get(GameQueryDto query)
        {
            var items = _store.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(query.Search))
                items = items.Where(g =>
                    g.Title.Contains(query.Search, StringComparison.OrdinalIgnoreCase) ||
                    g.Description.Contains(query.Search, StringComparison.OrdinalIgnoreCase));

            if (query.Genres is { Length: > 0 })
                items = items.Where(g => g.GameGenres.Any(gg =>
                    query.Genres.Any(qg =>
                        gg.Genre.Slug.Equals(qg, StringComparison.OrdinalIgnoreCase) ||
                        gg.Genre.Name.Equals(qg, StringComparison.OrdinalIgnoreCase))));

            if (query.MinPrice.HasValue)
                items = items.Where(g => g.Price >= query.MinPrice.Value);

            if (query.MaxPrice.HasValue)
                items = items.Where(g => g.Price <= query.MaxPrice.Value);

            items = (query.SortBy.ToLower(), query.SortDirection.ToLower()) switch
            {
                ("price",  "asc")  => items.OrderBy(g => g.Price),
                ("price",  _)      => items.OrderByDescending(g => g.Price),
                ("rating", "asc")  => items.OrderBy(g => g.Rating),
                ("rating", _)      => items.OrderByDescending(g => g.Rating),
                ("title",  "asc")  => items.OrderBy(g => g.Title),
                ("title",  _)      => items.OrderByDescending(g => g.Title),
                (_,        "asc")  => items.OrderBy(g => g.CreatedAt),
                _                  => items.OrderByDescending(g => g.CreatedAt),
            };

            var list = items.ToList();
            return Task.FromResult(Paginate(list, query.Page, query.PageSize));
        }

        public Task<GameEntity?> GetById(Guid id) =>
            Task.FromResult(_store.FirstOrDefault(g => g.Id == id));

        public Task<GameEntity?> GetByIdWithGenres(Guid id) =>
            Task.FromResult(_store.FirstOrDefault(g => g.Id == id));

        public Task<GameEntity> Create(GameEntity entity)
        {
            entity.Id        = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            _store.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<GameEntity?> Update(GameEntity entity)
        {
            var existing = _store.FirstOrDefault(g => g.Id == entity.Id);
            if (existing is null) return Task.FromResult<GameEntity?>(null);

            existing.Title               = entity.Title;
            existing.Description         = entity.Description;
            existing.LongDescription     = entity.LongDescription;
            existing.Price               = entity.Price;
            existing.OriginalPrice       = entity.OriginalPrice;
            existing.Discount            = entity.Discount;
            existing.CoverImage          = entity.CoverImage;
            existing.Screenshots         = entity.Screenshots;
            existing.Developer           = entity.Developer;
            existing.Publisher           = entity.Publisher;
            existing.ReleaseDate         = entity.ReleaseDate;
            existing.Tags                = entity.Tags;
            existing.Features            = entity.Features;
            existing.SystemRequirements  = entity.SystemRequirements;
            existing.IsActive            = entity.IsActive;
            existing.UpdatedAt           = DateTime.UtcNow;

            return Task.FromResult<GameEntity?>(existing);
        }

        public Task<bool> Delete(Guid id)
        {
            var entity = _store.FirstOrDefault(g => g.Id == id);
            if (entity is null) return Task.FromResult(false);
            _store.Remove(entity);
            return Task.FromResult(true);
        }

        public Task<List<GenreEntity>> GetOrCreateGenres(string[] genreNames)
        {
            var result = new List<GenreEntity>();
            foreach (var name in genreNames)
            {
                var genre = MockData.Genres.FirstOrDefault(g =>
                    g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                if (genre is null)
                {
                    genre = new GenreEntity
                    {
                        Id        = Guid.NewGuid(),
                        Name      = name,
                        Slug      = name.ToLower().Replace(" ", "-"),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    };
                    MockData.Genres.Add(genre);
                }

                result.Add(genre);
            }
            return Task.FromResult(result);
        }

        private static PaginatedResultDto<GameEntity> Paginate(List<GameEntity> items, int page, int pageSize)
        {
            var total = items.Count;
            return new PaginatedResultDto<GameEntity>
            {
                Items      = items.Skip((page - 1) * pageSize).Take(pageSize),
                Page       = page,
                PageSize   = pageSize,
                TotalItems = total,
                TotalPages = (int)Math.Ceiling((double)total / pageSize),
            };
        }
    }
}
