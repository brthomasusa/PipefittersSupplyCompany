using System;
using System.Collections.Generic;
using System.Linq;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.SharedKernel.Interfaces;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.Core.Financing.FinancierAggregate;

namespace PipefittersSupplyCompany.Core.Financing.StockSubscriptionAggregate
{
    public class StockSubscription : AggregateRoot<Guid>, IAggregateRoot
    {
        private List<DividendPayment> _dividendPaymentRates;

        protected StockSubscription() { }

        public StockSubscription
        (
            EconomicEvent economicEvent,
            FinancierId financierId,
            StockIssueDate stockIssueDate,
            SharesIssured sharesIssured,
            PricePerShare pricePerShare,
            UserId userId
        )
        {
            EconomicEvent = economicEvent ?? throw new ArgumentNullException("The economic event is required.");
            Id = economicEvent.Id;
            FinancierId = financierId ?? throw new ArgumentNullException("The financier id is required.");
            StockIssueDate = stockIssueDate ?? throw new ArgumentNullException("The stock issue date is required.");
            SharesIssured = sharesIssured ?? throw new ArgumentNullException("The shares issued amount is required.");
            PricePerShare = pricePerShare ?? throw new ArgumentNullException("The price per share is required.");
            UserId = userId ?? throw new ArgumentNullException("The id of the employee recording this stock subscription is required.");

            CheckValidity();
            _dividendPaymentRates = new List<DividendPayment>();
        }

        public virtual EconomicEvent EconomicEvent { get; private set; }

        public Guid FinancierId { get; private set; }

        public virtual SharesIssured SharesIssured { get; private set; }
        public void UpdateSharesIssured(SharesIssured value)
        {
            SharesIssured = value ?? throw new ArgumentNullException("The number of shares issued is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual PricePerShare PricePerShare { get; private set; }
        public void UpdatePricePerShare(PricePerShare value)
        {
            PricePerShare = value ?? throw new ArgumentNullException("The price per share is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual StockIssueDate StockIssueDate { get; private set; }
        public void UpdateStockIssueDate(StockIssueDate value)
        {
            StockIssueDate = value ?? throw new ArgumentNullException("The stock issue date is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual UserId UserId { get; private set; }
        public void UpdateUserId(UserId value)
        {
            UserId = value ?? throw new ArgumentNullException("The user id is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual IReadOnlyList<DividendPayment> DividendPaymentRates => _dividendPaymentRates.ToList();

        public void AddDividendPaymentRate(DividendPayment dividendDeclaration)
        {
            //TODO check for duplicate dividend declaration
            _dividendPaymentRates.Add(dividendDeclaration);
        }

        public void UpdateDividendPaymentRate(DividendPayment dividendDeclaration)
        {
            string errMsg = $"Update failed, a dividend declaration with id '{dividendDeclaration.Id}' could not be found!";

            DividendPayment found =
                ((List<DividendPayment>)DividendPaymentRates).Find(p => p.Id == dividendDeclaration.Id)
                    ?? throw new InvalidOperationException(errMsg);

            found.UpdateDividendDeclarationDate(dividendDeclaration.DividendDeclarationDate);
            found.UpdateDividendPerShare(dividendDeclaration.DividendPerShare);
            found.UpdateUserId(dividendDeclaration.UserId);
        }

        public void DeleteDividendPaymentRate(Guid dividendId)
        {
            string errMsg = $"Delete failed, a dividend declaration with id '{dividendId}' could not be found!";

            DividendPayment found =
                ((List<DividendPayment>)DividendPaymentRates).Find(p => p.Id == dividendId)
                    ?? throw new InvalidOperationException(errMsg);

            _dividendPaymentRates.Remove(found);
        }

        protected override void CheckValidity()
        {
            if (EconomicEvent.EventType is not EventType.CashReceiptFromStockSubscription)
            {
                throw new ArgumentException("Invalid EconomicEvent type; it must be 'EventType.CashReceiptFromStockSubscription'.");
            }
        }
    }
}