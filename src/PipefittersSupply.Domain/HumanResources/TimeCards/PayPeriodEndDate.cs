using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.HumanResources.TimeCards
{
    public class PayPeriodEndDate : Value<PayPeriodEndDate>
    {
        public DateTime Value { get; }

        protected PayPeriodEndDate() { }

        internal PayPeriodEndDate(DateTime value) => Value = value;

        public static implicit operator DateTime(PayPeriodEndDate self) => self.Value;

        public static PayPeriodEndDate FromDateTime(DateTime endDate)
        {
            CheckValidity(endDate);
            return new PayPeriodEndDate(endDate);
        }

        private static void CheckValidity(DateTime endDate)
        {
            if (endDate == default)
            {
                throw new ArgumentNullException("The pay period ending date is required.", nameof(endDate));
            }
        }
    }
}
