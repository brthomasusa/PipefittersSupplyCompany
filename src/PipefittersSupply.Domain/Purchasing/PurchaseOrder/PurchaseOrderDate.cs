using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.Purchasing.PurchaseOrder
{
    public class PurchaseOrderDate : Value<PurchaseOrderDate>
    {
        public DateTime Value { get; }

        protected PurchaseOrderDate() { }

        internal PurchaseOrderDate(DateTime value) => Value = value;

        public static implicit operator DateTime(PurchaseOrderDate self) => self.Value;

        public static PurchaseOrderDate FromDateTime(DateTime value)
        {
            CheckValidity(value);
            return new PurchaseOrderDate(value);
        }

        private static void CheckValidity(DateTime value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The purchase order date is required.", nameof(value));
            }
        }
    }
}