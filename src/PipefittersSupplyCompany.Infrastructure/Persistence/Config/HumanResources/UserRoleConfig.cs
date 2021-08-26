using PipefittersSupplyCompany.Core.HumanResources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PipefittersSupplyCompany.Infrastructure.Persistence.Config.HumanResources
{
    internal class UserRoleConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> entity)
        {
            entity.ToTable("UserRoles", schema: "HumanResources");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasColumnName("UserRoleId");
            // entity.HasOne(p => p.User).WithMany(p => p.RoleLink);
            // entity.HasOne(p => p.Role).WithMany(p => p.UserLink);
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime2(7)")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("sysdatetime()");
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime2(7)");
        }
    }
}