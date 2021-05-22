using System;
using System.Threading.Tasks;
using PipefittersSupply.Domain.HumanResources.Employees;
using PipefittersSupply.Domain.Repository;
using Raven.Client.Documents.Session;

namespace PipefittersSupply.Repositories
{
    public class EmployeeRepository : IEmployeeRepository, IDisposable
    {
        private readonly AsyncDocumentSession _session;

        public EmployeeRepository(AsyncDocumentSession session) => _session = session;

        public Task<bool> Exists(EmployeeId id) => _session.Advanced.ExistsAsync(EntityId(id));

        public Task<Employee> Load(EmployeeId id) => _session.LoadAsync<Employee>(EntityId(id));

        public async Task Save(Employee entity)
        {
            await _session.StoreAsync(entity, EntityId(entity.Id));
            await _session.SaveChangesAsync();
        }

        public void Dispose() => _session.Dispose();

        private string EntityId(EmployeeId id) => $"Employee/{id}";
    }
}