using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.Core.Financing.StockSubscriptionAggregate;
using PipefittersSupplyCompany.Core.Shared;

namespace PipefittersSupplyCompany.Infrastructure.Persistence.Repositories.Financing
{
    public class StockSubscriptionAggregateRepository : IStockSubscriptionAggregateRepository, IDisposable
    {
        private bool isDisposed;
        private readonly AppDbContext _dbContext;

        public StockSubscriptionAggregateRepository(AppDbContext ctx) => _dbContext = ctx;
        ~StockSubscriptionAggregateRepository() => Dispose(false);

        public async Task<StockSubscription> GetByIdAsync(Guid id) => await _dbContext.StockSubscriptions.FindAsync(id);

        public async Task<bool> Exists(Guid id) => await _dbContext.StockSubscriptions.FindAsync(id) != null;

        public async Task AddAsync(StockSubscription entity)
        {
            await _dbContext.StockSubscriptions.AddAsync(entity);
        }

        public async Task AddEconomicEventAsync(EconomicEvent economicEvent)
        {
            await _dbContext.EconomicEvents.AddAsync(economicEvent);
        }

        public void Update(StockSubscription entity) => _dbContext.StockSubscriptions.Update(entity);

        public void Delete(StockSubscription entity)
        {
            _dbContext.StockSubscriptions.Remove(entity);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                // free managed resources
                _dbContext.Dispose();
            }

            isDisposed = true;
        }
    }
}