using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using nebula.api.src.Entities;
using nebula.api.src.Repositories;
using nebula.api.src.Services;

namespace nebula.api.src.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<TokenService>();
            services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();

            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGameService, GameService>();

            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewService, ReviewService>();

            services.AddScoped<IWishlistService, WishlistService>();
            services.AddScoped<ILibraryService, LibraryService>();

            services.AddScoped<IFriendshipService, FriendshipService>();
            services.AddScoped<IMessageService, MessageService>();

            return services;
        }

        public static IMvcBuilder AddValidationErrors(this IMvcBuilder builder)
        {
            return builder.ConfigureApiBehaviorOptions(options =>
                options.InvalidModelStateResponseFactory = BuildValidationErrorResponse);
        }

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Nebula API",
                    Version = "v1",
                    Description = "Autenticação via cookie HttpOnly. Faça POST em /api/auth/login e o cookie será enviado automaticamente nas requisições seguintes."
                }));

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            byte[] key,
            string issuer,
            string audience)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Cookies.TryGetValue("NebulaAuthToken", out var cookieToken))
                                context.Token = cookieToken;

                            return Task.CompletedTask;
                        }
                    };

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            return services;
        }

        private static IActionResult BuildValidationErrorResponse(ActionContext context)
        {
            var errors = context.ModelState
                .Where(kv => kv.Value?.Errors.Count > 0)
                .ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value!.Errors
                        .Select(e => ResolveErrorMessage(e, kv.Key))
                        .ToArray()
                );

            return new BadRequestObjectResult(new { message = "Dados inválidos.", errors });
        }

        private static string ResolveErrorMessage(ModelError error, string field)
        {
            if (!string.IsNullOrWhiteSpace(error.ErrorMessage))
                return error.ErrorMessage;

            if (error.Exception is not null)
                return $"O valor informado para '{field}' é inválido.";

            return $"O campo '{field}' é obrigatório.";
        }
    }
}
