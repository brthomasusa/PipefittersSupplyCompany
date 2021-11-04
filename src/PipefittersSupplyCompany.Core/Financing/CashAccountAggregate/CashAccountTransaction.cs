using System;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.CashAccountAggregate
{
    public class CashAccountTransaction : Entity<int>
    {
        private string _emittanceAdvice;
        private string _notes;

        protected CashAccountTransaction() { }

        public CashAccountTransaction
        (
            CashTransactionType transactionType,
            CashAccountId cashAcctID,
            CashAcctTransactionDate transactionDate,
            CashAcctTransactionAmount transactionAmount,
            ExternalAgentId agentId,
            EconomicEventId eventId,
            CheckNumber checkNumber,
            string remittanceAdvice,
            string notes,
            UserId userID
        )
            : this()
        {
            CashTransactionType = transactionType;
            CashAccountId = cashAcctID;
            CashAcctTransactionDate = transactionDate;
            CashAcctTransactionAmount = transactionAmount;
            EventId = eventId;
            AgentId = agentId;
            CheckNumber = checkNumber;
            RemittanceAdvice = remittanceAdvice;
            Notes = notes;
            UserId = userID;

            CheckValidity();
        }

        public virtual CashTransactionType CashTransactionType { get; private set; }

        public virtual CashAccountId CashAccountId { get; private set; }

        public virtual CashAcctTransactionDate CashAcctTransactionDate { get; }

        public virtual CashAcctTransactionAmount CashAcctTransactionAmount { get; }

        public virtual ExternalAgentId AgentId { get; private set; }

        public virtual EconomicEventId EventId { get; private set; }

        public virtual CheckNumber CheckNumber { get; private set; }

        public string RemittanceAdvice
        {
            get { return _emittanceAdvice; }

            private set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (value.Length <= 50)
                    {
                        _emittanceAdvice = value;
                    }
                    else
                    {
                        throw new ArgumentException("The remittance advice maximum length is 50 characters.", nameof(value));
                    }
                }
            }
        }

        public string Notes
        {
            get { return _notes; }

            private set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (value.Length <= 3072)
                    {
                        _notes = value;
                    }
                    else
                    {
                        throw new ArgumentException("The notes maximum length is 3072 characters.", nameof(value));
                    }
                }
            }
        }

        public virtual UserId UserId { get; private set; }

        protected override void CheckValidity()
        {
            // 

        }
    }
}