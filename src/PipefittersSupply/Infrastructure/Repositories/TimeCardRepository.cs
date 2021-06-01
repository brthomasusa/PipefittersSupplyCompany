using System;
using System.Threading.Tasks;
using PipefittersSupply.Domain.HumanResources.TimeCards;
using PipefittersSupply.Domain.Repository;
using Raven.Client.Documents.Session;

namespace PipefittersSupply.Infrastructure.Repositories
{
    public class TimeCardRepository : ITimeCardRepository, IDisposable
    {
        private readonly PipefittersSupplyDbContext _dbContext;
        private string EntityId(TimeCardId id) => $"TimeCard/{id}";

        public TimeCardRepository(PipefittersSupplyDbContext dbCtx) => _dbContext = dbCtx;

        public async Task Add(TimeCard entity) => await _dbContext.TimeCards.AddAsync(entity);

        public async Task<bool> Exists(TimeCardId id) => await _dbContext.TimeCards.FindAsync(id) != null;

        public async Task<TimeCard> Load(TimeCardId id) => await _dbContext.TimeCards.FindAsync(id);

        public void Dispose() => _dbContext.Dispose();
    }
}