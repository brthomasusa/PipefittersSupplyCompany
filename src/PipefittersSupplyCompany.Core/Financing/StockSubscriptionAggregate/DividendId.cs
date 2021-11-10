using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.StockSubscriptionAggregate
{
    public class DividendId
    {
        public Guid Value { get; }

        protected DividendId() { }

        private DividendId(Guid dividendId)
            : this()
        {
            Value = dividendId;
        }

        public static implicit operator Guid(DividendId self) => self.Value;

        public static DividendId Create(Guid dividendId)
        {
            CheckValidity(dividendId);
            return new DividendId(dividendId);
        }

        private static void CheckValidity(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The dividend Id is required; it can not be a default Guid.", nameof(value));
            }
        }
    }
}