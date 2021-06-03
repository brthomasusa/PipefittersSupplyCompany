using System.Threading.Tasks;
using PipefittersSupply.Domain.Interfaces;

namespace PipefittersSupply.Infrastructure
{
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        private readonly PipefittersSupplyDbContext _dbContext;

        public EfCoreUnitOfWork(PipefittersSupplyDbContext ctx) => _dbContext = ctx;

        public Task Commit() => _dbContext.SaveChangesAsync();
    }
}