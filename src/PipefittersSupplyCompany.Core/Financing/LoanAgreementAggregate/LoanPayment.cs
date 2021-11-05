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
            LoanId = LoanId.Create(loanAgreement.Id);
            PaymentNumber = paymentNumber;
            PaymentDueDate = paymentDueDate;
            LoanPrincipalAmount = principalAmount;
            LoanInterestAmount = interestAmount;
            LoanPrincipalRemaining = remainPrincipal;
            UserId = userID;

            CheckValidity();
        }

        public virtual EconomicEvent EconomicEvent { get; private set; }

        public virtual LoanAgreement LoanAgreement { get; private set; }

        public virtual LoanId LoanId { get; private set; }

        public virtual PaymentNumber PaymentNumber { get; private set; }

        public virtual PaymentDueDate PaymentDueDate { get; private set; }

        public virtual LoanPrincipalAmount LoanPrincipalAmount { get; private set; }

        public virtual LoanInterestAmount LoanInterestAmount { get; private set; }

        public virtual LoanPrincipalRemaining LoanPrincipalRemaining { get; private set; }

        public virtual UserId UserId { get; private set; }

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