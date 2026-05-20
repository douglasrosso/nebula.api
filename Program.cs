using System.Text;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using nebula.api.src.Data;
using nebula.api.src.Extensions;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

if (string.IsNullOrEmpty(jwtKey) || jwtKey.Length < 32)
    throw new InvalidOperationException("JWT_KEY inválida ou não encontrada.");

if (string.IsNullOrEmpty(jwtIssuer))
    throw new InvalidOperationException("JWT_ISSUER não definido.");

if (string.IsNullOrEmpty(jwtAudience))
    throw new InvalidOperationException("JWT_AUDIENCE não definido.");

if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("CONNECTION_STRING não definida.");

var key = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddDbContext<NebulaDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddApplicationServices();
builder.Services.AddControllers().AddValidationErrors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Program).Assembly));
builder.Services.AddJwtAuthentication(key, jwtIssuer, jwtAudience);
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseGlobalExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();

#pragma warning disable S1118 // Exposed for WebApplicationFactory<Program> in integration tests.
public partial class Program { }
#pragma warning restore S1118
