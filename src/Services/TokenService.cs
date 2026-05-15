using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace nebula.api.src.Services
{
    public class TokenService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public TokenService(IHttpContextAccessor httpContextAccessor)
        {
            _key = Environment.GetEnvironmentVariable("JWT_KEY")!;
            _issuer = Environment.GetEnvironmentVariable("JWT_ISSUER")!;
            _audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")!;
            _httpContextAccessor = httpContextAccessor;
        }

        public void GenerateToken(string userId, string email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email)
            };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            this.DefinirTokenCookie(token.ToString());
        }

        public void DefinirTokenCookie(string token)
        {
            var context = _httpContextAccessor.HttpContext;

            if (context != null)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(1)
                };

                context.Response.Cookies.Append("AuthToken", token, cookieOptions);
            }
        }
    }
}
