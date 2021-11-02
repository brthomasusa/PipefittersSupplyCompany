using System;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.SharedKernel.Interfaces;
using PipefittersSupplyCompany.Core.Financing.FinancierAggregate;

namespace PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate
{
    public class LoanPymtSchedule : Entity<Guid>
    {
        protected LoanPymtSchedule() { }

        public LoanPymtSchedule
        (
            EconomicEvent economicEvent,
            LoanAgreement loanAgreement,
            PaymentNumber paymentNumber,
            PaymentDueDate paymentDueDate,
            LoanPrincipalAmount principalAmount,
            LoanInterestAmount interestAmount,
            Guid userID
        )
        {
            EconomicEvent = economicEvent ?? throw new ArgumentNullException("The economic event is required.");

            Id = economicEvent.Id;
            LoanAgreement = loanAgreement;
            PaymentNumber = paymentNumber;
            PaymentDueDate = paymentDueDate;
            LoanPrincipalAmount = principalAmount;
            LoanInterestAmount = interestAmount;

            if (userID == default)
            {
                throw new ArgumentNullException("The user id (the employee creating this record) parameter is required.");
            }
            UserId = userID;
        }

        public virtual EconomicEvent EconomicEvent { get; private set; }

        public virtual LoanAgreement LoanAgreement { get; private set; }

        public virtual PaymentNumber PaymentNumber { get; private set; }

        public virtual PaymentDueDate PaymentDueDate { get; set; }

        public virtual LoanPrincipalAmount LoanPrincipalAmount { get; set; }

        public virtual LoanInterestAmount LoanInterestAmount { get; set; }

        public Guid UserId { get; private set; }

        protected override void CheckValidity()
        {
            if (EconomicEvent.EventType is not EventType.LoanPayment)
            {
                throw new ArgumentException("Invalid EconomicEvent type; it must be 'EventType.LoanPayment'.");
            }

        }
    }
}