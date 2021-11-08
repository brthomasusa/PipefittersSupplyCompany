using System;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.StockSubscriptionAggregate
{
    public class DividendPaymentRate : Entity<Guid>
    {
        protected DividendPaymentRate() { }

        public DividendPaymentRate
        (
            EconomicEvent economicEvent,
            UserId userID
        )
        {

        }
    }
}