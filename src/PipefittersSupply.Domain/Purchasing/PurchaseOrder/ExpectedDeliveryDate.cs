using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.Purchasing.PurchaseOrder
{
    public class ExpectedDeliveryDate : Value<ExpectedDeliveryDate>
    {
        public DateTime Value { get; }

        internal ExpectedDeliveryDate(DateTime value) => Value = value;

        public static implicit operator DateTime(ExpectedDeliveryDate self) => self.Value;

        public static ExpectedDeliveryDate FromDateTime(DateTime value)
        {
            CheckValidity(value);
            return new ExpectedDeliveryDate(value);
        }

        private static void CheckValidity(DateTime value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The P.O. expected delivery date is required.", nameof(value));
            }
        }
    }
}