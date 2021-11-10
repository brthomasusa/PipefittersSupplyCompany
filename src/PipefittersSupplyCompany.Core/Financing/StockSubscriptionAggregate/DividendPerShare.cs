using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.StockSubscriptionAggregate
{
    public class DividendPerShare : ValueObject
    {
        public decimal Value { get; }

        protected DividendPerShare() { }

        private DividendPerShare(decimal dividendPerShare)
            : this()
        {
            Value = dividendPerShare;
        }

        public static implicit operator decimal(DividendPerShare self) => self.Value;

        public static DividendPerShare Create(decimal dividendPerShare)
        {
            CheckValidity(dividendPerShare);
            return new DividendPerShare(dividendPerShare);
        }

        private static void CheckValidity(decimal value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("The dividend per share amount must be greater than $0.00.", nameof(value));
            }
        }
    }
}