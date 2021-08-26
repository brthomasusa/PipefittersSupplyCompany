using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PipefittersSupplyCompany.Infrastructure.Persistence.Config.HumanResources
{
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users", schema: "HumanResources");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("UserId");
            entity.Property(p => p.UserName).HasColumnType("NVARCHAR(256)").HasColumnName("UserName").IsRequired();
            entity.Property(p => p.Email).HasColumnType("NVARCHAR(256)").HasColumnName("Email").IsRequired();
            entity.HasOne(e => e.Employee);
            // entity.HasMany(e => e.RoleLink).WithMany(e => e.UserLink);
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime2(7)")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("sysdatetime()");
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime2(7)");
        }
    }
}