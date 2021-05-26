using System;
using System.Collections.Generic;
using PipefittersSupply.Domain.Common;
using PipefittersSupply.Domain.HumanResources.Employees;
using PipefittersSupply.Domain.Purchasing.Vendor;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Purchasing.PurchaseOrder
{
    public class PurchaseOrder : Entity<PurchaseOrderId>
    {
        public List<PurchaseOrderDetail> PurchaseOrderDetails { get; private set; } = new List<PurchaseOrderDetail>();

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
                PurchaseOrderAmount = amount
            });


        public PurchaseOrderId Id { get; private set; }

        public VendorId VendorId { get; private set; }
        public void UpdateVendorId(VendorId value) =>
            Apply(new Events.VendorIdUpdated
            {
                Id = Id,
                VendorId = EmployeeId,
                LastModifiedDate = LastModifiedDate
            });

        public EmployeeId EmployeeId { get; private set; }
        public void UpdateEmployeeId(EmployeeId value) =>
            Apply(new Events.EmployeeIdUpdated
            {
                Id = Id,
                EmployeeId = EmployeeId,
                LastModifiedDate = LastModifiedDate
            });

        public PurchaseOrderDate PurchaseOrderDate { get; private set; }

        public ExpectedDeliveryDate ExpectedDeliveryDate { get; private set; }

        public PurchaseOrderAmount PurchaseOrderAmount { get; private set; }

        public CreatedDate CreatedDate { get; private set; }

        public LastModifiedDate LastModifiedDate { get; private set; }

        protected override void EnsureValidState()
        {
            var valid = Id != null &&
                EmployeeId != null &&
                VendorId != null &&
                ExpectedDeliveryDate.Value >= PurchaseOrderDate.Value;

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