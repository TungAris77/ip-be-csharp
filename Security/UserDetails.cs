namespace iPortal.Security
{
    public class UserDetails
    {
        public string Username { get; }
        public string Password { get; }
        public IEnumerable<SimpleGrantedAuthority> Authorities { get; }
        public bool IsEnabled { get; }

        public UserDetails(string username, string password, IEnumerable<SimpleGrantedAuthority> authorities, bool isEnabled = true)
        {
            Username = username;
            Password = password;
            Authorities = authorities;
            IsEnabled = isEnabled;
        }
    }

    public class SimpleGrantedAuthority
    {
        public string Authority { get; }

        public SimpleGrantedAuthority(string authority)
        {
            Authority = authority;
        }
    }
}