using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate
{
    public class LoanDate : ValueObject
    {
        public DateTime Value { get; }

        protected LoanDate() { }

        private LoanDate(DateTime loanDate)
            : this()
        {
            Value = loanDate;
        }

        public static implicit operator DateTime(LoanDate self) => self.Value;

        public static LoanDate Create(DateTime loanDate)
        {
            CheckValidity(loanDate);
            return new LoanDate(loanDate);
        }

        private static void CheckValidity(DateTime value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The loan date is required.", nameof(value));
            }
        }
    }
}