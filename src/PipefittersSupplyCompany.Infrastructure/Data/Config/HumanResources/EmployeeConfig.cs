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
            entity.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("EmployeeId");
            entity.Property(p => p.SupervisorId).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("SupervisorId").IsRequired();
            entity.Property(p => p.LastName).HasColumnType("NVARCHAR(25)").HasColumnName("LastName").IsRequired();
            entity.Property(p => p.FirstName).HasColumnType("NVARCHAR(25)").HasColumnName("FirstName").IsRequired();
            entity.Property(p => p.MiddleInitial).HasColumnType("NCHAR(1)").HasColumnName("MiddleInitial");
            entity.Property(p => p.SSN).HasColumnType("NVARCHAR(9)").HasColumnName("SSN").IsRequired();
            entity.Property(p => p.AddressLine1).HasColumnType("NVARCHAR(30)").HasColumnName("AddressLine1").IsRequired();
            entity.Property(p => p.AddressLine2).HasColumnType("NVARCHAR(30)").HasColumnName("AddressLine2");
            entity.Property(p => p.City).HasColumnType("NVARCHAR(30)").HasColumnName("City").IsRequired();
            entity.Property(p => p.StateProvinceCode).HasColumnType("NCHAR(2)").HasColumnName("StateCode").IsRequired();
            entity.Property(p => p.Zipcode).HasColumnType("NVARCHAR(10)").HasColumnName("ZipCode").IsRequired();
            entity.Property(p => p.Telephone).HasColumnType("NVARCHAR(14)").HasColumnName("Telephone").IsRequired();
            entity.Property(p => p.MaritalStatus).HasColumnType("NCHAR(1)").HasColumnName("MaritalStatus").IsRequired();
            entity.Property(p => p.TaxExemption).HasColumnType("int").HasColumnName("Exemptions").IsRequired();
            entity.Property(p => p.PayRate).HasColumnType("DECIMAL(18,2)").HasColumnName("PayRate").IsRequired();
            entity.Property(p => p.StartDate).HasColumnType("datetime2(0)").HasColumnName("StartDate").IsRequired();
            entity.Property(p => p.IsActive).HasColumnType("BIT").HasColumnName("IsActive").IsRequired();
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime2(7)")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("sysdatetime()");
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime2(7)");
        }
    }
}