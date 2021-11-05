using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate
{
    public class LoanId : ValueObject
    {
        public Guid Value { get; }

        protected LoanId() { }

        private LoanId(Guid loanID)
            : this()
        {
            Value = loanID;
        }

        public static implicit operator Guid(LoanId self) => self.Value;

        public static LoanId Create(Guid loanID)
        {
            CheckValidity(loanID);
            return new LoanId(loanID);
        }

        private static void CheckValidity(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The loan agreement Id is required; it can not be a default Guid.", nameof(value));
            }
        }
    }
}