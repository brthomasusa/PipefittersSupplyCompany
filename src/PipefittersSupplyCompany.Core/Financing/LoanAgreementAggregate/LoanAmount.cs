using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate
{
    public class LoanAmount : ValueObject
    {
        public decimal Value { get; }

        protected LoanAmount() { }

        private LoanAmount(decimal loanAmount)
            : this()
        {
            Value = loanAmount;
        }

        public static implicit operator decimal(LoanAmount self) => self.Value;

        public static LoanAmount Create(decimal loanAmount)
        {
            CheckValidity(loanAmount);
            return new LoanAmount(loanAmount);
        }

        private static void CheckValidity(decimal value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("The loan amount must be greater than $0.00.", nameof(value));
            }
        }
    }
}