using iPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace iPortal.Data.Repositories
{
    public class TokenRepository
    {
        private readonly IPortalDbContext _context;

        public TokenRepository(IPortalDbContext context)
        {
            _context = context;
        }

        public void DeleteByUsername(string username)
        {
            var tokens = _context.Tokens.Where(t => t.Username == username);
            _context.Tokens.RemoveRange(tokens);
            _context.SaveChanges();
        }
    }
}