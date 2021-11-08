using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate
{
    public class PaymentNumber : ValueObject
    {
        public int Value { get; }

        protected PaymentNumber() { }

        private PaymentNumber(int paymentsPerYear)
            : this()
        {
            Value = paymentsPerYear;
        }

        public static implicit operator int(PaymentNumber self) => self.Value;

        public static PaymentNumber Create(int paymentNumber)
        {
            CheckValidity(paymentNumber);
            return new PaymentNumber(paymentNumber);
        }

        private static void CheckValidity(int value)
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException("The payment number must be greater than or equal to one.", nameof(value));
            }
        }
    }
}