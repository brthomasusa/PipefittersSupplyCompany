using System;
using System.Threading.Tasks;
using PipefittersSupply.Domain.HumanResources.Employees;
using PipefittersSupply.Domain.Repository;
using Raven.Client.Documents.Session;

namespace PipefittersSupply.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository, IDisposable
    {
        private readonly IAsyncDocumentSession _session;
        private string EntityId(EmployeeId id) => $"Employee/{id}";

        public EmployeeRepository(IAsyncDocumentSession session) => _session = session;

        public Task<bool> Exists(EmployeeId id) => _session.Advanced.ExistsAsync(EntityId(id));

        public Task<Employee> Load(EmployeeId id) => _session.LoadAsync<Employee>(EntityId(id));

        public async Task Save(Employee entity)
        {
            await _session.StoreAsync(entity, EntityId(entity.Id));
            await _session.SaveChangesAsync();
        }

        public void Dispose() => _session.Dispose();
    }
}