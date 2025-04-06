using iPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace iPortal.Data.Repositories
{
    public class StudentRepository
    {
        private readonly IPortalDbContext _context;

        public StudentRepository(IPortalDbContext context)
        {
            _context = context;
        }

        public Student Save(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return student;
        }
    }
}