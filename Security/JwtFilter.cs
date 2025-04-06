using iPortal.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using System.Threading.Tasks;

namespace iPortal.Security
{
    public class JwtFilter
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _scopeFactory;

        public JwtFilter(RequestDelegate next, IServiceScopeFactory scopeFactory)
        {
            _next = next;
            _scopeFactory = scopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string? token = GetTokenFromRequest(context.Request);

            if (!string.IsNullOrEmpty(token))
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var jwtUtil = scope.ServiceProvider.GetRequiredService<JwtUtil>();
                    var userDetailsService = scope.ServiceProvider.GetRequiredService<IUserDetailsService>();

                    if (jwtUtil.ValidateToken(token))
                    {
                        string username = jwtUtil.ExtractUsername(token);
                        var userDetails = userDetailsService.LoadUserByUsername(username);

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, username),
                            new Claim(ClaimTypes.Role, userDetails.Authorities.First().Authority)
                        };
                        var identity = new ClaimsIdentity(claims, "jwt");
                        context.User = new ClaimsPrincipal(identity);
                    }
                }
            }

            await _next(context);
        }

        private string? GetTokenFromRequest(HttpRequest request)
        {
            request.Headers.TryGetValue("Authorization", out StringValues header);
            if (!string.IsNullOrEmpty(header) && header.ToString().StartsWith("Bearer "))
            {
                return header.ToString().Substring(7);
            }
            return null;
        }
    }
}