using Employee.Portal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Employee.Portal.Repo.EntitiesConfiguration
{
    public class EmployeeConfig : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder.Property(p => p.FirstName).HasMaxLength(100);
            builder.Property(p => p.LastName).HasMaxLength(100);
            builder.Property(p => p.Phone).HasMaxLength(15);
        }
    }
}
