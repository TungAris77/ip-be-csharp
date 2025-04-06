using iPortal.Data.Repositories;
using iPortal.Services.Interfaces;
using System.Security.Authentication;

namespace iPortal.Security
{
    public class CustomUserDetailsService : IUserDetailsService
    {
        private readonly UserRepository _userRepository;

        public CustomUserDetailsService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDetails LoadUserByUsername(string username)
        {
            var user = _userRepository.FindByUsername(username)
                ?? throw new UsernameNotFoundException("Username not exists");

            if (user.status == "RESIGNED") // Đổi từ Status
            {
                throw new AuthenticationException("Tài khoản của bạn đã bị vô hiệu hóa.");
            }

            var authority = new SimpleGrantedAuthority(user.role.roleName); // Đổi từ Role.RoleName

            return new UserDetails(
                user.username, // Đổi từ Username
                user.password, // Đổi từ Password
                new List<SimpleGrantedAuthority> { authority }
            );
        }
    }

    public class UsernameNotFoundException : Exception
    {
        public UsernameNotFoundException(string message) : base(message) { }
    }

    public class UserDetails
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<SimpleGrantedAuthority> Authorities { get; set; }

        public UserDetails(string username, string password, List<SimpleGrantedAuthority> authorities)
        {
            Username = username;
            Password = password;
            Authorities = authorities;
        }
    }

    public class SimpleGrantedAuthority
    {
        public string Authority { get; set; }

        public SimpleGrantedAuthority(string authority)
        {
            Authority = authority;
        }
    }
}