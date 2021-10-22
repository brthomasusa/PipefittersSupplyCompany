using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.Core.Financing.FinancierAggregate;

namespace PipefittersSupplyCompany.Infrastructure.Persistence.Repositories.Financing
{
    public class FinancierAggregateRepository : IFinancierAggregateRepository, IDisposable
    {
        private bool isDisposed;
        private readonly AppDbContext _dbContext;

        public FinancierAggregateRepository(AppDbContext ctx) => _dbContext = ctx;

        ~FinancierAggregateRepository() => Dispose(false);

        public async Task<Financier> GetByIdAsync(Guid id) => await _dbContext.Financiers.FindAsync(id);

        public async Task<bool> Exists(Guid id) => await _dbContext.Financiers.FindAsync(id) != null;

        public async Task AddAsync(Financier entity)
        {
            await _dbContext.ExternalAgents.AddAsync(entity.ExternalAgent);
            await _dbContext.Financiers.AddAsync(entity);
        }

        public void Update(Financier entity) => _dbContext.Financiers.Update(entity);

        public void Delete(Financier entity)
        {
            _dbContext.Financiers.Remove(entity);
            _dbContext.ExternalAgents.Remove(entity.ExternalAgent);
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