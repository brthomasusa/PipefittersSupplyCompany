using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate
{
    public class InterestRate : ValueObject
    {
        public double Value { get; }

        protected InterestRate() { }

        private InterestRate(double rate)
            : this()
        {
            Value = rate;
        }

        public static InterestRate Create(double rate)
        {
            CheckValidity(rate);
            return new InterestRate(rate);
        }

        public static implicit operator double(InterestRate self) => self.Value;

        private static void CheckValidity(double value)
        {
            if (value < 0)
            {
                throw new ArgumentException("The interest rate can not be negative.", nameof(value));
            }
        }
    }
}