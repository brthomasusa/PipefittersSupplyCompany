using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.Purchasing.PurchaseOrder
{
    public class PurchaseOrderAmount : Value<PurchaseOrderAmount>
    {
        public decimal Value { get; }

        protected PurchaseOrderAmount() { }


        internal PurchaseOrderAmount(decimal value) => Value = value;

        public static implicit operator decimal(PurchaseOrderAmount self) => self.Value;

        public static PurchaseOrderAmount FromDecimal(decimal rate)
        {
            CheckValidity(rate);
            return new PurchaseOrderAmount(rate);
        }

        private static void CheckValidity(decimal value)
        {
            if (value < 0M)
            {
                throw new ArgumentException("Invalid P.O. amount, can not be a negative amount!", nameof(value));
            }
        }
    }
}