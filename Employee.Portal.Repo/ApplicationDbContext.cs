using Employee.Portal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Employee.Portal.Repo
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            #region Entities Configuration
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            #endregion
        }
    }
}
