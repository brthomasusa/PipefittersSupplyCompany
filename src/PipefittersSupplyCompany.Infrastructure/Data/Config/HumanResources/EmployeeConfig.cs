using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PipefittersSupplyCompany.Infrastructure.Data.Config.HumanResources
{
    internal class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.ToTable("Employees", schema: "HumanResources");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("EmployeeID");
            entity.HasOne<EmployeeType>(e => e.EmployeeType).WithMany().HasForeignKey("EmployeeTypeID").IsRequired();
            entity.Property(p => p.SupervisorId).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("SupervisorID");
            entity.OwnsOne(p => p.Name, p =>
            {
                p.Property(pp => pp.FirstName).HasColumnName("FirstName");
                p.Property(pp => pp.LastName).HasColumnName("LastName");
                p.Property(pp => pp.MiddleInitial).HasColumnName("MiddleInitial");
            });
        }
    }
}