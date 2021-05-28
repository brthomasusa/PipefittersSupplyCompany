using System;
using PipefittersSupply.Domain.Common;
using PipefittersSupply.Domain.Purchasing.Inventory;
using PipefittersSupply.Framework;
namespace PipefittersSupply.Domain.Purchasing.PurchaseOrder
{
    public class PurchaseOrderDetail : Entity<PurchaseOrderDetailId>
    {
        public PurchaseOrderDetail(Action<object> applier)
            : base(applier) { }

        internal PurchaseOrderId PurchaseOrderId { get; set; }

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
                    break;
                    // case Events.OvertimeHoursUpdated evt:
                    //     OvertimeHours = new OvertimeHours(evt.OvertimeHours);
                    //     LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    //     break;
            }
        }

        // protected override void EnsureValidState()
        // {
        //     var valid = Id != null &&
        //         PurchaseOrderId != null &&
        //         InventoryId != null;

        //     if (!valid)
        //     {
        //         throw new InvalidEntityStateException(this, "Post-checks failed!");
        //     }
        // }


    }
}