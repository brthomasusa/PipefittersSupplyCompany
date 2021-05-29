using System;

namespace PipefittersSupply.Domain.Purchasing.PurchaseOrder
{
    public static class Events
    {
        public class PurchaseOrderCreated
        {
            public int Id { get; set; }
            public int VendorId { get; set; }
            public int EmployeeId { get; set; }
            public DateTime PurchaseOrderDate { get; set; }
            public DateTime ExpectedDeliveryDate { get; set; }
            public decimal PurchaseOrderAmount { get; set; }
            public DateTime CreatedDate { get; set; }
        }

        public class VendorIdUpdated
        {
            public int Id { get; set; }
            public int VendorId { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }

        public class EmployeeIdUpdated
        {
            public int Id { get; set; }
            public int EmployeeId { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }

        public class PurchaseOrderDateUpdated
        {
            public int Id { get; set; }
            public DateTime PurchaseOrderDate { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }

        public class ExpectedDeliveryDateUpdated
        {
            public int Id { get; set; }
            public DateTime ExpectedDeliveryDate { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }

        public class PurchaseOrderAmountUpdated
        {
            public int Id { get; set; }
            public decimal PurchaseOrderAmount { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }

        // PurchaseOrderDetail domain events

        public class PurchaseOrderDetailAddedToPurchaseOrder
        {
            public int Id { get; set; }
            public int PurchaseOrderId { get; set; }
            public int InventoryId { get; set; }
            public string VendorPartNumber { get; set; }
            public int QuantityOrdered { get; set; }
            public decimal UnitCost { get; set; }
            public DateTime CreatedDate { get; set; }
        }

        public class PurchaseOrderDetailPurchaseOrderIdUpdated
        {
            public int Id { get; set; }
            public int PurchaseOrderId { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }

        public class PurchaseOrderDetailInventoryIdUpdated
        {
            public int Id { get; set; }
            public int InventoryId { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }

        public class PurchaseOrderDetailVendorPartNumberUpdated
        {
            public int Id { get; set; }
            public string VendorPartNumber { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }

        public class PurchaseOrderDetailQuantityOrderedUpdated
        {
            public int Id { get; set; }
            public int QuantityOrdered { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }

        public class PurchaseOrderDetailUnitCostUpdated
        {
            public int Id { get; set; }
            public decimal UnitCost { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }
    }
}