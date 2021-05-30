using System.Threading.Tasks;
using PipefittersSupply.Domain.Interfaces;
using Raven.Client.Documents.Session;

namespace PipefittersSupply.Domain.Infrastructure
{
    public class RavenDbUnitOfWork : IUnitOfWork
    {
        private readonly IAsyncDocumentSession _session;

        public RavenDbUnitOfWork(IAsyncDocumentSession session) => _session = session;

        public Task Commit() => _session.SaveChangesAsync();
    }
}