using System.Net;
using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using nebula.api.src.Data;
using nebula.api.src.DTOs;
using nebula.api.tests.Integration.Fixtures;

namespace nebula.api.tests.Integration.Controllers;

[Collection(nameof(ApiCollection))]
public class UsersControllerTests : IAsyncLifetime
{
    private readonly ApiFixture _fixture;

    public UsersControllerTests(ApiFixture fixture)
    {
        _fixture = fixture;
    }

    public Task InitializeAsync() => _fixture.ResetDatabaseAsync();
    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task POST_users_creates_a_new_user_and_persists_it()
    {
        var client = _fixture.CreateClient();

        var response = await client.PostAsJsonAsync("/api/users", new CreateUserDto
        {
            Name = "Anderson",
            Email = "anderson@example.com",
            Password = "hunter22"
        });

        response.StatusCode.ShouldBe(HttpStatusCode.Created);

        var created = await response.Content.ReadFromJsonAsync<UserDto>();
        created.ShouldNotBeNull();
        created!.Email.ShouldBe("anderson@example.com");
        created.Name.ShouldBe("Anderson");
        created.Id.ShouldNotBe(Guid.Empty);

        using var scope = _fixture.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<NebulaDbContext>();
        var persisted = await db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == created.Id);
        persisted.ShouldNotBeNull();
        persisted!.Password.ShouldNotBe("hunter22", "password must be hashed before persistence");
    }

    [Fact]
    public async Task POST_users_returns_409_when_email_already_exists()
    {
        var client = _fixture.CreateClient();
        var dto = new CreateUserDto
        {
            Name = "Anderson",
            Email = "duplicate@example.com",
            Password = "hunter22"
        };

        var first = await client.PostAsJsonAsync("/api/users", dto);
        first.StatusCode.ShouldBe(HttpStatusCode.Created);

        var second = await client.PostAsJsonAsync("/api/users", dto);
        second.StatusCode.ShouldBe(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task POST_users_normalizes_email_to_lowercase_so_duplicates_are_detected_case_insensitively()
    {
        var client = _fixture.CreateClient();

        var first = await client.PostAsJsonAsync("/api/users", new CreateUserDto
        {
            Name = "Anderson",
            Email = "MixedCase@Example.com",
            Password = "hunter22"
        });
        first.StatusCode.ShouldBe(HttpStatusCode.Created);

        var second = await client.PostAsJsonAsync("/api/users", new CreateUserDto
        {
            Name = "Anderson 2",
            Email = "mixedcase@example.com",
            Password = "hunter22"
        });
        second.StatusCode.ShouldBe(HttpStatusCode.Conflict);
    }
}
