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
            entity.HasOne<Employee>(e => e.Supervisor).WithMany().HasForeignKey("SupervisorID").IsRequired();
        }
    }
}