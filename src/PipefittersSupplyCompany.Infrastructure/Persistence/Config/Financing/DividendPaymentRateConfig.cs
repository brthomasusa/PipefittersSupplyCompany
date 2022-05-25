using PipefittersSupplyCompany.Core.Financing.StockSubscriptionAggregate;
using Microsoft.EntityFrameworkCore;
using PipefittersSupplyCompany.Core.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PipefittersSupplyCompany.Infrastructure.Persistence.Config.Financing
{
    internal class DividendPaymentRateConfig : IEntityTypeConfiguration<DividendPayment>
    {
        public void Configure(EntityTypeBuilder<DividendPayment> entity)
        {
            entity.ToTable("DividendPymtRates", schema: "Finance");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("DividendId");
            entity.HasOne(p => p.EconomicEvent).WithOne().HasForeignKey<DividendPayment>(p => p.Id);
            entity.HasOne(p => p.StockSubscription).WithMany(p => p.DividendPaymentRates).HasForeignKey(p => p.StockId);
            entity.Property(p => p.StockId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasColumnName("StockId")
                .IsRequired();
            entity.Property(p => p.DividendDeclarationDate)
                .HasConversion(p => p.Value, p => DividendDeclarationDate.Create(p))
                .HasColumnType("DATETIME2(0)")
                .HasColumnName("DividendDeclarationDate")
                .IsRequired();
            entity.Property(p => p.DividendPerShare)
                .HasConversion(p => p.Value, p => DividendPerShare.Create(p))
                .HasColumnType("DECIMAL(18,2)")
                .HasColumnName("DividendPerShare")
                .IsRequired();
            entity.Property(p => p.UserId)
                .HasConversion(p => p.Value, p => UserId.Create(p))
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasColumnName("UserId")
                .IsRequired();
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime2(7)")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("sysdatetime()");
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime2(7)");
        }
    }
}