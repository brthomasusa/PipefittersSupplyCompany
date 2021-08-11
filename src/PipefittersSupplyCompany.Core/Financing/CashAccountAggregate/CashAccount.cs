using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Ardalis.GuardClauses;
using PipefittersSupplyCompany.Core.Financing.CashAccountAggregate;
using PipefittersSupplyCompany.Core.Exceptions;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.CashAccountAggregate
{
    public class CashAccount : AggregateRoot
    {
        private string _bankName;
        private string _accountName;
        private string _accountNumber;
        private string _transitABA;
        private DateTime _openedDate;
        private readonly List<CashTransaction> _cashTransactions = new List<CashTransaction>();

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

        public virtual IReadOnlyList<CashTransaction> CashTransactions => _cashTransactions.ToList();

        protected override void EnsureValidState()
        {
            // var valid = Id != default && SupervisorId != default;

            // if (!valid)
            // {
            //     throw new InvalidEntityStateException(this, "Employee validity check failed; the employee id and supervisor id are required.!");
            // }
        }

        protected override void When(BaseDomainEvent @event)
        {
            // switch (@event)
            // {
            //     case EmployeeEvent.EmployeeCreated evt:
            //         Id = evt.Id;
            //         SupervisorId = evt.SupervisorId;
            //         LastName = evt.LastName;
            //         FirstName = evt.FirstName;
            //         MiddleInitial = evt.MiddleInitial;
            //         SSN = evt.SSN;
            //         AddressLine1 = evt.AddressLine1;
            //         AddressLine2 = evt.AddressLine2;
            //         City = evt.City;
            //         StateProvinceCode = evt.StateProvinceCode;
            //         Zipcode = evt.Zipcode;
            //         Telephone = evt.Telephone;
            //         MaritalStatus = evt.MaritalStatus;
            //         TaxExemption = evt.Exemptions;
            //         PayRate = evt.PayRate;
            //         StartDate = evt.StartDate;
            //         IsActive = evt.IsActive;
            //         CreatedDate = DateTime.Now;
            //         break;


            // }
        }   // End of When()         
    }
}