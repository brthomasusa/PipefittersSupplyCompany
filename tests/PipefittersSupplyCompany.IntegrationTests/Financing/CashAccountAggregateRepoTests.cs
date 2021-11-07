using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.Infrastructure.Persistence;
using PipefittersSupplyCompany.Infrastructure.Persistence.Repositories.Financing;
using PipefittersSupplyCompany.Core.Financing.CashAccountAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.IntegrationTests.Base;

namespace PipefittersSupplyCompany.IntegrationTests.Financing
{
    public class CashAccountAggregateRepoTests : IntegrationTestBaseEfCore
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICashAccountAggregateRepository _cashAcctRepo;

        public CashAccountAggregateRepoTests()
        {
            TestDataInitialization.InitializeData(_dbContext);
            _unitOfWork = new AppUnitOfWork(_dbContext);
            _cashAcctRepo = new CashAccountAggregateRepository(_dbContext);
        }

        [Fact]
        public async Task ShouldInsert_CashAccount_UsingCashAccountRepo()
        {
            CashAccount account = new CashAccount
            (
                CashAccountId.Create(new Guid("1e5b3dcf-9ffd-4671-95ee-373e4ca08804")),
                BankName.Create("ABCDEFG Banking Hair Stylist, Inc."),
                CashAccountName.Create("Entertainment"),
                CashAccountNumber.Create("12345-678987"),
                RoutingTransitNumber.Create("125478991"),
                DateOpened.Create(new DateTime(2021, 11, 6)),
                UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
            );

            await _cashAcctRepo.AddAsync(account);
            await _unitOfWork.Commit();

            var result = await _cashAcctRepo.Exists(account.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task ShouldUpdate_CashAccount_UsingCashAccountRepo()
        {
            CashAccount account = await _cashAcctRepo.GetByIdAsync(new Guid("765ec2b0-406a-4e42-b831-c9aa63800e76"));

            account.UpdateBankName(BankName.Create("Test Bank"));
            account.UpdateCashAccountName(CashAccountName.Create("Testing Account"));
            account.UpdateCashAccountNumber(CashAccountNumber.Create("9876543210"));
            account.UpdateRoutingTransitNumber(RoutingTransitNumber.Create("125478991"));
            account.UpdateDateOpened(DateOpened.Create(new DateTime(2021, 11, 7)));
            account.UpdateUserId(UserId.Create(UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))));

            await _unitOfWork.Commit();

            CashAccount result = await _cashAcctRepo.GetByIdAsync(new Guid("765ec2b0-406a-4e42-b831-c9aa63800e76"));

            Assert.Equal("Test Bank", result.BankName);
            Assert.Equal("Testing Account", result.CashAccountName);
            Assert.Equal("9876543210", result.CashAccountNumber);
            Assert.Equal("125478991", result.RoutingTransitNumber);
            Assert.Equal(new DateTime(2021, 11, 7), result.DateOpened);
            Assert.Equal(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"), result.UserId);
        }

        [Fact]
        public async void ShouldDelete_CashAccount_UsingCashAccountRepo()
        {
            CashAccount account = await _cashAcctRepo.GetByIdAsync(new Guid("765ec2b0-406a-4e42-b831-c9aa63800e76"));
            Assert.NotNull(account);

            _cashAcctRepo.Delete(account);
            await _unitOfWork.Commit();

            CashAccount result = await _cashAcctRepo.GetByIdAsync(new Guid("765ec2b0-406a-4e42-b831-c9aa63800e76"));

            Assert.Null(result);
        }

    }
}