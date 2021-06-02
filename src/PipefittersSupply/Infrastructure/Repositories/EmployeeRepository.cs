using System;
using System.Threading.Tasks;
using PipefittersSupply.Domain.Repository;
using PipefittersSupply.Domain.HumanResources.Employees;

namespace PipefittersSupply.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository, IDisposable
    {
        private readonly PipefittersSupplyDbContext _dbContext;

        public EmployeeRepository(PipefittersSupplyDbContext dbCtx) => _dbContext = dbCtx;

        public async Task Add(Employee entity) => await _dbContext.Employees.AddAsync(entity);

        public async Task<bool> Exists(EmployeeId id) => await _dbContext.Employees.FindAsync(id.Value) != null;

        public async Task<Employee> Load(EmployeeId id) => await _dbContext.Employees.FindAsync(id);

        public void Dispose() => _dbContext.Dispose();
    }
}