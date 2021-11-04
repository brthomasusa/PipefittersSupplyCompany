using System;
using PipefittersSupplyCompany.SharedKernel;


namespace PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate
{
    public class LoanPrincipalRemaining : ValueObject
    {
        public decimal Value { get; }

        protected LoanPrincipalRemaining() { }

        private LoanPrincipalRemaining(decimal principalRemainingAmount)
            : this()
        {
            Value = principalRemainingAmount;
        }

        public static implicit operator decimal(LoanPrincipalRemaining self) => self.Value;

        public static LoanPrincipalRemaining Create(decimal principalRemainingAmount)
        {
            CheckValidity(principalRemainingAmount);
            return new LoanPrincipalRemaining(principalRemainingAmount);
        }

        private static void CheckValidity(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException("The loan principal remaining amount can not be negative.", nameof(value));
            }
        }
    }
}