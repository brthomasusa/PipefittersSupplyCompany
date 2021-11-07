using System;
using System.Collections.Generic;
using System.Linq;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.SharedKernel.Interfaces;
using PipefittersSupplyCompany.Core.Shared;

namespace PipefittersSupplyCompany.Core.Financing.CashAccountAggregate
{
    public class CashAccount : AggregateRoot<Guid>, IAggregateRoot
    {
        private List<CashAccountTransaction> _cashAccountTransactions = new List<CashAccountTransaction>();

        protected CashAccount() { }

        public CashAccount
        (
            CashAccountId id,
            BankName bankName,
            CashAccountName acctName,
            CashAccountNumber acctNumber,
            RoutingTransitNumber routingTransitNumber,
            DateOpened openedDate,
            UserId userID
        )
            : this()
        {
            Id = id ?? throw new ArgumentNullException("The cash account id is required.");
            BankName = bankName ?? throw new ArgumentNullException("The bank name is required.");
            CashAccountName = acctName ?? throw new ArgumentNullException("The cash account name is required.");
            CashAccountNumber = acctNumber ?? throw new ArgumentNullException("The cash account number is required.");
            RoutingTransitNumber = routingTransitNumber ?? throw new ArgumentNullException("The routing account number is required.");
            DateOpened = openedDate ?? throw new ArgumentNullException("The date that the cash account was opened is required.");
            UserId = userID ?? throw new ArgumentNullException("The user id is required.");

            CheckValidity();
        }

        //TODO unit test passing null to Update methods
        public virtual BankName BankName { get; private set; }
        public void UpdateBankName(BankName value)
        {
            BankName = value ?? throw new ArgumentNullException("The bank name is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual CashAccountName CashAccountName { get; private set; }
        public void UpdateCashAccountName(CashAccountName value)
        {
            CashAccountName = value ?? throw new ArgumentNullException("The cash account name is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual CashAccountNumber CashAccountNumber { get; private set; }
        public void UpdateCashAccountNumber(CashAccountNumber value)
        {
            CashAccountNumber = value ?? throw new ArgumentNullException("The cash account number is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual RoutingTransitNumber RoutingTransitNumber { get; private set; }
        public void UpdateRoutingTransitNumber(RoutingTransitNumber value)
        {
            RoutingTransitNumber = value ?? throw new ArgumentNullException("The routing transit number is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual DateOpened DateOpened { get; private set; }
        public void UpdateDateOpened(DateOpened value)
        {
            DateOpened = value ?? throw new ArgumentNullException("The account open date is required.");
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

        public virtual IReadOnlyList<CashAccountTransaction> CashAccountTransactions => _cashAccountTransactions.ToList();

        public void AddCashAccountTransaction(CashAccountTransaction cashAccountTransaction)
        {
            //TODO check for duplicate loan payment
            _cashAccountTransactions.Add(cashAccountTransaction);
        }

        public void UpdateCashAccountTransaction(CashAccountTransaction cashAccountTransaction)
        {
            string errMsg = $"Update failed, a cash transaction with id '{cashAccountTransaction.Id}' could not be found!";

            CashAccountTransaction found =
                ((List<CashAccountTransaction>)CashAccountTransactions).Find(p => p.Id == cashAccountTransaction.Id)
                    ?? throw new InvalidOperationException(errMsg);

            // found.UpdateLoanInterestAmount(payment.LoanInterestAmount);
            // found.UpdatePaymentNumber(payment.PaymentNumber);
            // found.UpdatePaymentDueDate(payment.PaymentDueDate);
            // found.UpdateLoanPrincipalAmount(payment.LoanPrincipalAmount);
            // found.UpdateLoanInterestAmount(payment.LoanInterestAmount);
            // found.UpdateLoanPrincipalRemaining(payment.LoanPrincipalRemaining);
            // found.UpdateUserId(payment.UserId);
        }

        public void DeleteCashAccountTransaction(CashAccountTransaction cashAccountTransaction)
        {
            string errMsg = $"Delete failed, a cash transaction with id '{cashAccountTransaction.Id}' could not be found!";

            CashAccountTransaction found =
                ((List<CashAccountTransaction>)CashAccountTransactions).Find(p => p.Id == cashAccountTransaction.Id)
                    ?? throw new InvalidOperationException(errMsg);

            _cashAccountTransactions.Remove(found);
        }

        protected override void CheckValidity()
        {
            //
        }
    }
}