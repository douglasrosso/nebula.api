using nebula.api.src.Entities;
using nebula.api.src.Services;
using nebula.api.tests.Unit.Helpers;

namespace nebula.api.tests.Unit.Services;

public class CartServiceTests
{
    private readonly nebula.api.src.Data.NebulaDbContext _db;
    private readonly CartService _service;

    public CartServiceTests()
    {
        _db = DbContextFactory.Create();
        _service = new CartService(_db);
    }

    private async Task<GameEntity> SeedGame(decimal price = 29.99m)
    {
        var game = new GameEntity
        {
            Id = Guid.NewGuid(),
            Title = "Test Game",
            Price = price
        };
        _db.Games.Add(game);
        await _db.SaveChangesAsync();
        return game;
    }

    // --- AddToCart ---

    [Fact]
    public async Task AddToCart_throws_KeyNotFound_when_game_does_not_exist()
    {
        await Should.ThrowAsync<KeyNotFoundException>(() =>
            _service.AddToCart(Guid.NewGuid(), Guid.NewGuid()));
    }

    [Fact]
    public async Task AddToCart_throws_when_game_is_already_in_cart()
    {
        var userId = Guid.NewGuid();
        var game = await SeedGame();
        _db.Cart.Add(new CartItemEntity { UserId = userId, GameId = game.Id, AddedAt = DateTime.UtcNow });
        await _db.SaveChangesAsync();

        await Should.ThrowAsync<InvalidOperationException>(() =>
            _service.AddToCart(userId, game.Id));
    }

    [Fact]
    public async Task AddToCart_throws_when_user_already_owns_the_game()
    {
        var userId = Guid.NewGuid();
        var game = await SeedGame();
        _db.UserLibrary.Add(new UserLibraryEntity { UserId = userId, GameId = game.Id, AcquiredAt = DateTime.UtcNow });
        await _db.SaveChangesAsync();

        await Should.ThrowAsync<InvalidOperationException>(() =>
            _service.AddToCart(userId, game.Id));
    }

    [Fact]
    public async Task AddToCart_returns_dto_with_correct_price()
    {
        var userId = Guid.NewGuid();
        var game = await SeedGame(price: 49.99m);

        var result = await _service.AddToCart(userId, game.Id);

        result.GameId.ShouldBe(game.Id);
        result.Price.ShouldBe(49.99m);
        result.Title.ShouldBe("Test Game");
    }

    [Fact]
    public async Task AddToCart_persists_item_to_database()
    {
        var userId = Guid.NewGuid();
        var game = await SeedGame();

        await _service.AddToCart(userId, game.Id);

        _db.Cart.Any(c => c.UserId == userId && c.GameId == game.Id).ShouldBeTrue();
    }

    // --- RemoveFromCart ---

    [Fact]
    public async Task RemoveFromCart_returns_false_when_item_does_not_exist()
    {
        var result = await _service.RemoveFromCart(Guid.NewGuid(), Guid.NewGuid());
        result.ShouldBeFalse();
    }

    [Fact]
    public async Task RemoveFromCart_removes_item_and_returns_true()
    {
        var userId = Guid.NewGuid();
        var game = await SeedGame();
        _db.Cart.Add(new CartItemEntity { UserId = userId, GameId = game.Id, AddedAt = DateTime.UtcNow });
        await _db.SaveChangesAsync();

        var result = await _service.RemoveFromCart(userId, game.Id);

        result.ShouldBeTrue();
        _db.Cart.Any().ShouldBeFalse();
    }

    // --- ClearCart ---

    [Fact]
    public async Task ClearCart_removes_all_items_belonging_to_the_user()
    {
        var userId = Guid.NewGuid();
        var game1 = await SeedGame();
        var game2 = await SeedGame();
        _db.Cart.AddRange(
            new CartItemEntity { UserId = userId, GameId = game1.Id, AddedAt = DateTime.UtcNow },
            new CartItemEntity { UserId = userId, GameId = game2.Id, AddedAt = DateTime.UtcNow });
        await _db.SaveChangesAsync();

        await _service.ClearCart(userId);

        _db.Cart.Any(c => c.UserId == userId).ShouldBeFalse();
    }

    [Fact]
    public async Task ClearCart_does_not_remove_items_belonging_to_other_users()
    {
        var userId = Guid.NewGuid();
        var otherUser = Guid.NewGuid();
        var game = await SeedGame();
        _db.Cart.AddRange(
            new CartItemEntity { UserId = userId, GameId = game.Id, AddedAt = DateTime.UtcNow },
            new CartItemEntity { UserId = otherUser, GameId = game.Id, AddedAt = DateTime.UtcNow });
        await _db.SaveChangesAsync();

        await _service.ClearCart(userId);

        _db.Cart.Any(c => c.UserId == otherUser).ShouldBeTrue();
    }

    // --- GetCart ---

    [Fact]
    public async Task GetCart_returns_only_items_belonging_to_the_given_user()
    {
        var userId = Guid.NewGuid();
        var otherUser = Guid.NewGuid();
        var game = await SeedGame();
        _db.Cart.AddRange(
            new CartItemEntity { UserId = userId, GameId = game.Id, AddedAt = DateTime.UtcNow },
            new CartItemEntity { UserId = otherUser, GameId = game.Id, AddedAt = DateTime.UtcNow });
        await _db.SaveChangesAsync();

        var result = await _service.GetCart(userId);

        result.Count.ShouldBe(1);
        result[0].GameId.ShouldBe(game.Id);
    }

    [Fact]
    public async Task GetCart_returns_empty_list_when_cart_is_empty()
    {
        var result = await _service.GetCart(Guid.NewGuid());
        result.ShouldBeEmpty();
    }
}
