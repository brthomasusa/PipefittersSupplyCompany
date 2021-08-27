using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;

namespace PipefittersSupplyCompany.Infrastructure.Persistence
{
    public class AppUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public AppUnitOfWork(AppDbContext ctx) => _dbContext = ctx;

        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}