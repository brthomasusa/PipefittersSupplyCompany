using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PipefittersSupply.Domain.HumanResources.Employees;

namespace PipefittersSupply.Infrastructure.Configuration
{
    internal class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.ToTable("Employees", schema: "HumanResources");
            entity.HasKey(e => e.EmployeeId);

            entity.OwnsOne(e => e.Id).Property(p => p.Value).HasColumnType("int").IsRequired();
            entity.OwnsOne(e => e.EmployeeType).Property(p => p.Value).HasColumnType("int").HasColumnName("EmployeeType").IsRequired();
            entity.OwnsOne(e => e.SupervisorId).Property(p => p.Value).HasColumnName("SupervisorId").IsRequired();
            entity.OwnsOne(e => e.LastName).Property(p => p.Value).HasColumnType("nvarchar(25)").HasColumnName("LastName").IsRequired();
            entity.OwnsOne(e => e.FirstName).Property(p => p.Value).HasColumnType("nvarchar(25)").HasColumnName("FirstName").IsRequired();
            entity.OwnsOne(e => e.MiddleInitial).Property(p => p.Value).HasColumnType("nchar(1)").HasColumnName("MiddleInitial").IsRequired();
            entity.OwnsOne(e => e.SSN).Property(p => p.Value).HasColumnType("nvarchar(11)").HasColumnName("SSN").IsRequired();
            entity.OwnsOne(e => e.AddressLine1).Property(p => p.Value).HasColumnType("nvarchar(30)").HasColumnName("AddressLine1").IsRequired();
            entity.OwnsOne(e => e.AddressLine2).Property(p => p.Value).HasColumnType("nvarchar(30)").HasColumnName("AddressLine2").IsRequired();
            entity.OwnsOne(e => e.City).Property(p => p.Value).HasColumnType("nvarchar(30)").HasColumnName("City").IsRequired();
            entity.OwnsOne(e => e.State).Property(p => p.Value).HasColumnType("nchar(2)").HasColumnName("StateProvinceCode").IsRequired();
            entity.OwnsOne(e => e.Zipcode).Property(p => p.Value).HasColumnType("nvarchar(12)").HasColumnName("Zipcode").IsRequired();
            entity.OwnsOne(e => e.Telephone).Property(p => p.Value).HasColumnType("nvarchar(14)").HasColumnName("Telephone").IsRequired();
            entity.OwnsOne(e => e.MaritalStatus).Property(p => p.Value).HasColumnType("nchar(1)").HasColumnName("MaritalStatus").IsRequired();
            entity.OwnsOne(e => e.Exemptions).Property(p => p.Value).HasColumnType("int").HasColumnName("TaxExemptions").IsRequired();
            entity.OwnsOne(e => e.PayRate).Property(p => p.Value).HasColumnType("decimal(18,2)").HasColumnName("PayRate").IsRequired();
            entity.OwnsOne(e => e.StartDate).Property(p => p.Value).HasColumnType("date").HasColumnName("StartDate").IsRequired();
            entity.OwnsOne(e => e.IsActive).Property(p => p.Value).HasColumnType("bit").HasColumnName("IsActive").IsRequired();
            entity.OwnsOne(e => e.CreatedDate)
                .Property(p => p.Value)
                .HasColumnType("datetime2(7)")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("sysdatetime()")
                .HasColumnName("CreatedDate")
                .IsRequired();
            entity.OwnsOne(e => e.LastModifiedDate)
            .Property(p => p.Value)
            .HasColumnType("datetime2(7)")
            .HasColumnName("LastModifiedDate");
        }
    }
}