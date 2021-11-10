using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate
{
    public class LoanPaymentId : ValueObject
    {
        public Guid Value { get; }

        protected LoanPaymentId() { }

        private LoanPaymentId(Guid loanPaymentId)
            : this()
        {
            Value = loanPaymentId;
        }

        public static implicit operator Guid(LoanPaymentId self) => self.Value;

        public static LoanPaymentId Create(Guid loanPaymentId)
        {
            CheckValidity(loanPaymentId);
            return new LoanPaymentId(loanPaymentId);
        }

        private static void CheckValidity(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The loan payment Id is required; it can not be a default Guid.", nameof(value));
            }
        }
    }
}