using nebula.api.src.Entities;
using nebula.api.src.Services;
using nebula.api.tests.Unit.Helpers;

namespace nebula.api.tests.Unit.Services;

public class LibraryServiceTests
{
    private readonly nebula.api.src.Data.NebulaDbContext _db;
    private readonly LibraryService _service;

    public LibraryServiceTests()
    {
        _db = DbContextFactory.Create();
        _service = new LibraryService(_db);
    }

    // --- AddToLibrary ---

    [Fact]
    public async Task AddToLibrary_adds_item_when_game_is_not_yet_owned()
    {
        var userId = Guid.NewGuid();
        var gameId = Guid.NewGuid();

        await _service.AddToLibrary(userId, gameId);

        _db.UserLibrary.Any(l => l.UserId == userId && l.GameId == gameId).ShouldBeTrue();
    }

    [Fact]
    public async Task AddToLibrary_uses_provided_acquiredAt_date()
    {
        var userId = Guid.NewGuid();
        var gameId = Guid.NewGuid();
        var date = new DateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc);

        await _service.AddToLibrary(userId, gameId, date);

        var entry = _db.UserLibrary.First(l => l.UserId == userId && l.GameId == gameId);
        entry.AcquiredAt.ShouldBe(date);
    }

    [Fact]
    public async Task AddToLibrary_skips_silently_when_game_is_already_owned()
    {
        var userId = Guid.NewGuid();
        var gameId = Guid.NewGuid();
        _db.UserLibrary.Add(new UserLibraryEntity { UserId = userId, GameId = gameId, AcquiredAt = DateTime.UtcNow });
        await _db.SaveChangesAsync();

        await _service.AddToLibrary(userId, gameId);

        _db.UserLibrary.Count(l => l.UserId == userId && l.GameId == gameId).ShouldBe(1);
    }

    // --- IsInLibrary ---

    [Fact]
    public async Task IsInLibrary_returns_true_when_game_is_owned()
    {
        var userId = Guid.NewGuid();
        var gameId = Guid.NewGuid();
        _db.UserLibrary.Add(new UserLibraryEntity { UserId = userId, GameId = gameId, AcquiredAt = DateTime.UtcNow });
        await _db.SaveChangesAsync();

        var result = await _service.IsInLibrary(userId, gameId);

        result.ShouldBeTrue();
    }

    [Fact]
    public async Task IsInLibrary_returns_false_when_game_is_not_owned()
    {
        var result = await _service.IsInLibrary(Guid.NewGuid(), Guid.NewGuid());
        result.ShouldBeFalse();
    }

    [Fact]
    public async Task IsInLibrary_returns_false_when_game_belongs_to_another_user()
    {
        var gameId = Guid.NewGuid();
        _db.UserLibrary.Add(new UserLibraryEntity { UserId = Guid.NewGuid(), GameId = gameId, AcquiredAt = DateTime.UtcNow });
        await _db.SaveChangesAsync();

        var result = await _service.IsInLibrary(Guid.NewGuid(), gameId);

        result.ShouldBeFalse();
    }

    // --- CountByUser ---

    [Fact]
    public async Task CountByUser_returns_zero_when_library_is_empty()
    {
        var count = await _service.CountByUser(Guid.NewGuid());
        count.ShouldBe(0);
    }

    [Fact]
    public async Task CountByUser_returns_correct_count_for_given_user()
    {
        var userId = Guid.NewGuid();
        _db.UserLibrary.AddRange(
            new UserLibraryEntity { UserId = userId, GameId = Guid.NewGuid(), AcquiredAt = DateTime.UtcNow },
            new UserLibraryEntity { UserId = userId, GameId = Guid.NewGuid(), AcquiredAt = DateTime.UtcNow },
            new UserLibraryEntity { UserId = Guid.NewGuid(), GameId = Guid.NewGuid(), AcquiredAt = DateTime.UtcNow });
        await _db.SaveChangesAsync();

        var count = await _service.CountByUser(userId);

        count.ShouldBe(2);
    }
}
