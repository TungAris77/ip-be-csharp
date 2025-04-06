using iPortal.Security;

namespace iPortal.Services.Interfaces
{
    public interface IUserDetailsService
    {
        UserDetails LoadUserByUsername(string username);
    }
}