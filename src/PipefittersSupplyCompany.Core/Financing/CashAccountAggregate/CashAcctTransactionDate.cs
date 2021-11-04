using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.CashAccountAggregate
{
    public class CashAcctTransactionDate : ValueObject
    {
        public DateTime Value { get; }

        protected CashAcctTransactionDate() { }

        public CashAcctTransactionDate(DateTime transactionDate)
            : this()
        {
            Value = transactionDate;
        }

        public static implicit operator DateTime(CashAcctTransactionDate self) => self.Value;

        public static CashAcctTransactionDate Create(DateTime transactionDate)
        {
            CheckValidity(transactionDate);
            return new CashAcctTransactionDate(transactionDate);
        }

        private static void CheckValidity(DateTime value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The cash account transaction date is required.", nameof(value));
            }
        }
    }
}