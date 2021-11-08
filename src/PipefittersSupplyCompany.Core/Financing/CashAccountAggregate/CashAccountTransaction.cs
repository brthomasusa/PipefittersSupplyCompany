using System;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.CashAccountAggregate
{
    public class CashAccountTransaction : Entity<int>
    {
        private string _emittanceAdvice;

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
            UserId userID
        )
            : this()
        {
            CashTransactionType = transactionType;
            CashAccountId = cashAcctID ?? throw new ArgumentNullException("The cash account id is required.");
            CashAcctTransactionDate = transactionDate ?? throw new ArgumentNullException("The cash transaction date is required.");
            CashAcctTransactionAmount = transactionAmount ?? throw new ArgumentNullException("The cash transaction amount is required.");
            AgentId = agentId ?? throw new ArgumentNullException("The external agent id is required.");
            EventId = eventId ?? throw new ArgumentNullException("The cash economic event id is required.");
            CheckNumber = checkNumber ?? throw new ArgumentNullException("The check number is required.");
            RemittanceAdvice = remittanceAdvice;
            UserId = userID ?? throw new ArgumentNullException("The user id is required.");

            CheckValidity();
        }

        public virtual CashTransactionType CashTransactionType { get; private set; }
        public void UpdateCashTransactionType(CashTransactionType cashTransactionType)
        {
            //TODO add validation to CashTransactionType
            CashTransactionType = cashTransactionType;
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual Guid CashAccountId { get; private set; }
        public void UpdateCashAccountId(CashAccountId cashAcctID)
        {
            CashAccountId = cashAcctID ?? throw new ArgumentNullException("The cash account id is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual CashAcctTransactionDate CashAcctTransactionDate { get; private set; }
        public void UpdateCashAcctTransactionDate(CashAcctTransactionDate transactionDate)
        {
            CashAcctTransactionDate = transactionDate ?? throw new ArgumentNullException("The transaction date is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual CashAcctTransactionAmount CashAcctTransactionAmount { get; private set; }
        public void UpdateCashAcctTransactionAmount(CashAcctTransactionAmount transactionAmount)
        {
            CashAcctTransactionAmount = transactionAmount ?? throw new ArgumentNullException("The transaction amount is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual ExternalAgentId AgentId { get; private set; }
        public void UpdateExternalAgentId(ExternalAgentId agentId)
        {
            AgentId = agentId ?? throw new ArgumentNullException("The external agent id is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual EconomicEventId EventId { get; private set; }
        public void UpdateEconomicEventId(EconomicEventId eventId)
        {
            EventId = eventId ?? throw new ArgumentNullException("The economic event id is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual CheckNumber CheckNumber { get; private set; }
        public void UpdateCheckNumber(CheckNumber checkNumber)
        {
            CheckNumber = checkNumber ?? throw new ArgumentNullException("The check number is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

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

        public void UpdateRemittanceAdvice(string remittanceAdvice)
        {
            //TODO add validation to remittance advice
            RemittanceAdvice = remittanceAdvice;
            UpdateLastModifiedDate();
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
            // 

        }
    }
}