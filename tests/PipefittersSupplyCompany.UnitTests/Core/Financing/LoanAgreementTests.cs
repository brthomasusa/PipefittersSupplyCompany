using System;
using System.Collections.Generic;
using System.Linq;
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
            Financier financier = GetFinancier();

            LoanAgreement agreement = new LoanAgreement
            (
                new EconomicEvent(Guid.NewGuid(), EventType.CashReceiptFromLoanAgreement),
                FinancierId.Create(financier.Id),
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
                    FinancierId.Create(financier.Id),
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
                    FinancierId.Create(financier.Id),
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
                    FinancierId.Create(financier.Id),
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
                    FinancierId.Create(financier.Id),
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
                    FinancierId.Create(financier.Id),
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
                    FinancierId.Create(financier.Id),
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

        private List<LoanPayment> GetLoanPayments(LoanAgreement agreement) =>
            new List<LoanPayment>
            {
                new LoanPayment
                (
                    new EconomicEvent(Guid.NewGuid(), EventType.CashDisbursementForLoanPayment),
                    agreement,
                    PaymentNumber.Create(1),
                    PaymentDueDate.Create(new DateTime(2021, 12, 13)),
                    LoanPrincipalAmount.Create(975.04M),
                    LoanInterestAmount.Create(55M),
                    LoanPrincipalRemaining.Create(11024.96M),
                    UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
                ),
                new LoanPayment
                (
                    new EconomicEvent(Guid.NewGuid(), EventType.CashDisbursementForLoanPayment),
                    agreement,
                    PaymentNumber.Create(2),
                    PaymentDueDate.Create(new DateTime(2022, 1, 13)),
                    LoanPrincipalAmount.Create(979.51M),
                    LoanInterestAmount.Create(50.53M),
                    LoanPrincipalRemaining.Create(10045.45M),
                    UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
                ),
                new LoanPayment
                (
                    new EconomicEvent(Guid.NewGuid(), EventType.CashDisbursementForLoanPayment),
                    agreement,
                    PaymentNumber.Create(3),
                    PaymentDueDate.Create(new DateTime(2022, 2, 13)),
                    LoanPrincipalAmount.Create(984.00M),
                    LoanInterestAmount.Create(46.04M),
                    LoanPrincipalRemaining.Create(9061.45M),
                    UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
                ),
                new LoanPayment
                (
                    new EconomicEvent(Guid.NewGuid(), EventType.CashDisbursementForLoanPayment),
                    agreement,
                    PaymentNumber.Create(4),
                    PaymentDueDate.Create(new DateTime(2022, 3, 13)),
                    LoanPrincipalAmount.Create(988.51M),
                    LoanInterestAmount.Create(41.53M),
                    LoanPrincipalRemaining.Create(8072.94M),
                    UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
                ),
                new LoanPayment
                (
                    new EconomicEvent(Guid.NewGuid(), EventType.CashDisbursementForLoanPayment),
                    agreement,
                    PaymentNumber.Create(5),
                    PaymentDueDate.Create(new DateTime(2022, 4, 13)),
                    LoanPrincipalAmount.Create(993.04M),
                    LoanInterestAmount.Create(37.00M),
                    LoanPrincipalRemaining.Create(7079.90M),
                    UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
                ),
                new LoanPayment
                (
                    new EconomicEvent(Guid.NewGuid(), EventType.CashDisbursementForLoanPayment),
                    agreement,
                    PaymentNumber.Create(6),
                    PaymentDueDate.Create(new DateTime(2022, 5, 13)),
                    LoanPrincipalAmount.Create(997.59M),
                    LoanInterestAmount.Create(32.45M),
                    LoanPrincipalRemaining.Create(6082.31M),
                    UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
                ),
                new LoanPayment
                (
                    new EconomicEvent(Guid.NewGuid(), EventType.CashDisbursementForLoanPayment),
                    agreement,
                    PaymentNumber.Create(7),
                    PaymentDueDate.Create(new DateTime(2022, 6, 13)),
                    LoanPrincipalAmount.Create(1002.16M),
                    LoanInterestAmount.Create(27.88M),
                    LoanPrincipalRemaining.Create(5080.14M),
                    UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
                ),
                new LoanPayment
                (
                    new EconomicEvent(Guid.NewGuid(), EventType.CashDisbursementForLoanPayment),
                    agreement,
                    PaymentNumber.Create(8),
                    PaymentDueDate.Create(new DateTime(2022, 7, 13)),
                    LoanPrincipalAmount.Create(1006.76M),
                    LoanInterestAmount.Create(23.28M),
                    LoanPrincipalRemaining.Create(4073.38M),
                    UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
                ),
                new LoanPayment
                (
                    new EconomicEvent(Guid.NewGuid(), EventType.CashDisbursementForLoanPayment),
                    agreement,
                    PaymentNumber.Create(9),
                    PaymentDueDate.Create(new DateTime(2022, 8, 13)),
                    LoanPrincipalAmount.Create(1011.37M),
                    LoanInterestAmount.Create(18.67M),
                    LoanPrincipalRemaining.Create(3062.01M),
                    UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
                ),
                new LoanPayment
                (
                    new EconomicEvent(Guid.NewGuid(), EventType.CashDisbursementForLoanPayment),
                    agreement,
                    PaymentNumber.Create(10),
                    PaymentDueDate.Create(new DateTime(2022, 9, 13)),
                    LoanPrincipalAmount.Create(1016.01M),
                    LoanInterestAmount.Create(14.03M),
                    LoanPrincipalRemaining.Create(2046.01M),
                    UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
                ),
                new LoanPayment
                (
                    new EconomicEvent(Guid.NewGuid(), EventType.CashDisbursementForLoanPayment),
                    agreement,
                    PaymentNumber.Create(11),
                    PaymentDueDate.Create(new DateTime(2022, 10, 13)),
                    LoanPrincipalAmount.Create(1020.66M),
                    LoanInterestAmount.Create(9.38M),
                    LoanPrincipalRemaining.Create(1025.34M),
                    UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
                ),
                new LoanPayment
                (
                    new EconomicEvent(Guid.NewGuid(), EventType.CashDisbursementForLoanPayment),
                    agreement,
                    PaymentNumber.Create(12),
                    PaymentDueDate.Create(new DateTime(2022, 11, 13)),
                    LoanPrincipalAmount.Create(1025.34M),
                    LoanInterestAmount.Create(4.70M),
                    LoanPrincipalRemaining.Create(0),
                    UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
                )
            };

        private LoanAgreement GetLoanAgreementWithLoanPayments()
        {
            Financier financier = GetFinancier();

            LoanAgreement agreement = new LoanAgreement
            (
                new EconomicEvent(Guid.NewGuid(), EventType.CashReceiptFromLoanAgreement),
                FinancierId.Create(financier.Id),
                LoanAmount.Create(10000),
                InterestRate.Create(.006),
                LoanDate.Create(new DateTime(2020, 12, 31)),
                MaturityDate.Create(new DateTime(2021, 12, 31)),
                PaymentsPerYear.Create(12),
                UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
            );

            return agreement;
        }
    }
}