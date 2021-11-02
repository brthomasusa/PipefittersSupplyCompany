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
        public void ShouldRaiseError_InvalidAgentTypeId()
        {
            Assert.Throws<ArgumentException>(() => new EconomicEvent(Guid.NewGuid(), 0));
        }

        [Fact]
        public void ShouldReturn_NewLoanAgreement()
        {
            Financier financier = GetFinancier();

            LoanAgreement agreement = new LoanAgreement
            (
                Guid.NewGuid(),
                financier,
                LoanAmount.Create(10000),
                InterestRate.Create(.006),
                LoanDate.Create(new DateTime(2020, 12, 31)),
                MaturityDate.Create(new DateTime(2021, 12, 31)),
                PaymentsPerYear.Create(12),
                new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
            );

            Assert.IsType<LoanAgreement>(agreement);
        }

        [Fact]
        public void ShouldRaiseError_InvalidLoanAmount()
        {
            Financier financier = GetFinancier();

            Assert.Throws<ArgumentException>(() =>
            {
                new LoanAgreement
                (
                    Guid.NewGuid(),
                    financier,
                    LoanAmount.Create(0),
                    InterestRate.Create(.006),
                    LoanDate.Create(new DateTime(2020, 12, 31)),
                    MaturityDate.Create(new DateTime(2021, 12, 31)),
                    PaymentsPerYear.Create(12),
                    new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
                );
            });
        }

        [Fact]
        public void ShouldRaiseError_InvalidInterestRate()
        {
            Financier financier = GetFinancier();

            Assert.Throws<ArgumentException>(() =>
            {
                new LoanAgreement
                (
                    Guid.NewGuid(),
                    financier,
                    LoanAmount.Create(10000),
                    InterestRate.Create(-1),
                    LoanDate.Create(new DateTime(2020, 12, 31)),
                    MaturityDate.Create(new DateTime(2021, 12, 31)),
                    PaymentsPerYear.Create(12),
                    new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
                );
            });
        }

        [Fact]
        public void ShouldRaiseError_DefaultLoanDate()
        {
            Financier financier = GetFinancier();

            Assert.Throws<ArgumentNullException>(() =>
            {
                new LoanAgreement
                (
                    Guid.NewGuid(),
                    financier,
                    LoanAmount.Create(10000),
                    InterestRate.Create(.006),
                    LoanDate.Create(new DateTime()),
                    MaturityDate.Create(new DateTime(2021, 12, 31)),
                    PaymentsPerYear.Create(12),
                    new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
                );
            });
        }

        [Fact]
        public void ShouldRaiseError_DefaultMaturityDate()
        {
            Financier financier = GetFinancier();

            Assert.Throws<ArgumentNullException>(() =>
            {
                new LoanAgreement
                (
                    Guid.NewGuid(),
                    financier,
                    LoanAmount.Create(10000),
                    InterestRate.Create(.006),
                    LoanDate.Create(new DateTime(2021, 12, 31)),
                    MaturityDate.Create(new DateTime()),
                    PaymentsPerYear.Create(12),
                    new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
                );
            });
        }

        [Fact]
        public void ShouldRaiseError_LoanDateGreaterThanMaturityDate()
        {
            Financier financier = GetFinancier();

            Assert.Throws<ArgumentException>(() =>
            {
                new LoanAgreement
                (
                    Guid.NewGuid(),
                    financier,
                    LoanAmount.Create(10000),
                    InterestRate.Create(.006),
                    LoanDate.Create(new DateTime(2021, 12, 31)),
                    MaturityDate.Create(new DateTime(2021, 11, 30)),
                    PaymentsPerYear.Create(12),
                    new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
                );
            });
        }

        [Fact]
        public void ShouldRaiseError_InvalidPymtsPerYear()
        {
            Financier financier = GetFinancier();

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new LoanAgreement
                (
                    Guid.NewGuid(),
                    financier,
                    LoanAmount.Create(10000),
                    InterestRate.Create(.006),
                    LoanDate.Create(new DateTime(2020, 12, 31)),
                    MaturityDate.Create(new DateTime(2021, 12, 31)),
                    PaymentsPerYear.Create(0),
                    new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
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