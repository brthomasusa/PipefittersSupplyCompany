using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PipefittersSupply.Domain.Purchasing.PurchaseOrder;

namespace PipefittersSupply.Infrastructure.Configuration
{
    internal class PurchaseOrderConfig : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> entity)
        {
            entity.ToTable("PurchaseOrders", schema: "Purchasing");
            entity.HasKey(e => e.PurchaseOrderId);
            entity.OwnsOne(e => e.Id);
            entity.OwnsOne(e => e.VendorId);
            entity.OwnsOne(e => e.EmployeeId);
            entity.OwnsOne(e => e.PurchaseOrderDate);
            entity.OwnsOne(e => e.ExpectedDeliveryDate);
            entity.OwnsOne(e => e.PurchaseOrderAmount);
            entity.OwnsOne(e => e.CreatedDate);
            entity.OwnsOne(e => e.LastModifiedDate);
        }
    }
}