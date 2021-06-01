using System;
using System.Collections.Generic;
using System.Linq;
using PipefittersSupply.Domain.Common;
using PipefittersSupply.Domain.HumanResources.Employees;
using PipefittersSupply.Domain.Purchasing.Inventory;
using PipefittersSupply.Domain.Purchasing.Vendor;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.Purchasing.PurchaseOrder
{
    public class PurchaseOrder : AggregateRoot<PurchaseOrderId>
    {
        private int _nextItemNumber = 0;

        public List<PurchaseOrderDetail> PurchaseOrderDetails { get; private set; } = new List<PurchaseOrderDetail>();

        public int PurchaseOrderId { get; private set; }

        protected PurchaseOrder() { }

        public PurchaseOrder
        (
            PurchaseOrderId id,
            VendorId vendorId,
            EmployeeId employeeId,
            PurchaseOrderDate orderDate,
            ExpectedDeliveryDate deliveryDate,
            PurchaseOrderAmount amount
        ) =>
            Apply(new Events.PurchaseOrderCreated
            {
                Id = id,
                VendorId = vendorId,
                EmployeeId = employeeId,
                PurchaseOrderDate = orderDate,
                ExpectedDeliveryDate = deliveryDate,
                PurchaseOrderAmount = amount,
                CreatedDate = DateTime.Now
            });

        private string DbId
        {
            get => $"Employee/{Id}";
            set { }
        }

        public VendorId VendorId { get; private set; }
        public void UpdateVendorId(VendorId value) =>
            Apply(new Events.VendorIdUpdated
            {
                Id = Id,
                VendorId = value,
                LastModifiedDate = DateTime.Now
            });

        public EmployeeId EmployeeId { get; private set; }
        public void UpdateEmployeeId(EmployeeId value) =>
            Apply(new Events.EmployeeIdUpdated
            {
                Id = Id,
                EmployeeId = value,
                LastModifiedDate = DateTime.Now
            });

        public PurchaseOrderDate PurchaseOrderDate { get; private set; }
        public void UpdatePurchaseOrderDate(PurchaseOrderDate value) =>
            Apply(new Events.PurchaseOrderDateUpdated
            {
                Id = Id,
                PurchaseOrderDate = value,
                LastModifiedDate = DateTime.Now
            });

        public ExpectedDeliveryDate ExpectedDeliveryDate { get; private set; }
        public void UpdateExpectedDeliveryDate(ExpectedDeliveryDate value) =>
            Apply(new Events.ExpectedDeliveryDateUpdated
            {
                Id = Id,
                ExpectedDeliveryDate = value,
                LastModifiedDate = DateTime.Now
            });

        public PurchaseOrderAmount PurchaseOrderAmount { get; private set; }
        public void UpdatePurchaseOrderAmount(PurchaseOrderAmount value) =>
            Apply(new Events.PurchaseOrderAmountUpdated
            {
                Id = Id,
                PurchaseOrderAmount = value,
                LastModifiedDate = DateTime.Now
            });

        public CreatedDate CreatedDate { get; private set; }

        public LastModifiedDate LastModifiedDate { get; private set; }

        // PurchaseOrderDetail property update methods

        public void AddPurchaseOrderDetail
        (
            InventoryId inventoryId,
            VendorPartNumber partNumber,
            Quantity quantity,
            UnitCost unitCost
        ) =>
            Apply(new Events.PurchaseOrderDetailAddedToPurchaseOrder
            {
                Id = ++_nextItemNumber,
                PurchaseOrderId = Id,
                InventoryId = inventoryId,
                VendorPartNumber = partNumber,
                QuantityOrdered = quantity,
                UnitCost = unitCost,
                CreatedDate = DateTime.Now
            });

        public void UpdatePoDetailPurchaseOrderId(PurchaseOrderDetailId lineItemID, PurchaseOrderId value)
        {
            var lineItem = FindPurchaseOrderDetail(lineItemID);

            if (lineItem == null)
            {
                throw new InvalidOperationException($"Unable to find PurchaseOrderDetail item with id: {lineItemID}.");
            }

            lineItem.UpdatePurchaseOrderId(value);
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.PurchaseOrderCreated evt:
                    Id = new PurchaseOrderId(evt.Id);
                    VendorId = new VendorId(evt.VendorId);
                    EmployeeId = new EmployeeId(evt.EmployeeId);
                    PurchaseOrderDate = new PurchaseOrderDate(evt.PurchaseOrderDate);
                    ExpectedDeliveryDate = new ExpectedDeliveryDate(evt.ExpectedDeliveryDate);
                    PurchaseOrderAmount = new PurchaseOrderAmount(evt.PurchaseOrderAmount);
                    CreatedDate = new CreatedDate(evt.CreatedDate);
                    PurchaseOrderId = evt.Id;
                    break;
                case Events.VendorIdUpdated evt:
                    VendorId = new VendorId(evt.VendorId);
                    LastModifiedDate = new LastModifiedDate(evt.LastModifiedDate);
                    break;
                case Events.EmployeeIdUpdated evt:
                    EmployeeId = new EmployeeId(evt.EmployeeId);
                    LastModifiedDate = new LastModifiedDate(evt.LastModifiedDate);
                    break;
                case Events.PurchaseOrderDateUpdated evt:
                    PurchaseOrderDate = new PurchaseOrderDate(evt.PurchaseOrderDate);
                    LastModifiedDate = new LastModifiedDate(evt.LastModifiedDate);
                    break;
                case Events.ExpectedDeliveryDateUpdated evt:
                    ExpectedDeliveryDate = new ExpectedDeliveryDate(evt.ExpectedDeliveryDate);
                    LastModifiedDate = new LastModifiedDate(evt.LastModifiedDate);
                    break;
                case Events.PurchaseOrderAmountUpdated evt:
                    PurchaseOrderAmount = new PurchaseOrderAmount(evt.PurchaseOrderAmount);
                    LastModifiedDate = new LastModifiedDate(evt.LastModifiedDate);
                    break;
                case Events.PurchaseOrderDetailAddedToPurchaseOrder evt:
                    var detailItem = new PurchaseOrderDetail(Apply);
                    ApplyToEntity(detailItem, evt);
                    PurchaseOrderDetails.Add(detailItem);
                    break;
                case Events.PurchaseOrderDetailPurchaseOrderIdUpdated evt:

                    break;
            }
        }

        protected override void EnsureValidState()
        {
            var valid = Id != null &&
                EmployeeId != null &&
                VendorId != null &&
                ExpectedDeliveryDate.Value >= PurchaseOrderDate.Value &&
                EnsureValidStatePurchaseOrderDetails();

            if (!valid)
            {
                throw new InvalidEntityStateException(this, "Post-checks failed!");
            }
        }

        private PurchaseOrderDetail FindPurchaseOrderDetail(PurchaseOrderDetailId id) =>
            PurchaseOrderDetails.FirstOrDefault(item => item.Id == id);

        private bool EnsureValidStatePurchaseOrderDetails()
        {
            var retVal = true;

            foreach (var item in PurchaseOrderDetails)
            {
                if (!item.IsValid())
                {
                    retVal = false;
                    break;
                }
            }

            return retVal;
        }
    }
}