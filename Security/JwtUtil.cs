using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace iPortal.Security
{
    public class JwtUtil
    {
        private readonly string _secretKey;
        private readonly long _validityInMilliseconds = 3600000; // 1 hour

        public JwtUtil(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:SecretKey"]
                ?? throw new ArgumentNullException(nameof(configuration), "Jwt:SecretKey is not configured.");
        }

        public string GenerateToken(ClaimsPrincipal authentication)
        {
            if (authentication == null || authentication.Identity == null || string.IsNullOrEmpty(authentication.Identity.Name))
            {
                throw new ArgumentException("Authentication principal is invalid or lacks a username.");
            }

            string username = authentication.Identity.Name;
            var now = DateTime.UtcNow;
            var validity = now.AddMilliseconds(_validityInMilliseconds);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
                IssuedAt = now,
                Expires = validity,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string ExtractUsername(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token cannot be null or empty.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var nameClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);
            if (nameClaim == null)
            {
                throw new InvalidOperationException("Token does not contain a username claim.");
            }
            return nameClaim.Value;
        }

        public bool IsTokenExpired(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                return jwtToken.ValidTo < DateTime.UtcNow;
            }
            catch
            {
                return true;
            }
        }

        public bool ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out _);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}