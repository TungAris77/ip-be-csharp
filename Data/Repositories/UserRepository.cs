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
                .Include(u => u.role) // Đổi từ Role
                .FirstOrDefault(u => u.username == username); // Đổi từ Username
        }

        public List<User> FindByRoleId(long roleId)
        {
            return _context.Users.Where(u => u.roleId == roleId).ToList(); // Đổi từ RoleId
        }

        public User Save(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}