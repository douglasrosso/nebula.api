using AutoMapper;
using NSubstitute;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;
using nebula.api.src.Repositories;
using nebula.api.src.Services;
using nebula.api.tests.Unit.Helpers;

namespace nebula.api.tests.Unit.Services;

public class ReviewServiceTests
{
    private readonly nebula.api.src.Data.NebulaDbContext _db;
    private readonly IReviewRepository _repository;
    private readonly IMapper _mapper;
    private readonly ReviewService _service;

    public ReviewServiceTests()
    {
        _db = DbContextFactory.Create();
        _repository = Substitute.For<IReviewRepository>();
        _mapper = Substitute.For<IMapper>();
        _service = new ReviewService(_repository, _mapper, _db);
    }

    private ReviewEntity BuildReviewWithUser(Guid? id = null, Guid? gameId = null, bool isPositive = true) =>
        new()
        {
            Id = id ?? Guid.NewGuid(),
            GameId = gameId ?? Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            IsPositive = isPositive,
            Content = "Great game!",
            User = new UserEntity { Name = "Alice", DisplayName = "Alice" },
            Game = new GameEntity { Id = gameId ?? Guid.NewGuid() }
        };

    // --- CreateReview ---

    [Fact]
    public async Task CreateReview_throws_when_user_already_reviewed_the_game()
    {
        var userId = Guid.NewGuid();
        var gameId = Guid.NewGuid();
        _repository.UserAlreadyReviewed(userId, gameId).Returns(true);

        await Should.ThrowAsync<InvalidOperationException>(() =>
            _service.CreateReview(userId, new CreateReviewDto
            {
                GameId = gameId,
                Rating = "positive",
                Content = "Amazing!"
            }));
    }

    [Fact]
    public async Task CreateReview_sets_rating_to_positive_when_dto_rating_is_positive()
    {
        var userId = Guid.NewGuid();
        var gameId = Guid.NewGuid();
        _repository.UserAlreadyReviewed(userId, gameId).Returns(false);
        _repository.RecalculateGameStats(gameId).Returns(Task.CompletedTask);
        _repository.GetByIdWithUser(Arg.Any<Guid>()).Returns(BuildReviewWithUser(gameId: gameId, isPositive: true));

        var result = await _service.CreateReview(userId, new CreateReviewDto
        {
            GameId = gameId,
            Rating = "positive",
            Content = "Great game!"
        });

        result.Rating.ShouldBe("positive");
    }

    [Fact]
    public async Task CreateReview_sets_rating_to_negative_when_dto_rating_is_negative()
    {
        var userId = Guid.NewGuid();
        var gameId = Guid.NewGuid();
        _repository.UserAlreadyReviewed(userId, gameId).Returns(false);
        _repository.RecalculateGameStats(gameId).Returns(Task.CompletedTask);
        _repository.GetByIdWithUser(Arg.Any<Guid>()).Returns(BuildReviewWithUser(gameId: gameId, isPositive: false));

        var result = await _service.CreateReview(userId, new CreateReviewDto
        {
            GameId = gameId,
            Rating = "negative",
            Content = "Not my style"
        });

        result.Rating.ShouldBe("negative");
    }

    [Fact]
    public async Task CreateReview_persists_review_to_database()
    {
        var userId = Guid.NewGuid();
        var gameId = Guid.NewGuid();
        _repository.UserAlreadyReviewed(userId, gameId).Returns(false);
        _repository.RecalculateGameStats(gameId).Returns(Task.CompletedTask);
        _repository.GetByIdWithUser(Arg.Any<Guid>()).Returns(BuildReviewWithUser(gameId: gameId));

        await _service.CreateReview(userId, new CreateReviewDto
        {
            GameId = gameId,
            Rating = "positive",
            Content = "Loved it!"
        });

        _db.Reviews.Any(r => r.UserId == userId && r.GameId == gameId).ShouldBeTrue();
    }

    [Fact]
    public async Task CreateReview_recalculates_game_stats_after_saving()
    {
        var userId = Guid.NewGuid();
        var gameId = Guid.NewGuid();
        _repository.UserAlreadyReviewed(userId, gameId).Returns(false);
        _repository.RecalculateGameStats(gameId).Returns(Task.CompletedTask);
        _repository.GetByIdWithUser(Arg.Any<Guid>()).Returns(BuildReviewWithUser(gameId: gameId));

        await _service.CreateReview(userId, new CreateReviewDto
        {
            GameId = gameId,
            Rating = "positive",
            Content = "Great game!"
        });

        await _repository.Received(1).RecalculateGameStats(gameId);
    }

    // --- MarkHelpful ---

    [Fact]
    public async Task MarkHelpful_returns_false_when_review_does_not_exist()
    {
        var result = await _service.MarkHelpful(Guid.NewGuid());
        result.ShouldBeFalse();
    }

    [Fact]
    public async Task MarkHelpful_increments_helpful_count_and_returns_true()
    {
        var review = new ReviewEntity
        {
            Id = Guid.NewGuid(),
            GameId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            HelpfulCount = 3,
            Content = "test"
        };
        _db.Reviews.Add(review);
        await _db.SaveChangesAsync();

        var result = await _service.MarkHelpful(review.Id);

        result.ShouldBeTrue();
        (await _db.Reviews.FindAsync(review.Id))!.HelpfulCount.ShouldBe(4);
    }

    // --- MarkFunny ---

    [Fact]
    public async Task MarkFunny_returns_false_when_review_does_not_exist()
    {
        var result = await _service.MarkFunny(Guid.NewGuid());
        result.ShouldBeFalse();
    }

    [Fact]
    public async Task MarkFunny_increments_funny_count_and_returns_true()
    {
        var review = new ReviewEntity
        {
            Id = Guid.NewGuid(),
            GameId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FunnyCount = 7,
            Content = "test"
        };
        _db.Reviews.Add(review);
        await _db.SaveChangesAsync();

        var result = await _service.MarkFunny(review.Id);

        result.ShouldBeTrue();
        (await _db.Reviews.FindAsync(review.Id))!.FunnyCount.ShouldBe(8);
    }

    // --- Delete ---

    [Fact]
    public async Task Delete_returns_false_when_review_does_not_exist()
    {
        var result = await _service.Delete(Guid.NewGuid());
        result.ShouldBeFalse();
    }

    [Fact]
    public async Task Delete_removes_review_from_database_and_returns_true()
    {
        var gameId = Guid.NewGuid();
        var review = new ReviewEntity
        {
            Id = Guid.NewGuid(),
            GameId = gameId,
            UserId = Guid.NewGuid(),
            Content = "test"
        };
        _db.Reviews.Add(review);
        await _db.SaveChangesAsync();
        _repository.RecalculateGameStats(gameId).Returns(Task.CompletedTask);

        var result = await _service.Delete(review.Id);

        result.ShouldBeTrue();
        _db.Reviews.Any().ShouldBeFalse();
    }

    [Fact]
    public async Task Delete_recalculates_game_stats_after_removal()
    {
        var gameId = Guid.NewGuid();
        var review = new ReviewEntity
        {
            Id = Guid.NewGuid(),
            GameId = gameId,
            UserId = Guid.NewGuid(),
            Content = "test"
        };
        _db.Reviews.Add(review);
        await _db.SaveChangesAsync();
        _repository.RecalculateGameStats(gameId).Returns(Task.CompletedTask);

        await _service.Delete(review.Id);

        await _repository.Received(1).RecalculateGameStats(gameId);
    }
}
