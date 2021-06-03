using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PipefittersSupply.Domain.HumanResources.Employees;
namespace PipefittersSupply.Infrastructure.Configuration
{
    internal class EmployeeTypeConfig : IEntityTypeConfiguration<EmployeeType>
    {
        public void Configure(EntityTypeBuilder<EmployeeType> entity)
        {
            entity.ToTable("EmployeeTypes", schema: "HumanResources");
            entity.HasKey(e => e.EmployeeTypeId);
            entity.Property(p => p.EmployeeTypeId).ValueGeneratedNever();
            entity.OwnsOne(e => e.Id).Property(p => p.Value).HasColumnType("int").IsRequired();
            entity.OwnsOne(e => e.EmployeeTypeName).Property(p => p.Value).HasColumnType("nvarchar(25)").HasColumnName("EmployeeTypeName").IsRequired();
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

            // entity.HasData(
            //     new EmployeeType(EmployeeTypeIdentifier.FromInterger(1), EmployeeTypeName.FromString("Accountant")),
            //     new EmployeeType(EmployeeTypeIdentifier.FromInterger(2), EmployeeTypeName.FromString("Administrator")),
            //     new EmployeeType(EmployeeTypeIdentifier.FromInterger(3), EmployeeTypeName.FromString("Maintenance")),
            //     new EmployeeType(EmployeeTypeIdentifier.FromInterger(4), EmployeeTypeName.FromString("Materials Handler")),
            //     new EmployeeType(EmployeeTypeIdentifier.FromInterger(5), EmployeeTypeName.FromString("Purchasing Agent")),
            //     new EmployeeType(EmployeeTypeIdentifier.FromInterger(6), EmployeeTypeName.FromString("Salesperson"))
            // );
        }
    }
}