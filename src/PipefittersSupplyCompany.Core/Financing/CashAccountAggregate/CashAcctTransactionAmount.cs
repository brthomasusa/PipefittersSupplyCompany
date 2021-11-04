using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.CashAccountAggregate
{
    public class CashAcctTransactionAmount : ValueObject
    {
        public decimal Value { get; }

        protected CashAcctTransactionAmount() { }

        private CashAcctTransactionAmount(decimal transactionAmount)
            : this()
        {
            Value = transactionAmount;
        }

        public static implicit operator decimal(CashAcctTransactionAmount self) => self.Value;

        public static CashAcctTransactionAmount Create(decimal transactionAmount)
        {
            CheckValidity(transactionAmount);
            return new CashAcctTransactionAmount(transactionAmount);
        }

        private static void CheckValidity(decimal value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("The cash account transaction amount must be greater than $0.00.", nameof(value));
            }
        }
    }
}