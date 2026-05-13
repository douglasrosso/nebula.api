using AutoMapper;
using Microsoft.EntityFrameworkCore;
using nebula.api.src.Common.DTOs;
using nebula.api.src.Common.Services;
using nebula.api.src.Data;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;
using nebula.api.src.Repositories;

namespace nebula.api.src.Services
{
    public class GameService
        : BaseService<GameEntity, GameDto, CreateGameDto, UpdateGameDto, GameQueryDto>,
          IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly NebulaDbContext _context;

        public GameService(IGameRepository gameRepository, IMapper mapper, NebulaDbContext context)
            : base(gameRepository, mapper)
        {
            _gameRepository = gameRepository;
            _context = context;
        }

        public async Task<PaginatedResultDto<GameSummaryDto>> GetSummaries(GameQueryDto query)
        {
            var result = await _gameRepository.Get(query);
            return new PaginatedResultDto<GameSummaryDto>
            {
                Items = result.Items.Select(MapGameToSummaryDto).ToList(),
                Page = result.Page,
                PageSize = result.PageSize,
                TotalItems = result.TotalItems,
                TotalPages = result.TotalPages
            };
        }

        public async Task<GameDto?> GetByIdWithGenres(Guid id)
        {
            var game = await _gameRepository.GetByIdWithGenres(id);
            return game is null ? null : MapGameToDto(game);
        }

        public async Task<List<GenreDto>> GetAllGenres()
        {
            var genres = await _context.Genres
                .AsNoTracking()
                .OrderBy(g => g.Name)
                .ToListAsync();

            return _mapper.Map<List<GenreDto>>(genres);
        }

        protected override async Task BeforeCreate(GameEntity entity, CreateGameDto dto)
        {
            ApplyNullSafeStrings(entity, dto.Title, dto.Description, dto.LongDescription,
                dto.CoverImage, dto.Developer, dto.Publisher);

            if (DateOnly.TryParse(dto.ReleaseDate, out var date))
                entity.ReleaseDate = date;

            await AttachGenres(entity, dto.GenreNames);
        }

        protected override async Task BeforeUpdate(GameEntity entity, UpdateGameDto dto)
        {
            ApplyNullSafeStrings(entity, dto.Title, dto.Description, dto.LongDescription,
                dto.CoverImage, dto.Developer, dto.Publisher);

            if (DateOnly.TryParse(dto.ReleaseDate, out var date))
                entity.ReleaseDate = date;

            _context.GameGenres.RemoveRange(
                _context.GameGenres.Where(gg => gg.GameId == entity.Id));

            await AttachGenres(entity, dto.GenreNames);
        }

        private static void ApplyNullSafeStrings(GameEntity entity,
            string? title, string? description, string? longDescription,
            string? coverImage, string? developer, string? publisher)
        {
            entity.Title = title?.Trim() ?? entity.Title;
            entity.Description = description?.Trim() ?? entity.Description;
            entity.LongDescription = longDescription?.Trim() ?? entity.LongDescription;
            entity.CoverImage = coverImage?.Trim() ?? entity.CoverImage;
            entity.Developer = developer?.Trim() ?? entity.Developer;
            entity.Publisher = publisher?.Trim() ?? entity.Publisher;
        }

        private async Task AttachGenres(GameEntity entity, string[] genreNames)
        {
            var genres = await _gameRepository.GetOrCreateGenres(genreNames);

            entity.GameGenres = genres.Select(g => new GameGenreEntity
            {
                GameId = entity.Id,
                GenreId = g.Id,
                Genre = g
            }).ToList();
        }

        // Overrides base GetById to include genres in the result
        public override async Task<GameDto?> GetById(Guid id)
        {
            return await GetByIdWithGenres(id);
        }

        private GameDto MapGameToDto(GameEntity game)
        {
            var dto = _mapper.Map<GameDto>(game);
            dto.Genres = game.GameGenres.Select(gg => gg.Genre.Name).ToArray();
            dto.ReleaseDate = game.ReleaseDate.ToString("yyyy-MM-dd");
            return dto;
        }

        private GameSummaryDto MapGameToSummaryDto(GameEntity game)
        {
            var dto = _mapper.Map<GameSummaryDto>(game);
            dto.Genres = game.GameGenres.Select(gg => gg.Genre.Name).ToArray();
            return dto;
        }
    }
}
