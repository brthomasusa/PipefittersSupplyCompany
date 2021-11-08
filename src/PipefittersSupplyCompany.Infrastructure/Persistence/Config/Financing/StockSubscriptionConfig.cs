using PipefittersSupplyCompany.Core.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PipefittersSupplyCompany.Core.Financing.StockSubscriptionAggregate;

namespace PipefittersSupplyCompany.Infrastructure.Persistence.Config.Financing
{
    internal class StockSubscriptionConfig : IEntityTypeConfiguration<StockSubscription>
    {
        public void Configure(EntityTypeBuilder<StockSubscription> entity)
        {
            entity.ToTable("StockSubscriptions", schema: "Finance");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("StockId");
            entity.HasOne(p => p.EconomicEvent).WithOne().HasForeignKey<StockSubscription>(p => p.Id);
            entity.Property(p => p.FinancierId).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("FinancierId");
            entity.Property(p => p.SharesIssured)
                .HasConversion(p => p.Value, p => SharesIssured.Create(p))
                .HasColumnType("INT")
                .HasColumnName("SharesIssured")
                .IsRequired();
            entity.Property(p => p.PricePerShare)
                .HasConversion(p => p.Value, p => PricePerShare.Create(p))
                .HasColumnType("DECIMAL(18,2)")
                .HasColumnName("PricePerShare")
                .IsRequired();
            entity.Property(p => p.StockIssueDate)
                .HasConversion(p => p.Value, p => StockIssueDate.Create(p))
                .HasColumnType("DATETIME2(0)")
                .HasColumnName("StockIssueDate")
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