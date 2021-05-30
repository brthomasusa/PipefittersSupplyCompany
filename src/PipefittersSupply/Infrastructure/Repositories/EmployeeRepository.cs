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

        public EmployeeRepository(IAsyncDocumentSession session) => _session = session;

        public Task Add(Employee entity) => _session.StoreAsync(entity, EntityId(entity.Id));

        public Task<bool> Exists(EmployeeId id) => _session.Advanced.ExistsAsync(EntityId(id));

        public Task<Employee> Load(EmployeeId id) => _session.LoadAsync<Employee>(EntityId(id));

        public void Dispose() => _session.Dispose();

        private string EntityId(EmployeeId id) => $"Employee/{id}";
    }
}