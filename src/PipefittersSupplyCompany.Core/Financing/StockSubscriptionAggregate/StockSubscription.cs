using System;
using System.Collections.Generic;
using System.Linq;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.SharedKernel.Interfaces;
using PipefittersSupplyCompany.Core.Shared;

namespace PipefittersSupplyCompany.Core.Financing.StockSubscriptionAggregate
{
    public class StockSubscription : AggregateRoot<Guid>, IAggregateRoot
    {
        protected StockSubscription() { }

        public StockSubscription
        (
            EconomicEvent economicEvent,
            Guid financierId,
            SharesIssured sharesIssured,
            PricePerShare pricePerShare,
            StockIssueDate stockIssueDate,
            UserId id
        )
        {
            EconomicEvent = economicEvent ?? throw new ArgumentNullException("The economic event is required.");
            Id = economicEvent.Id;
            FinancierId = financierId;
        }

        public virtual EconomicEvent EconomicEvent { get; private set; }

        public Guid FinancierId { get; private set; }

        public virtual SharesIssured SharesIssured { get; private set; }

        public virtual PricePerShare PricePerShare { get; private set; }

        public virtual StockIssueDate StockIssueDate { get; private set; }

        public virtual UserId UserId { get; private set; }


        protected override void CheckValidity()
        {
            if (EconomicEvent.EventType is not EventType.CashReceiptFromStockSubscription)
            {
                throw new ArgumentException("Invalid EconomicEvent type; it must be 'EventType.CashReceiptFromStockSubscription'.");
            }

            if (FinancierId == default)
            {
                throw new ArgumentNullException("The financier id is required.");
            }

        }
    }
}