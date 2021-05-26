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
    }
}