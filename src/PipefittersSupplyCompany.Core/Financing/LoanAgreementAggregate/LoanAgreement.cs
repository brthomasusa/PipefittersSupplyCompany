using System;
using System.Collections.Generic;
using System.Linq;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.SharedKernel.Interfaces;

namespace PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate
{
    public class LoanAgreement : AggregateRoot<Guid>, IAggregateRoot
    {
        private HashSet<LoanPayment> _loanPayments = new HashSet<LoanPayment>();

        protected LoanAgreement() { }

        public LoanAgreement
        (
            EconomicEvent economicEvent,
            Guid financierId,
            LoanAmount loanAmount,
            InterestRate interestRate,
            LoanDate loanDate,
            MaturityDate maturityDate,
            PaymentsPerYear paymentsPerYear,
            UserId userID
        )
        {
            EconomicEvent = economicEvent ?? throw new ArgumentNullException("The economic event is required.");
            Id = economicEvent.Id;
            FinancierId = financierId;
            LoanAmount = loanAmount ?? throw new ArgumentNullException("The loan amount for this loan agreement is required.");
            InterestRate = interestRate ?? throw new ArgumentNullException("The interest rate is required; if zero then pass in 0.");
            LoanDate = loanDate ?? throw new ArgumentNullException("The loan agreement date is required.");
            MaturityDate = maturityDate ?? throw new ArgumentNullException("The loan maturity date is required.");
            PaymentsPerYear = paymentsPerYear ?? throw new ArgumentNullException("The number of loan payments per year is required.");
            UserId = userID;

            CheckValidity();
        }

        public virtual EconomicEvent EconomicEvent { get; private set; }

        public Guid FinancierId { get; private set; }

        public virtual LoanAmount LoanAmount { get; private set; }

        public virtual InterestRate InterestRate { get; private set; }

        public virtual LoanDate LoanDate { get; private set; }

        public virtual MaturityDate MaturityDate { get; private set; }

        public virtual PaymentsPerYear PaymentsPerYear { get; private set; }

        public UserId UserId { get; private set; }

        public virtual IReadOnlyCollection<LoanPayment> LoanPayments => _loanPayments.ToList();

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

        public void UpdateUserId(UserId value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("User id can not be updated with default Guid value.");
            }

            UserId = value;
            CheckValidity();
        }

        public void AddLoanPayment(LoanPayment payment)
        {
            _loanPayments.Add(payment);
        }

        protected override void CheckValidity()
        {
            if (EconomicEvent.EventType is not EventType.CashReceiptFromLoanAgreement)
            {
                throw new ArgumentException("Invalid EconomicEvent type; it must be 'EventType.CashReceiptFromLoanAgreement'.");
            }

            if (DateTime.Compare(MaturityDate, LoanDate) < 0)
            {
                throw new ArgumentException("Loan maturity date must be greater than or equal to the loan date.");
            }

            if (FinancierId == default)
            {
                throw new ArgumentNullException("The financier id is required.");
            }

        }
    }
}