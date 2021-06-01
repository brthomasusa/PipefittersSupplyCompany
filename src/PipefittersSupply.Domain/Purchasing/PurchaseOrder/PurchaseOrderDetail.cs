using System;
using PipefittersSupply.Domain.Common;
using PipefittersSupply.Domain.Purchasing.Inventory;
using PipefittersSupply.Domain.Base;
namespace PipefittersSupply.Domain.Purchasing.PurchaseOrder
{
    public class PurchaseOrderDetail : Entity<PurchaseOrderDetailId>
    {
        public int PurchaseOrderDetailId { get; private set; }

        public PurchaseOrderDetail(Action<object> applier) : base(applier) { }

        protected PurchaseOrderDetail() { }

        public PurchaseOrderId PurchaseOrderId { get; private set; }
        internal void UpdatePurchaseOrderId(PurchaseOrderId value) =>
            Apply(new Events.PurchaseOrderDetailPurchaseOrderIdUpdated
            {
                Id = Id,
                PurchaseOrderId = value,
                LastModifiedDate = DateTime.Now
            });

        public InventoryId InventoryId { get; private set; }

        public VendorPartNumber VendorPartNumber { get; private set; }

        public Quantity QuantityOrdered { get; private set; }

        public UnitCost UnitCost { get; private set; }

        public CreatedDate CreatedDate { get; private set; }

        public LastModifiedDate LastModifiedDate { get; private set; }

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
                    PurchaseOrderDetailId = evt.Id;
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