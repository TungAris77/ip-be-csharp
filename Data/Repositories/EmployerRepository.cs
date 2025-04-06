using iPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace iPortal.Data.Repositories
{
    public class EmployerRepository
    {
        private readonly IPortalDbContext _context;

        public EmployerRepository(IPortalDbContext context)
        {
            _context = context;
        }

        public Employer Save(Employer employer)
        {
            _context.Employers.Add(employer);
            _context.SaveChanges();
            return employer;
        }
    }
}