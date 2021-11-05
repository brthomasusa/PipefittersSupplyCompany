using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.Infrastructure.Persistence;
using PipefittersSupplyCompany.Infrastructure.Persistence.Repositories.Financing;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;
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
        public async Task ShouldInsert_EconomicEventAndLoanAgreement_UsingRepo()
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
        public async Task ShouldInsert_LoanPymt_UsingLoanAgreementRepo()
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

            Assert.True(result);
        }

    }
}