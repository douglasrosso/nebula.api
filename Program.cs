using Microsoft.EntityFrameworkCore;
using nebula.api.src.Data;
using nebula.api.src.Repositories;
using nebula.api.src.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();



//var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
//    ?? throw new InvalidOperationException("CONNECTION_STRING environment variable is not set.");

builder.Services.AddDbContext<NebulaDbContext>(options => options.UseNpgsql("Host=127.0.0.1;Port=5432;Database=nebula;Username=postgres;Password=password"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Program).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger(); // Generates the swagger.json endpoint
    app.UseSwaggerUI(); // Serves the visual Swagger UI
}

app.UseHttpsRedirection();
app.MapControllers();


await app.RunAsync();
