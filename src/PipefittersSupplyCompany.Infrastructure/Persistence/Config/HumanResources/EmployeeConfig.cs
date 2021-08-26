using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PipefittersSupplyCompany.Infrastructure.Persistence.Config.HumanResources
{
    internal class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.ToTable("Employees", schema: "HumanResources");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("EmployeeId");
            entity.Property(p => p.SupervisorId)
                .HasConversion(p => p.Value, p => SupervisorId.Create(p))
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasColumnName("SupervisorId")
                .IsRequired();
            entity.OwnsOne(p => p.EmployeeName, p =>
            {
                p.Property(pp => pp.LastName).HasColumnType("NVARCHAR(25)").HasColumnName("LastName").IsRequired();
                p.Property(pp => pp.FirstName).HasColumnType("NVARCHAR(25)").HasColumnName("FirstName").IsRequired();
                p.Property(pp => pp.MiddleInitial).HasColumnType("NCHAR(1)").HasColumnName("MiddleInitial");
            });

            entity.Property(p => p.SSN)
                .HasConversion(p => p.Value, p => SSN.Create(p))
                .HasColumnType("NVARCHAR(9)")
                .HasColumnName("SSN")
                .IsRequired();
            entity.Property(p => p.Telephone)
                .HasConversion(p => p.Value, p => PhoneNumber.Create(p))
                .HasColumnType("NVARCHAR(14)")
                .HasColumnName("Telephone")
                .IsRequired();
            entity.Property(p => p.MaritalStatus)
                .HasConversion(p => p.Value, p => MaritalStatus.Create(p))
                .HasColumnType("NCHAR(1)")
                .HasColumnName("MaritalStatus")
                .IsRequired();
            entity.Property(p => p.TaxExemption)
                .HasConversion(p => p.Value, p => TaxExemption.Create(p))
                .HasColumnType("int")
                .HasColumnName("Exemptions")
                .IsRequired();
            entity.Property(p => p.PayRate)
                .HasConversion(p => p.Value, p => PayRate.Create(p))
                .HasColumnType("DECIMAL(18,2)")
                .HasColumnName("PayRate")
                .IsRequired();
            entity.Property(p => p.StartDate)
                .HasConversion(p => p.Value, p => StartDate.Create(p))
                .HasColumnType("datetime2(0)")
                .HasColumnName("StartDate")
                .IsRequired();
            entity.Property(p => p.IsActive)
                .HasConversion(p => p.Value, p => IsActive.Create(p))
                .HasColumnType("BIT")
                .HasColumnName("IsActive")
                .IsRequired();
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime2(7)")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("sysdatetime()");
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime2(7)");
        }
    }
}