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
        private List<LoanPayment> _loanPayments = new List<LoanPayment>();

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
            UserId = userID ?? throw new ArgumentNullException("The id of the employee creating this loan agreement is required."); ;

            CheckValidity();
        }

        public virtual EconomicEvent EconomicEvent { get; private set; }

        public Guid FinancierId { get; private set; }

        public virtual LoanAmount LoanAmount { get; private set; }

        public virtual InterestRate InterestRate { get; private set; }

        public virtual LoanDate LoanDate { get; private set; }

        public virtual MaturityDate MaturityDate { get; private set; }

        public virtual PaymentsPerYear PaymentsPerYear { get; private set; }

        public virtual UserId UserId { get; private set; }

        public void UpdateLoanAmount(LoanAmount value)
        {
            LoanAmount = value ?? throw new ArgumentNullException("The loan amount for this loan agreement is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public void UpdateInterestRate(InterestRate value)
        {
            InterestRate = value ?? throw new ArgumentNullException("The interest rate is required; if zero then pass in 0.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public void UpdateLoanDate(LoanDate value)
        {
            LoanDate = value ?? throw new ArgumentNullException("The loan agreement date is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public void UpdateMaturityDate(MaturityDate value)
        {
            MaturityDate = value ?? throw new ArgumentNullException("The loan maturity date is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public void UpdatePaymentsPerYear(PaymentsPerYear value)
        {
            PaymentsPerYear = value ?? throw new ArgumentNullException("The number of loan payments per year is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public void UpdateUserId(UserId value)
        {
            UserId = value ?? throw new ArgumentNullException("The user id is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual IReadOnlyList<LoanPayment> LoanPayments => _loanPayments.ToList();

        public void AddLoanPayment(LoanPayment payment)
        {
            //TODO check for duplicate loan payment
            _loanPayments.Add(payment);
        }

        public void UpdateLoanPayment(LoanPayment payment)
        {
            string errMsg = $"Update failed, a loan payment with id '{payment.Id}' could not be found!";

            LoanPayment found =
                ((List<LoanPayment>)LoanPayments).Find(p => p.Id == payment.Id)
                    ?? throw new InvalidOperationException(errMsg);

            found.UpdateLoanInterestAmount(payment.LoanInterestAmount);
            found.UpdatePaymentNumber(payment.PaymentNumber);
            found.UpdatePaymentDueDate(payment.PaymentDueDate);
            found.UpdateLoanPrincipalAmount(payment.LoanPrincipalAmount);
            found.UpdateLoanInterestAmount(payment.LoanInterestAmount);
            found.UpdateLoanPrincipalRemaining(payment.LoanPrincipalRemaining);
            found.UpdateUserId(payment.UserId);
        }

        public void DeleteLoanPayment(Guid loanPaymentId)
        {
            string errMsg = $"Delete failed, a loan payment with id '{loanPaymentId}' could not be found!";

            LoanPayment found =
                ((List<LoanPayment>)LoanPayments).Find(p => p.Id == loanPaymentId)
                    ?? throw new InvalidOperationException(errMsg);

            _loanPayments.Remove(found);
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