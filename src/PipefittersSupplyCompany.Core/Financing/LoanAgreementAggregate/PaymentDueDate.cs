using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate
{
    public class PaymentDueDate : ValueObject
    {
        public DateTime Value { get; }

        protected PaymentDueDate() { }

        private PaymentDueDate(DateTime paymentDueDate)
            : this()
        {
            Value = paymentDueDate;
        }

        public static implicit operator DateTime(PaymentDueDate self) => self.Value;

        public static PaymentDueDate Create(DateTime paymentDueDate, LoanAgreement loanAgreement)
        {
            CheckValidity(paymentDueDate, loanAgreement);
            return new PaymentDueDate(paymentDueDate);
        }

        private static void CheckValidity(DateTime value, LoanAgreement loanAgreement)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The payment due date is required.", nameof(value));
            }

            if (value < loanAgreement.LoanDate)
            {
                throw new ArgumentOutOfRangeException("Payment due date can not be before loan date.", nameof(value));
            }
        }
    }
}