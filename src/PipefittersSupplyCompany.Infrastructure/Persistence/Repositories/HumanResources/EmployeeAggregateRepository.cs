using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;

namespace PipefittersSupplyCompany.Infrastructure.Persistence.Repositories.HumanResources
{
    public class EmployeeAggregateRepository : IEmployeeAggregateRepository, IDisposable
    {
        private readonly AppDbContext _dbContext;

        public EmployeeAggregateRepository(AppDbContext ctx) => _dbContext = ctx;

        public async Task Add(Employee entity)
        {
            await _dbContext.ExternalAgents.AddAsync(entity.ExternalAgent);
            await _dbContext.Employees.AddAsync(entity);
        }

        public void Update(Employee entity) => _dbContext.Employees.Update(entity);

        public async Task<bool> Exists(Guid id) => await _dbContext.Employees.FindAsync(id) != null;

        public async Task<Employee> Load(Guid id) => await _dbContext.Employees.FindAsync(id);

        public void Dispose() => _dbContext.Dispose();
    }
}