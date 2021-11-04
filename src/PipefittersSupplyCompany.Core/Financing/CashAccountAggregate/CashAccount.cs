using System;
using System.Collections.Generic;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.SharedKernel.Interfaces;
using PipefittersSupplyCompany.Core.Shared;

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
            UserId userID
        )
            : this()
        {
            Id = id;
            BankName = bankName;
            CashAccountName = acctName;
            CashAccountNumber = acctNumber;
            RoutingTransitNumber = routingTransitNumber;
            DateOpened = openedDate;
            UserId = userID;

            CheckValidity();
        }

        public virtual BankName BankName { get; private set; }

        public virtual CashAccountName CashAccountName { get; private set; }

        public virtual CashAccountNumber CashAccountNumber { get; private set; }

        public virtual RoutingTransitNumber RoutingTransitNumber { get; private set; }

        public virtual DateOpened DateOpened { get; private set; }

        public virtual UserId UserId { get; private set; }

        public virtual IReadOnlyList<CashAccountTransaction> CashAccountTransactions { get; private set; } = new List<CashAccountTransaction>();

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
                throw new ArgumentNullException("The cash account id is required.");
            }
        }
    }
}