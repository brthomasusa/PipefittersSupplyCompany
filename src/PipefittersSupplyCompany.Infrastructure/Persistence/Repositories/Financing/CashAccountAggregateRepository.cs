using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.Core.Financing.CashAccountAggregate;

namespace PipefittersSupplyCompany.Infrastructure.Persistence.Repositories.Financing
{
    public class CashAccountAggregateRepository : ICashAccountAggregateRepository, IDisposable
    {
        private bool isDisposed;
        private readonly AppDbContext _dbContext;

        public CashAccountAggregateRepository(AppDbContext ctx) => _dbContext = ctx;
        ~CashAccountAggregateRepository() => Dispose(false);

        public async Task<CashAccount> GetByIdAsync(Guid id) => await _dbContext.CashAccounts.FindAsync(id);

        public async Task<bool> Exists(Guid id) => await _dbContext.CashAccounts.FindAsync(id) != null;

        public async Task AddAsync(CashAccount entity)
        {
            await _dbContext.CashAccounts.AddAsync(entity);
        }

        public void Update(CashAccount entity) => _dbContext.CashAccounts.Update(entity);

        public void Delete(CashAccount entity)
        {
            _dbContext.CashAccounts.Remove(entity);
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