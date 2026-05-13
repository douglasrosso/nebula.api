using nebula.api.src.Entities;
using nebula.api.src.Services;
using nebula.api.tests.Unit.Helpers;

namespace nebula.api.tests.Unit.Services;

public class WishlistServiceTests
{
    private readonly nebula.api.src.Data.NebulaDbContext _db;
    private readonly WishlistService _service;

    public WishlistServiceTests()
    {
        _db = DbContextFactory.Create();
        _service = new WishlistService(_db);
    }

    private async Task<GameEntity> SeedGame(string title = "Test Game", decimal price = 29.99m)
    {
        var game = new GameEntity { Id = Guid.NewGuid(), Title = title, Price = price };
        _db.Games.Add(game);
        await _db.SaveChangesAsync();
        return game;
    }

    // --- AddToWishlist ---

    [Fact]
    public async Task AddToWishlist_throws_KeyNotFound_when_game_does_not_exist()
    {
        await Should.ThrowAsync<KeyNotFoundException>(() =>
            _service.AddToWishlist(Guid.NewGuid(), Guid.NewGuid()));
    }

    [Fact]
    public async Task AddToWishlist_throws_when_game_is_already_in_wishlist()
    {
        var userId = Guid.NewGuid();
        var game = await SeedGame();
        _db.Wishlist.Add(new WishlistItemEntity { UserId = userId, GameId = game.Id, AddedAt = DateTime.UtcNow });
        await _db.SaveChangesAsync();

        await Should.ThrowAsync<InvalidOperationException>(() =>
            _service.AddToWishlist(userId, game.Id));
    }

    [Fact]
    public async Task AddToWishlist_returns_dto_with_correct_data()
    {
        var userId = Guid.NewGuid();
        var game = await SeedGame("Celeste", 19.99m);

        var result = await _service.AddToWishlist(userId, game.Id);

        result.GameId.ShouldBe(game.Id);
        result.Title.ShouldBe("Celeste");
        result.Price.ShouldBe(19.99m);
    }

    [Fact]
    public async Task AddToWishlist_persists_item_to_database()
    {
        var userId = Guid.NewGuid();
        var game = await SeedGame();

        await _service.AddToWishlist(userId, game.Id);

        _db.Wishlist.Any(w => w.UserId == userId && w.GameId == game.Id).ShouldBeTrue();
    }

    // --- RemoveFromWishlist ---

    [Fact]
    public async Task RemoveFromWishlist_returns_false_when_item_does_not_exist()
    {
        var result = await _service.RemoveFromWishlist(Guid.NewGuid(), Guid.NewGuid());
        result.ShouldBeFalse();
    }

    [Fact]
    public async Task RemoveFromWishlist_removes_item_and_returns_true()
    {
        var userId = Guid.NewGuid();
        var game = await SeedGame();
        _db.Wishlist.Add(new WishlistItemEntity { UserId = userId, GameId = game.Id, AddedAt = DateTime.UtcNow });
        await _db.SaveChangesAsync();

        var result = await _service.RemoveFromWishlist(userId, game.Id);

        result.ShouldBeTrue();
        _db.Wishlist.Any(w => w.UserId == userId).ShouldBeFalse();
    }

    [Fact]
    public async Task RemoveFromWishlist_does_not_affect_other_users_items()
    {
        var userId = Guid.NewGuid();
        var otherUser = Guid.NewGuid();
        var game = await SeedGame();
        _db.Wishlist.AddRange(
            new WishlistItemEntity { UserId = userId, GameId = game.Id, AddedAt = DateTime.UtcNow },
            new WishlistItemEntity { UserId = otherUser, GameId = game.Id, AddedAt = DateTime.UtcNow });
        await _db.SaveChangesAsync();

        await _service.RemoveFromWishlist(userId, game.Id);

        _db.Wishlist.Any(w => w.UserId == otherUser).ShouldBeTrue();
    }

    // --- GetWishlist ---

    [Fact]
    public async Task GetWishlist_returns_only_items_belonging_to_the_given_user()
    {
        var userId = Guid.NewGuid();
        var otherUser = Guid.NewGuid();
        var game = await SeedGame();
        _db.Wishlist.AddRange(
            new WishlistItemEntity { UserId = userId, GameId = game.Id, AddedAt = DateTime.UtcNow },
            new WishlistItemEntity { UserId = otherUser, GameId = game.Id, AddedAt = DateTime.UtcNow });
        await _db.SaveChangesAsync();

        var result = await _service.GetWishlist(userId);

        result.Count.ShouldBe(1);
        result[0].GameId.ShouldBe(game.Id);
    }

    [Fact]
    public async Task GetWishlist_returns_empty_list_when_wishlist_is_empty()
    {
        var result = await _service.GetWishlist(Guid.NewGuid());
        result.ShouldBeEmpty();
    }
}
