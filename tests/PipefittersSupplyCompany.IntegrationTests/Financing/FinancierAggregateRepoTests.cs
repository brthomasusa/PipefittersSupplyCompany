using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.Infrastructure.Persistence;
using PipefittersSupplyCompany.Infrastructure.Persistence.Repositories.Financing;

using PipefittersSupplyCompany.Core.Financing.FinancierAggregate;
using PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;
using PipefittersSupplyCompany.IntegrationTests.Base;

namespace PipefittersSupplyCompany.IntegrationTests.Financing
{
    public class FinancierAggregateRepoTests : IntegrationTestBaseEfCore
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IFinancierAggregateRepository _financierRepo;

        public FinancierAggregateRepoTests()
        {
            TestDataInitialization.InitializeData(_dbContext);
            _unitOfWork = new AppUnitOfWork(_dbContext);
            _financierRepo = new FinancierAggregateRepository(_dbContext);
        }

        [Fact]
        public async Task ShouldUpdate_LoanAgreement_UsingFinancierRepo()
        {
            Financier financier = await _financierRepo.GetByIdAsync(new Guid("12998229-7ede-4834-825a-0c55bde75695"));
            LoanAgreement agreement = financier.LoanAgreements.FirstOrDefault(p => p.Id == new Guid("41ca2b0a-0ed5-478b-9109-5dfda5b2eba1"));


            Assert.NotNull(agreement);
        }
    }
}