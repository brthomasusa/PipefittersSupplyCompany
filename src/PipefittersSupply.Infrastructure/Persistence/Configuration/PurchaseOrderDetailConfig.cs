using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PipefittersSupply.Domain.Purchasing.PurchaseOrder;

namespace PipefittersSupply.Infrastructure.Persistence.Configuration
{
    internal class PurchaseOrderDetailConfig : IEntityTypeConfiguration<PurchaseOrderDetail>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderDetail> entity)
        {
            entity.ToTable("PurchaseOrderDetails", schema: "Purchasing");
            entity.HasKey(e => e.PurchaseOrderDetailId);
            entity.Property(p => p.PurchaseOrderDetailId).ValueGeneratedNever();

            entity.OwnsOne(e => e.Id);
            entity.OwnsOne(e => e.PurchaseOrderId);
            entity.OwnsOne(e => e.InventoryId);
            entity.OwnsOne(e => e.VendorPartNumber);
            entity.OwnsOne(e => e.QuantityOrdered);
            entity.OwnsOne(e => e.UnitCost);
            entity.OwnsOne(e => e.CreatedDate);
            entity.OwnsOne(e => e.LastModifiedDate);
        }
    }
}