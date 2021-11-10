using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.StockSubscriptionAggregate
{
    public class StockId : ValueObject
    {
        public Guid Value { get; }

        protected StockId() { }

        private StockId(Guid stockId)
            : this()
        {
            Value = stockId;
        }

        public static implicit operator Guid(StockId self) => self.Value;

        public static StockId Create(Guid stockId)
        {
            CheckValidity(stockId);
            return new StockId(stockId);
        }

        private static void CheckValidity(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The stock subscription Id is required; it can not be a default Guid.", nameof(value));
            }
        }
    }
}