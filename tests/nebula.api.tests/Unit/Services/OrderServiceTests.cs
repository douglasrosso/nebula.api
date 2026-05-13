using NSubstitute;
using nebula.api.src.Entities;
using nebula.api.src.Services;
using nebula.api.tests.Unit.Helpers;

namespace nebula.api.tests.Unit.Services;

public class OrderServiceTests
{
    private readonly nebula.api.src.Data.NebulaDbContext _db;
    private readonly ILibraryService _libraryService;
    private readonly ICartService _cartService;
    private readonly OrderService _service;

    public OrderServiceTests()
    {
        _db = DbContextFactory.Create();
        _libraryService = Substitute.For<ILibraryService>();
        _cartService = Substitute.For<ICartService>();
        _service = new OrderService(_db, _libraryService, _cartService);
    }

    private async Task<GameEntity> SeedGame(decimal price)
    {
        var game = new GameEntity { Id = Guid.NewGuid(), Title = "Test Game", Price = price };
        _db.Games.Add(game);
        await _db.SaveChangesAsync();
        return game;
    }

    private async Task AddToCart(Guid userId, Guid gameId)
    {
        _db.Cart.Add(new CartItemEntity { UserId = userId, GameId = gameId, AddedAt = DateTime.UtcNow });
        await _db.SaveChangesAsync();
    }

    // --- Checkout ---

    [Fact]
    public async Task Checkout_throws_when_cart_is_empty()
    {
        await Should.ThrowAsync<InvalidOperationException>(() =>
            _service.Checkout(Guid.NewGuid()));
    }

    [Fact]
    public async Task Checkout_creates_order_with_total_equal_to_sum_of_game_prices()
    {
        var userId = Guid.NewGuid();
        var game1 = await SeedGame(10.00m);
        var game2 = await SeedGame(20.00m);
        await AddToCart(userId, game1.Id);
        await AddToCart(userId, game2.Id);

        var order = await _service.Checkout(userId);

        order.TotalAmount.ShouldBe(30.00m);
        order.Status.ShouldBe("completed");
    }

    [Fact]
    public async Task Checkout_clears_the_cart_for_the_user_after_purchase()
    {
        var userId = Guid.NewGuid();
        var game = await SeedGame(15.00m);
        await AddToCart(userId, game.Id);

        await _service.Checkout(userId);

        _db.Cart.Any(c => c.UserId == userId).ShouldBeFalse();
    }

    [Fact]
    public async Task Checkout_calls_AddToLibrary_for_each_purchased_game()
    {
        var userId = Guid.NewGuid();
        var game1 = await SeedGame(10.00m);
        var game2 = await SeedGame(20.00m);
        await AddToCart(userId, game1.Id);
        await AddToCart(userId, game2.Id);

        await _service.Checkout(userId);

        await _libraryService.Received(1).AddToLibrary(userId, game1.Id, Arg.Any<DateTime?>());
        await _libraryService.Received(1).AddToLibrary(userId, game2.Id, Arg.Any<DateTime?>());
    }

    [Fact]
    public async Task Checkout_creates_order_items_for_each_game_in_cart()
    {
        var userId = Guid.NewGuid();
        var game = await SeedGame(25.00m);
        await AddToCart(userId, game.Id);

        var order = await _service.Checkout(userId);

        order.Items.Count.ShouldBe(1);
        order.Items[0].GameId.ShouldBe(game.Id);
        order.Items[0].PricePaid.ShouldBe(25.00m);
    }

    // --- GetOrders ---

    [Fact]
    public async Task GetOrders_returns_only_orders_belonging_to_the_given_user()
    {
        var userId = Guid.NewGuid();
        var otherUser = Guid.NewGuid();
        _db.Orders.AddRange(
            new OrderEntity { Id = Guid.NewGuid(), UserId = userId, TotalAmount = 50m, Status = "completed", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new OrderEntity { Id = Guid.NewGuid(), UserId = otherUser, TotalAmount = 30m, Status = "completed", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
        await _db.SaveChangesAsync();

        var result = await _service.GetOrders(userId);

        result.Count.ShouldBe(1);
        result[0].TotalAmount.ShouldBe(50m);
    }

    [Fact]
    public async Task GetOrders_returns_empty_list_when_user_has_no_orders()
    {
        var result = await _service.GetOrders(Guid.NewGuid());
        result.ShouldBeEmpty();
    }

    // --- GetOrderById ---

    [Fact]
    public async Task GetOrderById_returns_null_when_order_does_not_exist()
    {
        var result = await _service.GetOrderById(Guid.NewGuid(), Guid.NewGuid());
        result.ShouldBeNull();
    }

    [Fact]
    public async Task GetOrderById_returns_null_when_order_belongs_to_another_user()
    {
        var order = new OrderEntity
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            TotalAmount = 10m,
            Status = "completed",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        var result = await _service.GetOrderById(order.Id, Guid.NewGuid());

        result.ShouldBeNull();
    }

    [Fact]
    public async Task GetOrderById_returns_order_dto_when_found_for_correct_user()
    {
        var userId = Guid.NewGuid();
        var order = new OrderEntity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            TotalAmount = 99.99m,
            Status = "completed",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        var result = await _service.GetOrderById(order.Id, userId);

        result.ShouldNotBeNull();
        result!.Id.ShouldBe(order.Id);
        result.TotalAmount.ShouldBe(99.99m);
    }
}
