using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate
{
    public class PaymentsPerYear : ValueObject
    {
        public int Value { get; }

        protected PaymentsPerYear() { }

        private PaymentsPerYear(int paymentsPerYear)
            : this()
        {
            Value = paymentsPerYear;
        }

        public static implicit operator int(PaymentsPerYear self) => self.Value;

        public static PaymentsPerYear Create(int paymentsPerYear)
        {
            CheckValidity(paymentsPerYear);
            return new PaymentsPerYear(paymentsPerYear);
        }

        private static void CheckValidity(int value)
        {
            if (value < 1 || value > 365)
            {
                throw new ArgumentOutOfRangeException("The number of payments per year must be between 1 and 365.", nameof(value));
            }
        }
    }
}