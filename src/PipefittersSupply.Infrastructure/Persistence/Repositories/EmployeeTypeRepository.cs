using System;
using System.Threading.Tasks;
using PipefittersSupply.Domain.Repository;
using PipefittersSupply.Domain.HumanResources.Employees;

namespace PipefittersSupply.Infrastructure.Persistence.Repositories
{
    public class EmployeeTypeRepository : IEmployeeTypeRepository, IDisposable
    {
        private readonly PipefittersSupplyDbContext _dbContext;

        public EmployeeTypeRepository(PipefittersSupplyDbContext dbCtx) => _dbContext = dbCtx;

        public async Task Add(EmployeeType entity) => await _dbContext.EmployeeTypes.AddAsync(entity);

        public async Task<bool> Exists(EmployeeTypeIdentifier id) => await _dbContext.EmployeeTypes.FindAsync(id) != null;

        public async Task<EmployeeType> Load(EmployeeTypeIdentifier id) => await _dbContext.EmployeeTypes.FindAsync(id);

        public void Dispose() => _dbContext.Dispose();
    }
}