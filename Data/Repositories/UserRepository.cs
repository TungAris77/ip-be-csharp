using iPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace iPortal.Data.Repositories
{
    public class UserRepository
    {
        private readonly IPortalDbContext _context;

        public UserRepository(IPortalDbContext context)
        {
            _context = context;
        }

        public User? FindByUsername(string username)
        {
            return _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Username == username);
        }

        public List<User> FindByRoleId(int roleId)
        {
            return _context.Users.Where(u => u.RoleId == roleId).ToList();
        }

        public User Save(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}