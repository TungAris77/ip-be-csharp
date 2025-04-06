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
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.User)
                .WithOne()
                .HasForeignKey<Student>(s => s.UserId);

            modelBuilder.Entity<Employer>()
                .HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Employer>(e => e.UserId);
        }
    }
}