using iPortal.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using System.Threading.Tasks;

namespace iPortal.Security
{
    public class JwtFilter
    {
        private readonly RequestDelegate _next;
        private readonly IUserDetailsService _userDetailsService;
        private readonly JwtUtil _jwtUtil;

        public JwtFilter(RequestDelegate next, IUserDetailsService userDetailsService, JwtUtil jwtUtil)
        {
            _next = next;
            _userDetailsService = userDetailsService;
            _jwtUtil = jwtUtil;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string token = GetTokenFromRequest(context.Request);

            if (!string.IsNullOrEmpty(token) && _jwtUtil.ValidateToken(token))
            {
                string username = _jwtUtil.ExtractUsername(token);
                var userDetails = _userDetailsService.LoadUserByUsername(username);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, userDetails.Authorities.First().Authority)
                };
                var identity = new ClaimsIdentity(claims, "jwt");
                context.User = new ClaimsPrincipal(identity);
            }

            await _next(context);
        }

        private string GetTokenFromRequest(HttpRequest request)
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