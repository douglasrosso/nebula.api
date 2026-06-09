using NSubstitute;
using nebula.api.src.Entities;
using nebula.api.src.Repositories;
using nebula.api.src.Services;
using nebula.api.tests.Unit.Helpers;

namespace nebula.api.tests.Unit.Services;

public class ReviewServiceTests
{
    private readonly IReviewRepository _repository;
    private readonly ReviewService _service;

    public ReviewServiceTests()
    {
        _repository = Substitute.For<IReviewRepository>();
        _service = new ReviewService(_repository);
    }

    private static ReviewEntity BuildReviewWithUser(Guid? gameId = null, bool isPositive = true) =>
        new()
        {
            Id = Guid.NewGuid(),
            GameId = gameId ?? Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            IsPositive = isPositive,
            Content = "Great game!",
            User = new UserEntity { Name = "Alice", DisplayName = "Alice" },
            Game = new GameEntity { Id = gameId ?? Guid.NewGuid() }
        };

    [Fact]
    public async Task GetByGameId_returns_reviews_for_the_given_game()
    {
        var gameId = Guid.NewGuid();
        var reviews = new List<ReviewEntity>
        {
            BuildReviewWithUser(gameId, isPositive: true),
            BuildReviewWithUser(gameId, isPositive: false)
        };
        _repository.GetByGameId(gameId).Returns(reviews);

        var result = await _service.GetByGameId(gameId);

        result.Count.ShouldBe(2);
        result.All(r => r.GameId == gameId).ShouldBeTrue();
    }

    [Fact]
    public async Task GetByGameId_maps_rating_correctly()
    {
        var gameId = Guid.NewGuid();
        _repository.GetByGameId(gameId).Returns(new List<ReviewEntity>
        {
            BuildReviewWithUser(gameId, isPositive: true),
            BuildReviewWithUser(gameId, isPositive: false)
        });

        var result = await _service.GetByGameId(gameId);

        result[0].Rating.ShouldBe("positive");
        result[1].Rating.ShouldBe("negative");
    }

    [Fact]
    public async Task GetByGameId_returns_empty_list_when_no_reviews_exist()
    {
        var gameId = Guid.NewGuid();
        _repository.GetByGameId(gameId).Returns(new List<ReviewEntity>());

        var result = await _service.GetByGameId(gameId);

        result.ShouldBeEmpty();
    }
}
