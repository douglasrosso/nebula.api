using Microsoft.EntityFrameworkCore;
using nebula.api.src.Common.Repositories;
using nebula.api.src.Data;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;

namespace nebula.api.src.Repositories
{
    public class GameRepository : BaseRepository<GameEntity, GameQueryDto>, IGameRepository
    {
        private readonly NebulaDbContext _nebulaContext;

        public GameRepository(NebulaDbContext context) : base(context)
        {
            _nebulaContext = context;
        }

        protected override IQueryable<GameEntity> ApplyFilters(IQueryable<GameEntity> query, GameQueryDto queryDto)
        {
            query = query
                .Include(g => g.GameGenres)
                .ThenInclude(gg => gg.Genre)
                .Where(g => g.IsActive);

            if (!string.IsNullOrWhiteSpace(queryDto.Search))
            {
                var search = $"%{queryDto.Search.Trim()}%";
                query = query.Where(g =>
                    EF.Functions.ILike(g.Title, search) ||
                    EF.Functions.ILike(g.Description, search));
            }

            if (queryDto.Genres is { Length: > 0 })
            {
                query = query.Where(g =>
                    g.GameGenres.Any(gg => queryDto.Genres.Contains(gg.Genre.Name)));
            }

            if (queryDto.MinPrice.HasValue)
                query = query.Where(g => g.Price >= queryDto.MinPrice.Value);

            if (queryDto.MaxPrice.HasValue)
                query = query.Where(g => g.Price <= queryDto.MaxPrice.Value);

            var isDesc = queryDto.SortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase);

            query = queryDto.SortBy.Trim().ToLowerInvariant() switch
            {
                "name" => isDesc
                    ? query.OrderByDescending(g => g.Title)
                    : query.OrderBy(g => g.Title),
                "price" or "lowest-price" or "price-low" => query.OrderBy(g => g.Price),
                "highest-price" or "price-high" => query.OrderByDescending(g => g.Price),
                "rating" or "reviews" => query.OrderByDescending(g => g.PositivePercentage),
                "release-date" or "releasedate" => query.OrderByDescending(g => g.ReleaseDate),
                _ => query.OrderByDescending(g => g.ReviewCount)
            };

            return query;
        }

        public async Task<GameEntity?> GetByIdWithGenres(Guid id)
        {
            return await _nebulaContext.Games
                .AsNoTracking()
                .Include(g => g.GameGenres)
                .ThenInclude(gg => gg.Genre)
                .FirstOrDefaultAsync(g => g.Id == id && g.IsActive);
        }

        public async Task<List<GenreEntity>> GetOrCreateGenres(string[] genreNames)
        {
            var genres = new List<GenreEntity>();

            foreach (var name in genreNames.Where(n => !string.IsNullOrWhiteSpace(n)))
            {
                var slug = name.Trim().ToLowerInvariant().Replace(" ", "-");
                var genre = await _nebulaContext.Genres.FirstOrDefaultAsync(g => g.Slug == slug);

                if (genre is null)
                {
                    genre = new GenreEntity
                    {
                        Id = Guid.NewGuid(),
                        Name = name.Trim(),
                        Slug = slug,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    _nebulaContext.Genres.Add(genre);
                }

                genres.Add(genre);
            }

            return genres;
        }
    }
}
