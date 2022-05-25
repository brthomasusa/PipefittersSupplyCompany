using System;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.StockSubscriptionAggregate
{
    public class DividendPayment : Entity<Guid>
    {
        protected DividendPayment() { }

        public DividendPayment
        (
            EconomicEvent economicEvent,
            StockSubscription stockSubscription,
            DividendDeclarationDate dividendDeclarationDate,
            DividendPerShare dividendPerShare,
            UserId userID
        )
        {
            EconomicEvent = economicEvent ?? throw new ArgumentNullException("The economic event is required.");
            StockSubscription = stockSubscription ?? throw new ArgumentNullException("The stock subscription is required.");
            StockId = stockSubscription.Id;
            DividendDeclarationDate = dividendDeclarationDate ?? throw new ArgumentNullException("The dividend declaration date is required.");
            DividendPerShare = dividendPerShare ?? throw new ArgumentNullException("The dividend per share is required.");
            UserId = userID ?? throw new ArgumentNullException("The stock issue date is required.");

            CheckValidity();
        }

        public virtual EconomicEvent EconomicEvent { get; private set; }

        public Guid StockId { get; private set; }
        public virtual StockSubscription StockSubscription { get; private set; }

        public virtual DividendDeclarationDate DividendDeclarationDate { get; private set; }
        public void UpdateDividendDeclarationDate(DividendDeclarationDate value)
        {
            DividendDeclarationDate = value ?? throw new ArgumentNullException("The dividend declaration date is required.");
        }

        public virtual DividendPerShare DividendPerShare { get; set; }
        public void UpdateDividendPerShare(DividendPerShare value)
        {
            DividendPerShare = value ?? throw new ArgumentNullException("The dividend per share is required.");
        }

        public virtual UserId UserId { get; private set; }
        public void UpdateUserId(UserId value)
        {
            UserId = value ?? throw new ArgumentNullException("The user id is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        protected override void CheckValidity()
        {
            if (EconomicEvent.EventType is not EventType.CashDisbursementForDividentPayment)
            {
                throw new ArgumentException("Invalid EconomicEvent type; it must be 'EventType.CashReceiptFromStockSubscription'.");
            }

            if (DividendDeclarationDate.Value < StockSubscription.StockIssueDate)
            {
                throw new ArgumentException("Invalid dividend declaration date; it can not be before stock subscription issue date'.");
            }
        }
    }
}