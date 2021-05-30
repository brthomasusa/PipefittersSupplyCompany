using System;
using PipefittersSupply.Domain.Common;
using PipefittersSupply.Domain.Purchasing.Inventory;
using PipefittersSupply.Domain.Base;
namespace PipefittersSupply.Domain.Purchasing.PurchaseOrder
{
    public class PurchaseOrderDetail : Entity<PurchaseOrderDetailId>
    {
        public PurchaseOrderDetail(Action<object> applier) : base(applier) { }

        internal PurchaseOrderId PurchaseOrderId { get; set; }
        internal void UpdatePurchaseOrderId(PurchaseOrderId value) =>
            Apply(new Events.PurchaseOrderDetailPurchaseOrderIdUpdated
            {
                Id = Id,
                PurchaseOrderId = value,
                LastModifiedDate = DateTime.Now
            });

        internal InventoryId InventoryId { get; set; }

        internal VendorPartNumber VendorPartNumber { get; set; }

        internal Quantity QuantityOrdered { get; set; }

        internal UnitCost UnitCost { get; set; }

        internal CreatedDate CreatedDate { get; set; }

        internal LastModifiedDate LastModifiedDate { get; set; }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.PurchaseOrderDetailAddedToPurchaseOrder evt:
                    Id = new PurchaseOrderDetailId(evt.Id);
                    PurchaseOrderId = new PurchaseOrderId(evt.PurchaseOrderId);
                    InventoryId = new InventoryId(evt.InventoryId);
                    QuantityOrdered = Quantity.FromInterger(evt.QuantityOrdered);
                    UnitCost = UnitCost.FromDecimal(evt.UnitCost);
                    CreatedDate = new CreatedDate(DateTime.Now);
                    this.EnsureValidState();
                    break;
                case Events.PurchaseOrderDetailPurchaseOrderIdUpdated evt:
                    PurchaseOrderId = new PurchaseOrderId(evt.PurchaseOrderId);
                    LastModifiedDate = new LastModifiedDate(evt.LastModifiedDate);
                    this.EnsureValidState();
                    break;
            }
        }
    }
}