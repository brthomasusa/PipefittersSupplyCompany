using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.Infrastructure.Persistence;
using PipefittersSupplyCompany.Infrastructure.Persistence.Repositories.Financing;
using PipefittersSupplyCompany.Core.Financing.StockSubscriptionAggregate;
using PipefittersSupplyCompany.Core.Financing.FinancierAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.IntegrationTests.Base;

namespace PipefittersSupplyCompany.IntegrationTests.Financing
{
    public class StockSubscriptionAggregateRepoTests : IntegrationTestBaseEfCore
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockSubscriptionAggregateRepository _stockSubscriptionRepo;

        public StockSubscriptionAggregateRepoTests()
        {
            TestDataInitialization.InitializeData(_dbContext);
            _unitOfWork = new AppUnitOfWork(_dbContext);
            _stockSubscriptionRepo = new StockSubscriptionAggregateRepository(_dbContext);
        }

        [Fact]
        public async Task ShouldInsert_StockSubscription_UsingStockSubscriptionRepo()
        {
            StockSubscription subscription = new StockSubscription
            (
                new EconomicEvent(Guid.NewGuid(), EventType.CashReceiptFromStockSubscription),
                FinancierId.Create(new Guid("84164388-28ff-4b47-bd63-dd9326d32236")),
                StockIssueDate.Create(new DateTime(2021, 11, 9)),
                SharesIssured.Create(33333),
                PricePerShare.Create(.33M),
                UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
            );

            await _stockSubscriptionRepo.AddAsync(subscription);
            await _unitOfWork.Commit();

            var result = await _stockSubscriptionRepo.Exists(subscription.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task ShouldUpdate_StockSubscription_UsingStockSubscrptionRepo()
        {
            StockSubscription stockSubscription = await _stockSubscriptionRepo.GetByIdAsync(new Guid("5997f125-bfca-4540-a144-01e444f6dc25"));

            stockSubscription.UpdatePricePerShare(PricePerShare.Create(.57M));
            stockSubscription.UpdateSharesIssured(SharesIssured.Create(666));
            stockSubscription.UpdateStockIssueDate(StockIssueDate.Create(new DateTime(2021, 11, 10)));

            await _unitOfWork.Commit();

            StockSubscription result = await _stockSubscriptionRepo.GetByIdAsync(new Guid("5997f125-bfca-4540-a144-01e444f6dc25"));
            Assert.Equal(.57M, result.PricePerShare);
            Assert.Equal(666, result.SharesIssured);
            Assert.Equal(new DateTime(2021, 11, 10), result.StockIssueDate);
        }

        [Fact]
        public async void ShouldDelete_StockSubscription_UsingStockSubscriptionRepo()
        {
            StockSubscription stockSubscription = await _stockSubscriptionRepo.GetByIdAsync(new Guid("5997f125-bfca-4540-a144-01e444f6dc25"));
            Assert.NotNull(stockSubscription);

            _stockSubscriptionRepo.Delete(stockSubscription);
            await _unitOfWork.Commit();

            StockSubscription result = await _stockSubscriptionRepo.GetByIdAsync(new Guid("5997f125-bfca-4540-a144-01e444f6dc25"));

            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldInsert_StockSubscriptionAndDividendDeclaration_UsingStockSubscriptionAggregate()
        {
            StockSubscription stockSubscription = new StockSubscription
            (
                new EconomicEvent(Guid.NewGuid(), EventType.CashReceiptFromStockSubscription),
                FinancierId.Create(new Guid("84164388-28ff-4b47-bd63-dd9326d32236")),
                StockIssueDate.Create(new DateTime(2021, 11, 9)),
                SharesIssured.Create(33333),
                PricePerShare.Create(.33M),
                UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
            );

            DividendPayment dividendPayment = new DividendPayment
            (
                new EconomicEvent(Guid.NewGuid(), EventType.CashDisbursementForDividentPayment),
                stockSubscription,
                DividendDeclarationDate.Create(new DateTime(2021, 11, 22)),
                DividendPerShare.Create(.02M),
                UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
            );

            stockSubscription.AddDividendPaymentRate(dividendPayment);
            await _stockSubscriptionRepo.AddAsync(stockSubscription);
            await _unitOfWork.Commit();

            var result = await _stockSubscriptionRepo.Exists(stockSubscription.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task ShouldInsert_DividendDeclaration_UsingStockSubscriptionAggregate()
        {
            StockSubscription stockSubscription = await _stockSubscriptionRepo.GetByIdAsync(new Guid("5997f125-bfca-4540-a144-01e444f6dc25"));

            EconomicEvent economicEvent = new EconomicEvent(Guid.NewGuid(), EventType.CashDisbursementForDividentPayment);
            await _stockSubscriptionRepo.AddEconomicEventAsync(economicEvent);

            DividendPayment dividendPayment = new DividendPayment
            (
                economicEvent,
                stockSubscription,
                DividendDeclarationDate.Create(new DateTime(2021, 11, 22)),
                DividendPerShare.Create(.02M),
                UserId.Create(new Guid("660bb318-649e-470d-9d2b-693bfb0b2744"))
            );

            stockSubscription.AddDividendPaymentRate(dividendPayment);
            _stockSubscriptionRepo.Update(stockSubscription);
            await _unitOfWork.Commit();

            DividendPayment result = stockSubscription.DividendPaymentRates.FirstOrDefault(p => p.Id == dividendPayment.EconomicEvent.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldUpdate_DividendDeclaration_UsingStockSubscriptionAggregate()
        {
            StockSubscription stockSubscription = await _stockSubscriptionRepo.GetByIdAsync(new Guid("264632b4-20bd-473f-9a9b-dd6f3b6ddbac"));

            DividendPayment dividendPayment = stockSubscription.DividendPaymentRates.FirstOrDefault(p => p.Id == new Guid("24d6936a-beb5-451b-a950-0f30e3ad463d"));
            dividendPayment.UpdateDividendDeclarationDate(DividendDeclarationDate.Create(new DateTime(2021, 3, 16)));
            dividendPayment.UpdateDividendPerShare(DividendPerShare.Create(.035M));

            stockSubscription.UpdateDividendPaymentRate(dividendPayment);
            await _unitOfWork.Commit();

            DividendPayment result = stockSubscription.DividendPaymentRates.FirstOrDefault(p => p.Id == new Guid("24d6936a-beb5-451b-a950-0f30e3ad463d"));

            Assert.Equal(new DateTime(2021, 3, 16), result.DividendDeclarationDate);
            Assert.Equal(.035M, result.DividendPerShare);
        }

        [Fact]
        public async Task ShouldDelete_DividendDeclaration_UsingStockSubscriptionAggregate()
        {
            StockSubscription stockSubscription = await _stockSubscriptionRepo.GetByIdAsync(new Guid("264632b4-20bd-473f-9a9b-dd6f3b6ddbac"));

            DividendPayment dividendPayment = stockSubscription.DividendPaymentRates.FirstOrDefault(p => p.Id == new Guid("24d6936a-beb5-451b-a950-0f30e3ad463d"));

            stockSubscription.DeleteDividendPaymentRate(dividendPayment.Id);
            await _unitOfWork.Commit();

            DividendPayment result = stockSubscription.DividendPaymentRates.FirstOrDefault(p => p.Id == new Guid("24d6936a-beb5-451b-a950-0f30e3ad463d"));

            Assert.Null(result);
        }
    }
}