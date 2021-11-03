using System;
using System.Collections.Generic;
using System.Linq;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.SharedKernel.Interfaces;

namespace PipefittersSupplyCompany.Core.Financing.CashAccountAggregate
{
    public class CashAccount : AggregateRoot<Guid>, IAggregateRoot
    {
        private List<CashAccountTransaction> _cashTransactions = new List<CashAccountTransaction>();

        protected CashAccount() { }

        public CashAccount
        (
            Guid id,
            BankName bankName,
            CashAccountName acctName,
            CashAccountNumber acctNumber,
            RoutingTransitNumber routingTransitNumber,
            DateOpened openedDate,
            Guid userID
        )
            : this()
        {
            if (id == default)
            {
                throw new ArgumentNullException("The cash account id is required.", nameof(id));
            }

            Id = id;
            BankName = bankName ?? throw new ArgumentNullException("The bank name is required");
            CashAccountName = acctName ?? throw new ArgumentNullException("The cash account name is required.", nameof(acctName));
            CashAccountNumber = acctNumber ?? throw new ArgumentNullException("The cash account number is required.", nameof(acctNumber));
            RoutingTransitNumber = routingTransitNumber ?? throw new ArgumentNullException("The bank routing number is required.", nameof(routingTransitNumber));
            DateOpened = openedDate ?? throw new ArgumentNullException("The account open date is required.", nameof(openedDate)); ;

            if (userID == default)
            {
                throw new ArgumentNullException("The user id (the employee creating this record) parameter is required.");
            }
            UserId = userID;

            CheckValidity();
        }

        public virtual BankName BankName { get; private set; }

        public virtual CashAccountName CashAccountName { get; private set; }

        public virtual CashAccountNumber CashAccountNumber { get; private set; }

        public virtual RoutingTransitNumber RoutingTransitNumber { get; private set; }

        public virtual DateOpened DateOpened { get; private set; }

        public Guid UserId { get; private set; }

        public virtual IReadOnlyList<CashAccountTransaction> CashTransactions => _cashTransactions.ToList();

        public void UpdateBankName(BankName value) => BankName = value;

        public void UpdateCashAccountName(CashAccountName value) => CashAccountName = value;

        public void UpdateCashAccountNumber(CashAccountNumber value) => CashAccountNumber = value;

        public void UpdateRoutingTransitNumber(RoutingTransitNumber value) => RoutingTransitNumber = value;

        public void UpdateDateOpened(DateOpened value) => DateOpened = value;

        public void UpdateUserId(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The user id (the employee updating this record) parameter is required.");
            }
        }

        protected override void CheckValidity()
        {
            if (Id == default)
            {
                throw new ArgumentException("The cash account id is required'.");
            }

        }
    }
}