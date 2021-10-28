using System;
using System.Collections.Generic;
using System.Linq;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.CashAccountAggregate
{
    public class CashAccount : AggregateRoot<Guid>
    {
        private string _bankName;
        private string _accountName;
        private string _accountNumber;
        private string _transitABA;
        private DateTime _openedDate;
        private List<CashAccountTransaction> _cashTransactions = new List<CashAccountTransaction>();

        protected CashAccount() { }

        public CashAccount(Guid id, string bankName, string acctName, string acctNumber, string transAba, DateTime openedDate)
            : this()
        {

        }

        public string bankName
        {
            get { return _bankName; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("The bank name is required.", nameof(value));
                }

                if (value.Length > 25)
                {
                    throw new ArgumentException("The bank name maximum length is 25 characters.", nameof(value));
                }

                _bankName = value;
            }
        }

        public string AccountName
        {
            get { return _accountName; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("The account name is required.", nameof(value));
                }

                if (value.Length > 25)
                {
                    throw new ArgumentException("The account name maximum length is 25 characters.", nameof(value));
                }

                _accountName = value;
            }
        }

        public string AccountNumber
        {
            get { return _accountNumber; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("The account number is required.", nameof(value));
                }

                if (value.Length > 25)
                {
                    throw new ArgumentException("The account number maximum length is 25 characters.", nameof(value));
                }

                _accountNumber = value;
            }
        }

        public string TransitABA
        {
            get { return _transitABA; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("The transit ABA number is required.", nameof(value));
                }

                if (value.Length > 25)
                {
                    throw new ArgumentException("The transit ABA number maximum length is 25 characters.", nameof(value));
                }

                _transitABA = value;
            }
        }

        public DateTime OpenedDate
        {
            get { return _openedDate; }

            private set
            {
                if (value == default)
                {
                    throw new ArgumentNullException("The account open date is required.", nameof(value));
                }

                _openedDate = value;
            }
        }

        public virtual IReadOnlyList<CashAccountTransaction> CashTransactions => _cashTransactions.ToList();
    }
}