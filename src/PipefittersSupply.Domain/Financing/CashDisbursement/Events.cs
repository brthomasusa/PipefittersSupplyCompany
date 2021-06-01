using System;

namespace PipefittersSupply.Domain.Financing.CashDisbursement
{
    public static class Events
    {
        public class CashDisbursementTypeCreated
        {
            public int Id { get; set; }
            public string EventTypeName { get; set; }
            public string PayeeTypeName { get; set; }
            public DateTime CreatedDate { get; set; }
        }

        public class EventTypeNameUpdated
        {
            public int Id { get; set; }
            public string EventTypeName { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }

        public class PayeeTypeNameUpdated
        {
            public int Id { get; set; }
            public string PayeeTypeName { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }
    }
}