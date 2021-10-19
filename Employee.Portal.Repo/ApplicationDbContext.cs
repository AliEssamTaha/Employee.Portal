using Employee.Portal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee.Portal.Repo
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Domain.Entities.Employee> Employees { get; set; }
        public DbSet<Department> Departmens { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
