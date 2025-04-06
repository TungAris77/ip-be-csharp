using iPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace iPortal.Data.Repositories
{
    public class RoleRepository
    {
        private readonly IPortalDbContext _context;

        public RoleRepository(IPortalDbContext context)
        {
            _context = context;
        }

        public Role? FindByRoleName(string roleName)
        {
            return _context.Roles.FirstOrDefault(r => r.RoleName == roleName);
        }
    }
}