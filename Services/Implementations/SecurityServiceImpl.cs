using iPortal.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using iPortal.Data.Repositories;
using System.Security.Claims;

namespace iPortal.Services.Implementations
{
    public class SecurityServiceImpl : ISecurityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserRepository _userRepository; // Khai báo trường

        public SecurityServiceImpl(IHttpContextAccessor httpContextAccessor, UserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository; // Khởi tạo qua DI
        }

        public string? GetCurrentUsername()
        {
            var authentication = _httpContextAccessor.HttpContext?.User;
            if (authentication != null && authentication.Identity?.IsAuthenticated == true)
            {
                return authentication.Identity.Name;
            }
            return null;
        }

        public bool HasRole(string roleName)
        {
            var username = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Username not found in token.");
                return false;
            }

            var user = _userRepository.FindByUsername(username);
            if (user == null)
            {
                Console.WriteLine($"User '{username}' not found in DB.");
                return false;
            }

            if (user.role == null)
            {
                Console.WriteLine($"Role not loaded for user '{username}'.");
                return false;
            }

            Console.WriteLine($"Checking role: {user.role.roleName} against {roleName}");
            return user.role.roleName == roleName;
        }
    }
}