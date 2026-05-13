using AutoMapper;
using NSubstitute;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;
using nebula.api.src.Repositories;
using nebula.api.src.Services;
using nebula.api.tests.Unit.Helpers;

namespace nebula.api.tests.Unit.Services;

public class GameServiceTests
{
    private readonly nebula.api.src.Data.NebulaDbContext _db;
    private readonly IGameRepository _repository;
    private readonly IMapper _mapper;
    private readonly GameService _service;

    public GameServiceTests()
    {
        _db = DbContextFactory.Create();
        _repository = Substitute.For<IGameRepository>();
        _mapper = Substitute.For<IMapper>();
        _service = new GameService(_repository, _mapper, _db);
    }

    private void SetupCreate(GameEntity entity, GameDto? returnDto = null)
    {
        _mapper.Map<GameEntity>(Arg.Any<CreateGameDto>()).Returns(entity);
        _repository.GetOrCreateGenres(Arg.Any<string[]>()).Returns([]);
        _repository.Create(Arg.Any<GameEntity>()).Returns(entity);
        _mapper.Map<GameDto>(Arg.Any<GameEntity>()).Returns(returnDto ?? new GameDto());
    }

    // --- BeforeCreate string normalization ---

    [Fact]
    public async Task Create_trims_whitespace_from_title_and_description()
    {
        var entity = new GameEntity();
        SetupCreate(entity);

        await _service.Create(new CreateGameDto
        {
            Title = "  Hollow Knight  ",
            Description = "  A great game  ",
            GenreNames = []
        });

        entity.Title.ShouldBe("Hollow Knight");
        entity.Description.ShouldBe("A great game");
    }

    [Fact]
    public async Task Create_preserves_existing_entity_value_when_dto_field_is_null()
    {
        var entity = new GameEntity { Title = "Original Title", Description = "Original Desc" };
        SetupCreate(entity);

        await _service.Create(new CreateGameDto { Title = null, Description = null, GenreNames = [] });

        entity.Title.ShouldBe("Original Title");
        entity.Description.ShouldBe("Original Desc");
    }

    // --- BeforeCreate date parsing ---

    [Fact]
    public async Task Create_parses_valid_release_date_string()
    {
        var entity = new GameEntity();
        SetupCreate(entity);

        await _service.Create(new CreateGameDto { ReleaseDate = "2024-03-15", GenreNames = [] });

        entity.ReleaseDate.ShouldBe(new DateOnly(2024, 3, 15));
    }

    [Fact]
    public async Task Create_does_not_change_release_date_when_string_is_invalid()
    {
        var entity = new GameEntity { ReleaseDate = new DateOnly(2020, 1, 1) };
        SetupCreate(entity);

        await _service.Create(new CreateGameDto { ReleaseDate = "not-a-date", GenreNames = [] });

        entity.ReleaseDate.ShouldBe(new DateOnly(2020, 1, 1));
    }

    // --- BeforeCreate genre attachment ---

    [Fact]
    public async Task Create_attaches_resolved_genres_to_entity()
    {
        var entity = new GameEntity();
        var genre = new GenreEntity { Id = Guid.NewGuid(), Name = "RPG", Slug = "rpg" };
        _mapper.Map<GameEntity>(Arg.Any<CreateGameDto>()).Returns(entity);
        _repository.GetOrCreateGenres(Arg.Any<string[]>()).Returns([genre]);
        _repository.Create(Arg.Any<GameEntity>()).Returns(entity);
        _mapper.Map<GameDto>(Arg.Any<GameEntity>()).Returns(new GameDto());

        await _service.Create(new CreateGameDto { GenreNames = ["RPG"] });

        entity.GameGenres.Count.ShouldBe(1);
        entity.GameGenres.First().GenreId.ShouldBe(genre.Id);
    }

    // --- GetById delegates to GetByIdWithGenres ---

    [Fact]
    public async Task GetById_returns_null_when_game_does_not_exist()
    {
        _repository.GetByIdWithGenres(Arg.Any<Guid>()).Returns((GameEntity?)null);

        var result = await _service.GetById(Guid.NewGuid());

        result.ShouldBeNull();
    }

    [Fact]
    public async Task GetById_returns_dto_with_mapped_genres()
    {
        var gameId = Guid.NewGuid();
        var genre = new GenreEntity { Id = Guid.NewGuid(), Name = "Action", Slug = "action" };
        var game = new GameEntity
        {
            Id = gameId,
            Title = "Test",
            ReleaseDate = new DateOnly(2023, 1, 1),
            GameGenres = [new GameGenreEntity { GameId = gameId, GenreId = genre.Id, Genre = genre }]
        };
        _repository.GetByIdWithGenres(gameId).Returns(game);
        _mapper.Map<GameDto>(game).Returns(new GameDto { Id = gameId });

        var result = await _service.GetById(gameId);

        result.ShouldNotBeNull();
        result!.Id.ShouldBe(gameId);
        result.Genres.ShouldContain("Action");
    }

    // --- GetAllGenres ---

    [Fact]
    public async Task GetAllGenres_returns_genres_sorted_alphabetically_from_database()
    {
        _db.Genres.AddRange(
            new GenreEntity { Id = Guid.NewGuid(), Name = "Strategy", Slug = "strategy" },
            new GenreEntity { Id = Guid.NewGuid(), Name = "Action", Slug = "action" },
            new GenreEntity { Id = Guid.NewGuid(), Name = "RPG", Slug = "rpg" });
        await _db.SaveChangesAsync();

        _mapper.Map<List<GenreDto>>(Arg.Any<object>())
            .Returns(ci => ci.Arg<List<GenreEntity>>()
                .Select(g => new GenreDto { Name = g.Name })
                .ToList());

        var result = await _service.GetAllGenres();

        result.Count.ShouldBe(3);
        result[0].Name.ShouldBe("Action");
        result[1].Name.ShouldBe("RPG");
        result[2].Name.ShouldBe("Strategy");
    }
}
