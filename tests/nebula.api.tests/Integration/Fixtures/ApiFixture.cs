using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using nebula.api.src.Data;
using Npgsql;
using Respawn;
using Testcontainers.PostgreSql;

namespace nebula.api.tests.Integration.Fixtures;

public class ApiFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
        .WithImage("postgres:16-alpine")
        .Build();

    private WebApplicationFactory<Program> _factory = null!;
    private Respawner _respawner = null!;
    private string _connectionString = null!;

    public HttpClient CreateClient() => _factory.CreateClient();

    public IServiceScope CreateScope() => _factory.Services.CreateScope();

    public async Task InitializeAsync()
    {
        await _postgres.StartAsync();
        _connectionString = _postgres.GetConnectionString();

        Environment.SetEnvironmentVariable("CONNECTION_STRING", _connectionString);
        Environment.SetEnvironmentVariable("JWT_KEY", "integration-test-key-must-be-at-least-32-chars");
        Environment.SetEnvironmentVariable("JWT_ISSUER", "nebula.tests");
        Environment.SetEnvironmentVariable("JWT_AUDIENCE", "nebula.tests");

        _factory = new WebApplicationFactory<Program>();

        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<NebulaDbContext>();
            await db.Database.MigrateAsync();
        }

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        _respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"],
            TablesToIgnore = [new Respawn.Graph.Table("__EFMigrationsHistory")]
        });
    }

    public async Task ResetDatabaseAsync()
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        await _respawner.ResetAsync(connection);
    }

    public async Task DisposeAsync()
    {
        await _factory.DisposeAsync();
        await _postgres.DisposeAsync();
    }
}
