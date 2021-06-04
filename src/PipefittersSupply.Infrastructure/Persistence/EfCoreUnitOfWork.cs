using System.Threading.Tasks;
using PipefittersSupply.Domain.Interfaces;

namespace PipefittersSupply.Infrastructure.Persistence.Repositories
{
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        private readonly PipefittersSupplyDbContext _dbContext;

        public EfCoreUnitOfWork(PipefittersSupplyDbContext ctx) => _dbContext = ctx;

        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}