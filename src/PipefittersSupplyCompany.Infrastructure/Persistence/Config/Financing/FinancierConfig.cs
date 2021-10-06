using PipefittersSupplyCompany.Core.Financing.FinancierAggregate;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PipefittersSupplyCompany.Infrastructure.Persistence.Config.Financing
{
    internal class FinancierConfig : IEntityTypeConfiguration<Financier>
    {
        public void Configure(EntityTypeBuilder<Financier> entity)
        {
            entity.ToTable("Financiers", schema: "Finance");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("FinancierId");
            entity.OwnsOne(p => p.FinancierName, p =>
            {
                p.Property(pp => pp.OrgName).HasColumnType("NVARCHAR(50)").HasColumnName("FinancierName").IsRequired();
            });
            entity.Property(p => p.Telephone)
                .HasConversion(p => p.Value, p => PhoneNumber.Create(p))
                .HasColumnType("NVARCHAR(14)")
                .HasColumnName("Telephone")
                .IsRequired();
            entity.Property(p => p.IsActive)
                .HasConversion(p => p.Value, p => IsActive.Create(p))
                .HasColumnType("BIT")
                .HasColumnName("IsActive")
                .IsRequired();
            entity.Property(p => p.UserId).HasColumnName("UserId").IsRequired();
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime2(7)")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("sysdatetime()");
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime2(7)");
        }
    }
}