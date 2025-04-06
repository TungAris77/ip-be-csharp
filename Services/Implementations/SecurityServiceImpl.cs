using iPortal.Services.Interfaces;
using System.Security.Claims;

namespace iPortal.Services.Implementations
{
    public class SecurityServiceImpl : ISecurityService
    {
        public string GetCurrentUsername()
        {
            var authentication = ClaimsPrincipal.Current;
            if (authentication != null && authentication.Identity.IsAuthenticated)
            {
                return authentication.Identity.Name;
            }
            return null;
        }

        public bool HasRole(string role)
        {
            var authentication = ClaimsPrincipal.Current;
            if (authentication != null && authentication.Claims != null)
            {
                return authentication.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == role);
            }
            return false;
        }
    }
}