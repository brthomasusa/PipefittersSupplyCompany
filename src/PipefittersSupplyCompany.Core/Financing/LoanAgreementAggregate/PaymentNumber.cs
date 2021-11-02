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

        public static PaymentNumber Create(int paymentNumber, LoanAgreement loanAgreement)
        {
            CheckValidity(paymentNumber, loanAgreement);
            return new PaymentNumber(paymentNumber);
        }

        private static void CheckValidity(int value, LoanAgreement loanAgreement)
        {
            if (value < 1)
            {
                throw new ArgumentOutOfRangeException("The payment number must be greater than or equal to one.", nameof(value));
            }

            if (value > (MonthDiff(loanAgreement.LoanDate, loanAgreement.MaturityDate)))
            {
                throw new ArgumentOutOfRangeException("Payment number can not be greater than the length (in months) of the loan agreement.", nameof(value));
            }
        }

        private static int MonthDiff(DateTime startDate, DateTime endDate)
        {
            int m1;
            int m2;
            if (startDate < endDate)
            {
                m1 = (endDate.Month - startDate.Month);//for years
                m2 = (endDate.Year - startDate.Year) * 12; //for months
            }
            else
            {
                m1 = (startDate.Month - endDate.Month);//for years
                m2 = (startDate.Year - endDate.Year) * 12; //for months
            }

            return m1 + m2;
        }
    }
}