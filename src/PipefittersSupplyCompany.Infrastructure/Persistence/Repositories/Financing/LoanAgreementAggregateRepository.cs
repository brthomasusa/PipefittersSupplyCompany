using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate;

namespace PipefittersSupplyCompany.Infrastructure.Persistence.Repositories.Financing
{
    public class LoanAgreementAggregateRepository : ILoanAgreementAggregateRepository, IDisposable
    {
        private bool isDisposed;
        private readonly AppDbContext _dbContext;

        public LoanAgreementAggregateRepository(AppDbContext ctx) => _dbContext = ctx;
        ~LoanAgreementAggregateRepository() => Dispose(false);

        public async Task<LoanAgreement> GetByIdAsync(Guid id) => await _dbContext.LoanAgreements.FindAsync(id);

        public async Task<bool> Exists(Guid id) => await _dbContext.LoanAgreements.FindAsync(id) != null;

        public async Task AddAsync(LoanAgreement entity)
        {
            await _dbContext.EconomicEvents.AddAsync(entity.EconomicEvent);
            await _dbContext.LoanAgreements.AddAsync(entity);
        }

        public void Update(LoanAgreement entity) => _dbContext.LoanAgreements.Update(entity);

        public void Delete(LoanAgreement entity)
        {
            _dbContext.LoanAgreements.Remove(entity);
            _dbContext.EconomicEvents.Remove(entity.EconomicEvent);
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