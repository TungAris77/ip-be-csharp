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
                ?? throw new UsernameNotFoundException("Username not exists"); // Sử dụng exception đã định nghĩa

            if (user.Status == "RESIGNED")
            {
                throw new AuthenticationException("Tài khoản của bạn đã bị vô hiệu hóa.");
            }

            var authority = new SimpleGrantedAuthority(user.Role.RoleName);

            return new UserDetails(
                username,
                user.Password,
                new List<SimpleGrantedAuthority> { authority }
            );
        }
    }

    // Định nghĩa UsernameNotFoundException trong cùng file
    public class UsernameNotFoundException : Exception
    {
        public UsernameNotFoundException(string message) : base(message) { }
    }
}