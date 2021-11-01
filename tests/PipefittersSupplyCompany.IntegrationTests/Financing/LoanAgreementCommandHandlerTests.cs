using System;
using System.Threading.Tasks;
using Xunit;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.Infrastructure.Persistence;
using PipefittersSupplyCompany.Infrastructure.Persistence.Repositories.Financing;
using PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing.LoanAgreements;
using PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate;
using PipefittersSupplyCompany.IntegrationTests.Base;


namespace PipefittersSupplyCompany.IntegrationTests.Financing
{
    public class LoanAgreementCommandHandlerTests : IntegrationTestBaseEfCore
    {
        private readonly ICommandHandler _cmdHdlr;

        public LoanAgreementCommandHandlerTests()
        {
            TestDataInitialization.InitializeData(_dbContext);
            IUnitOfWork unitOfWork = new AppUnitOfWork(_dbContext);
            IFinancierAggregateRepository financierRepo = new FinancierAggregateRepository(_dbContext);
            ILoanAgreementAggregateRepository loanAgreementRepository = new LoanAgreementAggregateRepository(_dbContext);

            _cmdHdlr = new LoanAgreementAggregateCommandHandler(financierRepo, loanAgreementRepository, unitOfWork);
        }

        [Fact]
        public async Task ShouldInsert_LoanAgreement_UsingCreateLoanAgreementInfoWriteModel()
        {
            Guid id = Guid.NewGuid();
            var model = new CreateLoanAgreementInfo
            {
                Id = id,
                FinancierId = new Guid("12998229-7ede-4834-825a-0c55bde75695"),
                LoanAmount = 10000,
                InterestRate = .006,
                LoanDate = new DateTime(2021, 1, 5),
                MaturityDate = new DateTime(2022, 1, 5),
                PaymentsPerYear = 12,
                UserId = new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
            };

            await _cmdHdlr.Handle(model);

            LoanAgreement result = await _dbContext.LoanAgreements.FindAsync(id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldRaiseError_CreateLoanAgreement_UsingIDOfExistingLoan()
        {
            var model = new CreateLoanAgreementInfo
            {
                Id = new Guid("1511c20b-6df0-4313-98a5-7c3561757dc2"),
                FinancierId = new Guid("12998229-7ede-4834-825a-0c55bde75695"),
                LoanAmount = 10000,
                InterestRate = .006,
                LoanDate = new DateTime(2021, 1, 5),
                MaturityDate = new DateTime(2022, 1, 5),
                PaymentsPerYear = 12,
                UserId = new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
            };

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _cmdHdlr.Handle(model));
        }

        [Fact]
        public async Task ShouldRaiseError_CreateLoanAgreement_UsingInvalidFinancierID()
        {
            var model = new CreateLoanAgreementInfo
            {
                Id = Guid.NewGuid(),
                FinancierId = new Guid("44448229-7ede-4834-825a-0c55bde75695"),
                LoanAmount = 10000,
                InterestRate = .006,
                LoanDate = new DateTime(2021, 1, 5),
                MaturityDate = new DateTime(2022, 1, 5),
                PaymentsPerYear = 12,
                UserId = new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
            };

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _cmdHdlr.Handle(model));
        }

        [Fact]
        public async Task ShouldUpdate_LoanAgreement_UsingEditLoanAgreementInfoWriteModel()
        {
            var model = new EditLoanAgreementInfo
            {
                Id = new Guid("1511c20b-6df0-4313-98a5-7c3561757dc2"),
                LoanAmount = 70000,
                InterestRate = .12,
                LoanDate = new DateTime(2021, 1, 5),
                MaturityDate = new DateTime(2023, 1, 5),
                PaymentsPerYear = 24,
                UserId = new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
            };

            await _cmdHdlr.Handle(model);
            LoanAgreement result = await _dbContext.LoanAgreements.FindAsync(model.Id);

            Assert.Equal(model.LoanAmount, result.LoanAmount);
            Assert.Equal(model.InterestRate, result.InterestRate);
            Assert.Equal(model.PaymentsPerYear, result.PaymentsPerYear);
            Assert.Equal(model.MaturityDate, result.MaturityDate);
        }

        [Fact]
        public async Task ShouldRaiseError_UpdateLoanAgreement_UsingInvalidLoanID()
        {
            var model = new EditLoanAgreementInfo
            {
                Id = new Guid("0000c20b-6df0-4313-98a5-7c3561757dc2"),
                LoanAmount = 70000,
                InterestRate = .12,
                LoanDate = new DateTime(2021, 1, 5),
                MaturityDate = new DateTime(2023, 1, 5),
                PaymentsPerYear = 24,
                UserId = new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
            };

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _cmdHdlr.Handle(model));
        }

        [Fact]
        public async Task ShouldDelete_LoanAgreement_UsingDeleteLoanAgreementInfoWriteModel()
        {
            var model = new DeleteLoanAgreementInfo
            {
                Id = new Guid("1511c20b-6df0-4313-98a5-7c3561757dc2"),
                UserId = new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
            };

            await _cmdHdlr.Handle(model);
            LoanAgreement result = await _dbContext.LoanAgreements.FindAsync(model.Id);

            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldRaiseError_DeleteLoanAgreement_UsingInvalidLoanID()
        {
            var model = new DeleteLoanAgreementInfo
            {
                Id = new Guid("0000c20b-6df0-4313-98a5-7c3561757dc2"),
                UserId = new Guid("660bb318-649e-470d-9d2b-693bfb0b2744")
            };

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _cmdHdlr.Handle(model));
        }
    }
}