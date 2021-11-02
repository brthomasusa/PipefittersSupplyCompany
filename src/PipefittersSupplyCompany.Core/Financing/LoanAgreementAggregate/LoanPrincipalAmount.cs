using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate
{
    public class LoanPrincipalAmount : ValueObject
    {
        public decimal Value { get; }

        protected LoanPrincipalAmount() { }

        private LoanPrincipalAmount(decimal principalAmount)
            : this()
        {
            Value = principalAmount;
        }

        public static implicit operator decimal(LoanPrincipalAmount self) => self.Value;

        public static LoanPrincipalAmount Create(decimal principalAmount)
        {
            CheckValidity(principalAmount);
            return new LoanPrincipalAmount(principalAmount);
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