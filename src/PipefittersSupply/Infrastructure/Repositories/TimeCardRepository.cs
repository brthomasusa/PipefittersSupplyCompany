using System;
using System.Threading.Tasks;
using PipefittersSupply.Domain.HumanResources.TimeCards;
using PipefittersSupply.Domain.Repository;
using Raven.Client.Documents.Session;

namespace PipefittersSupply.Infrastructure.Repositories
{
    public class TimeCardRepository : ITimeCardRepository, IDisposable
    {
        private readonly IAsyncDocumentSession _session;
        private string EntityId(TimeCardId id) => $"TimeCard/{id}";

        public TimeCardRepository(IAsyncDocumentSession session) => _session = session;

        public Task Add(TimeCard entity) => _session.StoreAsync(entity, EntityId(entity.Id));

        public Task<bool> Exists(TimeCardId id) => _session.Advanced.ExistsAsync(EntityId(id));

        public Task<TimeCard> Load(TimeCardId id) => _session.LoadAsync<TimeCard>(EntityId(id));

        public async Task Save(TimeCard entity)
        {
            await _session.StoreAsync(entity, EntityId(entity.Id));
            await _session.SaveChangesAsync();
        }

        public void Dispose() => _session.Dispose();
    }
}