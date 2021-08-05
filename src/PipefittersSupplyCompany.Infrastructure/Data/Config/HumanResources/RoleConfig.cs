using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PipefittersSupplyCompany.Core.HumanResources;

namespace PipefittersSupplyCompany.Infrastructure.Data.Config.HumanResources
{
    internal class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.ToTable("Roles", schema: "HumanResources");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("RoleId");
            entity.Property(p => p.RoleName).HasColumnType("NVARCHAR(256)").HasColumnName("RoleId").IsRequired();
        }
    }
}