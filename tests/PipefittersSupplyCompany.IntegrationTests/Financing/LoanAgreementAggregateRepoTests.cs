using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.Infrastructure.Persistence;
using PipefittersSupplyCompany.Infrastructure.Persistence.Repositories.Financing;
using PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.IntegrationTests.Base;

namespace PipefittersSupplyCompany.IntegrationTests.Financing
{
    public class LoanAgreementAggregateRepoTests : IntegrationTestBaseEfCore
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoanAgreementAggregateRepository _loanAgreementRepo;

        public LoanAgreementAggregateRepoTests()
        {
            TestDataInitialization.InitializeData(_dbContext);
            _unitOfWork = new AppUnitOfWork(_dbContext);
            _loanAgreementRepo = new LoanAgreementAggregateRepository(_dbContext);
        }

        [Fact]
        public async Task ShouldInsert_LoanAgreement_UsingLoanAgreementRepo()
        {
            LoanAgreement agreement = new LoanAgreement
            (
                new EconomicEvent(Guid.NewGuid(), EventType.CashReceiptFromLoanAgreement),
                new Guid("b49471a0-5c1e-4a4d-97e7-288fb0f6338a"),
                LoanAmount.Create(175000),
                InterestRate.Create(.0675),
                LoanDate.Create(new DateTime(2021, 11, 5)),
                MaturityDate.Create(new DateTime(2022, 11, 5)),
                PaymentsPerYear.Create(12),
                UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
            );

            await _loanAgreementRepo.AddAsync(agreement);
            await _unitOfWork.Commit();

            var result = await _loanAgreementRepo.Exists(agreement.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task ShouldUpdate_LoanAgreement_UsingLoanAgreementRepo()
        {
            LoanAgreement agreement = await _loanAgreementRepo.GetByIdAsync(new Guid("09b53ffb-9983-4cde-b1d6-8a49e785177f"));

            agreement.UpdateInterestRate(InterestRate.Create(.0975));
            agreement.UpdateLoanAmount(LoanAmount.Create(52128M));

            await _unitOfWork.Commit();

            LoanAgreement result = await _loanAgreementRepo.GetByIdAsync(new Guid("09b53ffb-9983-4cde-b1d6-8a49e785177f"));
            Assert.Equal(.0975, result.InterestRate);
            Assert.Equal(52128M, result.LoanAmount);
        }

        [Fact]
        public async void ShouldDelete_LoanAgreement_UsingLoanAgreementRepo()
        {
            LoanAgreement agreement = await _loanAgreementRepo.GetByIdAsync(new Guid("0a7181c0-3ce9-4981-9559-157fd8e09cfb"));
            Assert.NotNull(agreement);

            _loanAgreementRepo.Delete(agreement);
            await _unitOfWork.Commit();

            LoanAgreement result = await _loanAgreementRepo.GetByIdAsync(new Guid("0a7181c0-3ce9-4981-9559-157fd8e09cfb"));

            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldInsert_LoanAgreementAndLoanPymt_UsingLoanAgreementAggregate()
        {
            LoanAgreement agreement = new LoanAgreement
            (
                new EconomicEvent(Guid.NewGuid(), EventType.CashReceiptFromLoanAgreement),
                new Guid("b49471a0-5c1e-4a4d-97e7-288fb0f6338a"),
                LoanAmount.Create(175000),
                InterestRate.Create(.0675),
                LoanDate.Create(new DateTime(2021, 11, 5)),
                MaturityDate.Create(new DateTime(2022, 11, 5)),
                PaymentsPerYear.Create(12),
                UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
            );

            LoanPayment loanPayment = new LoanPayment
            (
                new EconomicEvent(Guid.NewGuid(), EventType.CashDisbursementForLoanPayment),
                agreement,
                PaymentNumber.Create(1),
                PaymentDueDate.Create(new DateTime(2021, 12, 5)),
                LoanPrincipalAmount.Create(14135),
                LoanInterestAmount.Create(984),
                LoanPrincipalRemaining.Create(160862),
                UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
            );

            agreement.AddLoanPayment(loanPayment);
            await _loanAgreementRepo.AddAsync(agreement);
            await _unitOfWork.Commit();

            var result = await _loanAgreementRepo.Exists(agreement.Id);

            Assert.True(result);    //TODO Test navigation to LoanPayment
        }

        [Fact]
        public async Task ShouldInsert_LoanPymt_UsingLoanAgreementAggregate()
        {
            LoanAgreement agreement = await _loanAgreementRepo.GetByIdAsync(new Guid("0a7181c0-3ce9-4981-9559-157fd8e09cfb"));

            EconomicEvent economicEvent = new EconomicEvent(Guid.NewGuid(), EventType.CashDisbursementForLoanPayment);
            await _loanAgreementRepo.AddEconomicEventAsync(economicEvent);

            LoanPayment loanPayment = new LoanPayment
            (
                economicEvent,
                agreement,
                PaymentNumber.Create(1),
                PaymentDueDate.Create(new DateTime(2021, 12, 5)),
                LoanPrincipalAmount.Create(14135.13M),
                LoanInterestAmount.Create(984),
                LoanPrincipalRemaining.Create(160862.13M),
                UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
            );

            agreement.AddLoanPayment(loanPayment);
            _loanAgreementRepo.Update(agreement);
            await _unitOfWork.Commit();

            LoanPayment result = agreement.LoanPayments.FirstOrDefault(p => p.Id == loanPayment.EconomicEvent.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldUpdate_LoanPayment_UsingLoanAgreementAggregate()
        {
            LoanAgreement agreement = await _loanAgreementRepo.GetByIdAsync(new Guid("1511c20b-6df0-4313-98a5-7c3561757dc2"));

            LoanPayment payment = agreement.LoanPayments.FirstOrDefault(p => p.Id == new Guid("2205ebde-58dc-4af7-958b-9124d9645b38"));
            payment.UpdatePaymentDueDate(PaymentDueDate.Create(new DateTime(2021, 1, 10)));
            payment.UpdateLoanInterestAmount(LoanInterestAmount.Create(360.07M));

            agreement.UpdateLoanPayment(payment);
            await _unitOfWork.Commit();

            LoanPayment result = agreement.LoanPayments.FirstOrDefault(p => p.Id == new Guid("2205ebde-58dc-4af7-958b-9124d9645b38"));

            Assert.Equal(new DateTime(2021, 1, 10), result.PaymentDueDate);
            Assert.Equal(360.07M, result.LoanInterestAmount);
        }

        [Fact]
        public async Task ShouldDelete_LoanPayment_UsingLoanAgreementAggregate()
        {
            LoanAgreement agreement = await _loanAgreementRepo.GetByIdAsync(new Guid("1511c20b-6df0-4313-98a5-7c3561757dc2"));

            LoanPayment payment = agreement.LoanPayments.FirstOrDefault(p => p.Id == new Guid("409e60dc-bbe6-4ca9-95c2-ebf6886e8c4c"));

            agreement.DeleteLoanPayment(payment.Id);
            await _unitOfWork.Commit();

            LoanPayment result = agreement.LoanPayments.FirstOrDefault(p => p.Id == new Guid("409e60dc-bbe6-4ca9-95c2-ebf6886e8c4c"));

            Assert.Null(result);
        }
    }
}