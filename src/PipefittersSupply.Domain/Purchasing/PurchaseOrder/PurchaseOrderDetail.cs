using System;
using PipefittersSupply.Domain.Common;
using PipefittersSupply.Domain.Purchasing.Inventory;
using PipefittersSupply.Framework;
namespace PipefittersSupply.Domain.Purchasing.PurchaseOrder
{
    public class PurchaseOrderDetail : Entity<PurchaseOrderDetailId>
    {
        public PurchaseOrderDetail() { }

        public PurchaseOrderDetailId Id { get; private set; }

        public PurchaseOrderId PurchaseOrderId { get; private set; }

        public InventoryId InventoryId { get; private set; }

        public VendorPartNumber VendorPartNumber { get; private set; }

        public Quantity QuantityOrdered { get; private set; }

        public UnitCost UnitCost { get; private set; }

        public CreatedDate CreatedDate { get; private set; }

        public LastModifiedDate LastModifiedDate { get; private set; }


        protected override void EnsureValidState()
        {
            var valid = Id != null &&
                PurchaseOrderId != null &&
                InventoryId != null;

            if (!valid)
            {
                throw new InvalidEntityStateException(this, "Post-checks failed!");
            }
        }

        protected override void When(object @event)
        {

        }
    }
}