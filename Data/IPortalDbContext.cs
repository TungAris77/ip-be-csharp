using iPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace iPortal.Data
{
    public class IPortalDbContext : DbContext
    {
        public IPortalDbContext(DbContextOptions<IPortalDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.role)
                .WithMany()
                .HasForeignKey(u => u.roleId);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.user)
                .WithOne()
                .HasForeignKey<Student>(s => s.userId);

            modelBuilder.Entity<Employer>()
                .HasOne(e => e.user)
                .WithOne()
                .HasForeignKey<Employer>(e => e.userId);
        }
    }
}