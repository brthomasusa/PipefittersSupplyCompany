using System;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.SharedKernel.Interfaces;
using PipefittersSupplyCompany.Core.Financing.FinancierAggregate;

namespace PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate
{
    public class LoanAgreement : AggregateRoot<Guid>, IAggregateRoot
    {
        protected LoanAgreement() { }

        public LoanAgreement
        (
            Guid id,
            Financier financier,
            LoanAmount loanAmount,
            InterestRate interestRate,
            LoanDate loanDate,
            MaturityDate maturityDate,
            PaymentsPerYear paymentsPerYear,
            Guid userID
        )
        {
            if (id == default)
            {
                throw new ArgumentNullException("The loan id is required.");
            }
            Id = id;

            Financier = financier ?? throw new ArgumentNullException("The financier of this loan agreement is required.");
            LoanAmount = loanAmount ?? throw new ArgumentNullException("The loan amount for this loan agreement is required.");
            InterestRate = interestRate ?? throw new ArgumentNullException("The interest rate is required; if zero then pass in 0.");
            LoanDate = loanDate ?? throw new ArgumentNullException("The loan agreement date is required.");
            MaturityDate = maturityDate ?? throw new ArgumentNullException("The loan maturity date is required.");
            PaymentsPerYear = paymentsPerYear ?? throw new ArgumentNullException("The number of loan payments per year is required.");

            if (userID == default)
            {
                throw new ArgumentNullException("The user id of the employee recording this loan agreement is required.");
            }
            UserId = userID;

            CheckValidity();
        }

        public virtual Financier Financier { get; private set; }

        public virtual LoanAmount LoanAmount { get; private set; }

        public virtual InterestRate InterestRate { get; private set; }

        public virtual LoanDate LoanDate { get; private set; }

        public virtual MaturityDate MaturityDate { get; private set; }

        public virtual PaymentsPerYear PaymentsPerYear { get; private set; }

        public Guid UserId { get; private set; }

        public void UpdateLoanAmount(LoanAmount value)
        {
            LoanAmount = value ?? throw new ArgumentNullException("The loan amount for this loan agreement is required.");
            CheckValidity();
        }

        public void UpdateInterestRate(InterestRate value)
        {
            InterestRate = value ?? throw new ArgumentNullException("The interest rate is required; if zero then pass in 0.");
            CheckValidity();
        }

        public void UpdateLoanDate(LoanDate value)
        {
            LoanDate = value ?? throw new ArgumentNullException("The loan agreement date is required.");
            CheckValidity();
        }

        public void UpdateMaturityDate(MaturityDate value)
        {
            MaturityDate = value ?? throw new ArgumentNullException("The loan maturity date is required.");
        }

        public void UpdatePaymentsPerYear(PaymentsPerYear value)
        {
            PaymentsPerYear = value ?? throw new ArgumentNullException("The number of loan payments per year is required.");
            CheckValidity();
        }

        public void UpdateUserId(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("User id can not be updated with default Guid value.");
            }

            UserId = value;
            CheckValidity();
        }

        protected override void CheckValidity()
        {
            if (DateTime.Compare(MaturityDate, LoanDate) < 0)
            {
                throw new ArgumentException("Loan maturity date must be greater than or equal to the loan date.");
            }
        }
    }
}