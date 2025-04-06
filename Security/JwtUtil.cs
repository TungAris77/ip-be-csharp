using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace iPortal.Security
{
    public class JwtUtil
    {
        private readonly string _secretKey;
        private readonly long _validityInMilliseconds = 3600000; // 1 hour, giống Java

        public JwtUtil(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:SecretKey"]
                ?? throw new ArgumentNullException(nameof(configuration), "Jwt:SecretKey is not configured.");
        }

        // Generate token giống Java: nhận username trực tiếp thay vì ClaimsPrincipal
        public string GenerateToken(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username cannot be null or empty.");
            }

            var now = DateTime.UtcNow;
            var validity = now.AddMilliseconds(_validityInMilliseconds);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("sub", username) }),
                IssuedAt = now,
                Expires = validity,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
                    SecurityAlgorithms.HmacSha512) // Đồng bộ với Java (HS512)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Extract username giống Java: lấy "sub"
        public string ExtractUsername(string token)
        {
            var claims = GetClaimsFromToken(token);
            var username = claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            if (username == null)
            {
                throw new InvalidOperationException("Token does not contain a subject claim.");
            }
            return username;
        }

        // Validate token giống Java: ném ngoại lệ nếu không hợp lệ
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
                }, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException("Invalid token", ex);
            }
        }

        // Kiểm tra token hết hạn giống Java
        public bool IsTokenExpired(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                return jwtToken.ValidTo < DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException("Error parsing token expiration", ex);
            }
        }

        // Hàm phụ để lấy claims, giống Java
        private List<Claim> GetClaimsFromToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                return jwtToken.Claims.ToList();
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException("Error parsing token claims", ex);
            }
        }
    }
}