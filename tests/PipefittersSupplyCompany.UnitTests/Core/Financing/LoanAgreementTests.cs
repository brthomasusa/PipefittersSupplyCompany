using System;
using Xunit;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;
using PipefittersSupplyCompany.Core.Financing.FinancierAggregate;
using PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate;

namespace PipefittersSupplyCompany.UnitTests.Core.Financing
{
    public class LoanAgreementTests
    {
        [Fact]
        public void ShouldReturn_ValidExternalAgent()
        {
            var result = new EconomicEvent(Guid.NewGuid(), EventType.CashReceiptFromLoanAgreement);

            Assert.IsType<EconomicEvent>(result);
        }

        [Fact]
        public void ShouldRaiseError_InvalidAgentTypeId()
        {
            Assert.Throws<ArgumentException>(() => new EconomicEvent(Guid.NewGuid(), 0));
        }

        [Fact]
        public void ShouldReturn_NewLoanAgreement()
        {
            var economicEvent = new EconomicEvent(Guid.NewGuid(), EventType.CashReceiptFromLoanAgreement);
            Financier financier = GetFinancier();

            LoanAgreement agreement = new LoanAgreement
            (
                economicEvent,
                financier.Id,
                LoanAmount.Create(10000),
                InterestRate.Create(.006),
                LoanDate.Create(new DateTime(2020, 12, 31)),
                MaturityDate.Create(new DateTime(2021, 12, 31)),
                PaymentsPerYear.Create(12),
                UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
            );

            Assert.IsType<LoanAgreement>(agreement);
        }

        [Fact]
        public void ShouldRaiseError_InvalidLoanAmount()
        {
            var economicEvent = new EconomicEvent(Guid.NewGuid(), EventType.CashReceiptFromLoanAgreement);
            Financier financier = GetFinancier();

            Assert.Throws<ArgumentException>(() =>
            {
                new LoanAgreement
                (
                    economicEvent,
                    financier.Id,
                    LoanAmount.Create(0),
                    InterestRate.Create(.006),
                    LoanDate.Create(new DateTime(2020, 12, 31)),
                    MaturityDate.Create(new DateTime(2021, 12, 31)),
                    PaymentsPerYear.Create(12),
                    UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
                );
            });
        }

        [Fact]
        public void ShouldRaiseError_InvalidInterestRate()
        {
            var economicEvent = new EconomicEvent(Guid.NewGuid(), EventType.CashReceiptFromLoanAgreement);
            Financier financier = GetFinancier();

            Assert.Throws<ArgumentException>(() =>
            {
                new LoanAgreement
                (
                    economicEvent,
                    financier.Id,
                    LoanAmount.Create(10000),
                    InterestRate.Create(-1),
                    LoanDate.Create(new DateTime(2020, 12, 31)),
                    MaturityDate.Create(new DateTime(2021, 12, 31)),
                    PaymentsPerYear.Create(12),
                    UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
                );
            });
        }

        [Fact]
        public void ShouldRaiseError_DefaultLoanDate()
        {
            var economicEvent = new EconomicEvent(Guid.NewGuid(), EventType.CashReceiptFromLoanAgreement);
            Financier financier = GetFinancier();

            Assert.Throws<ArgumentNullException>(() =>
            {
                new LoanAgreement
                (
                    economicEvent,
                    financier.Id,
                    LoanAmount.Create(10000),
                    InterestRate.Create(.006),
                    LoanDate.Create(new DateTime()),
                    MaturityDate.Create(new DateTime(2021, 12, 31)),
                    PaymentsPerYear.Create(12),
                    UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
                );
            });
        }

        [Fact]
        public void ShouldRaiseError_DefaultMaturityDate()
        {
            var economicEvent = new EconomicEvent(Guid.NewGuid(), EventType.CashReceiptFromLoanAgreement);
            Financier financier = GetFinancier();

            Assert.Throws<ArgumentNullException>(() =>
            {
                new LoanAgreement
                (
                    economicEvent,
                    financier.Id,
                    LoanAmount.Create(10000),
                    InterestRate.Create(.006),
                    LoanDate.Create(new DateTime(2021, 12, 31)),
                    MaturityDate.Create(new DateTime()),
                    PaymentsPerYear.Create(12),
                    UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
                );
            });
        }

        [Fact]
        public void ShouldRaiseError_LoanDateGreaterThanMaturityDate()
        {
            var economicEvent = new EconomicEvent(Guid.NewGuid(), EventType.CashReceiptFromLoanAgreement);
            Financier financier = GetFinancier();

            Assert.Throws<ArgumentException>(() =>
            {
                new LoanAgreement
                (
                    economicEvent,
                    financier.Id,
                    LoanAmount.Create(10000),
                    InterestRate.Create(.006),
                    LoanDate.Create(new DateTime(2021, 12, 31)),
                    MaturityDate.Create(new DateTime(2021, 11, 30)),
                    PaymentsPerYear.Create(12),
                    UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
                );
            });
        }

        [Fact]
        public void ShouldRaiseError_InvalidPymtsPerYear()
        {
            var economicEvent = new EconomicEvent(Guid.NewGuid(), EventType.CashReceiptFromLoanAgreement);
            Financier financier = GetFinancier();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new LoanAgreement
                (
                    economicEvent,
                    financier.Id,
                    LoanAmount.Create(10000),
                    InterestRate.Create(.006),
                    LoanDate.Create(new DateTime(2020, 12, 31)),
                    MaturityDate.Create(new DateTime(2021, 12, 31)),
                    PaymentsPerYear.Create(0),
                    UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
                );
            });
        }

        private Financier GetFinancier() =>
            new Financier
            (
                new ExternalAgent(Guid.NewGuid(), AgentType.Financier),
                OrganizationName.Create("First Bank and Trust"),
                PhoneNumber.Create("555-555-5555"),
                IsActive.Create(true),
                new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
            );
    }
}