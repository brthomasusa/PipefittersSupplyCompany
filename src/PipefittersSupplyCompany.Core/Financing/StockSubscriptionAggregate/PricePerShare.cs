using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.StockSubscriptionAggregate
{
    public class PricePerShare : ValueObject
    {
        public decimal Value { get; }

        protected PricePerShare() { }

        private PricePerShare(decimal pricePerShare)
            : this()
        {
            Value = pricePerShare;
        }

        public static implicit operator decimal(PricePerShare self) => self.Value;

        public static PricePerShare Create(decimal pricePerShare)
        {
            CheckValidity(pricePerShare);
            return new PricePerShare(pricePerShare);
        }

        private static void CheckValidity(decimal value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("The price per share must be greater than $0.00.", nameof(value));
            }
        }
    }
}