using System;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate
{
    public class LoanPayment : Entity<Guid>
    {
        protected LoanPayment() { }

        public LoanPayment
        (
            EconomicEvent economicEvent,
            LoanAgreement loanAgreement,
            PaymentNumber paymentNumber,
            PaymentDueDate paymentDueDate,
            LoanPrincipalAmount principalAmount,
            LoanInterestAmount interestAmount,
            LoanPrincipalRemaining remainPrincipal,
            UserId userID
        )
        {
            EconomicEvent = economicEvent ?? throw new ArgumentNullException("The economic event is required.");

            Id = economicEvent.Id;
            LoanAgreement = loanAgreement;
            LoanId = LoanId;
            PaymentNumber = paymentNumber;
            PaymentDueDate = paymentDueDate;
            LoanPrincipalAmount = principalAmount;
            LoanInterestAmount = interestAmount;
            LoanPrincipalRemaining = remainPrincipal;
            UserId = userID;

            CheckValidity();
        }

        public virtual EconomicEvent EconomicEvent { get; private set; }

        public Guid LoanId { get; private set; }
        public virtual LoanAgreement LoanAgreement { get; private set; }

        public virtual PaymentNumber PaymentNumber { get; private set; }
        public void UpdatePaymentNumber(PaymentNumber value)
        {
            PaymentNumber = value ?? throw new ArgumentNullException("The payment number is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual PaymentDueDate PaymentDueDate { get; private set; }
        public void UpdatePaymentDueDate(PaymentDueDate value)
        {
            PaymentDueDate = value ?? throw new ArgumentNullException("The payment due date is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual LoanPrincipalAmount LoanPrincipalAmount { get; private set; }
        public void UpdateLoanPrincipalAmount(LoanPrincipalAmount value)
        {
            LoanPrincipalAmount = value ?? throw new ArgumentNullException("The loan principal amount is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual LoanInterestAmount LoanInterestAmount { get; private set; }
        public void UpdateLoanInterestAmount(LoanInterestAmount value)
        {
            LoanInterestAmount = value ?? throw new ArgumentNullException("The loan principal amount is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual LoanPrincipalRemaining LoanPrincipalRemaining { get; private set; }
        public void UpdateLoanPrincipalRemaining(LoanPrincipalRemaining value)
        {
            LoanPrincipalRemaining = value ?? throw new ArgumentNullException("The loan principal balance remaining is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        public virtual UserId UserId { get; private set; }
        public void UpdateUserId(UserId value)
        {
            UserId = value ?? throw new ArgumentNullException("The user id is required.");
            UpdateLastModifiedDate();
            CheckValidity();
        }

        protected override void CheckValidity()
        {
            if (EconomicEvent.EventType is not EventType.CashDisbursementForLoanPayment)
            {
                throw new ArgumentException("Invalid EconomicEvent type; it must be 'EventType.CashDisbursementForLoanPayment'.");
            }

            if (PaymentNumber > (MonthDiff(LoanAgreement.LoanDate, LoanAgreement.MaturityDate)))
            {
                throw new ArgumentOutOfRangeException("Payment number can not be greater than the length (in months) of the loan agreement.", nameof(PaymentNumber));
            }

        }

        private int MonthDiff(DateTime startDate, DateTime endDate)
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