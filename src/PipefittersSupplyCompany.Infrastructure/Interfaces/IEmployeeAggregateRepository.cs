using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;

namespace PipefittersSupplyCompany.Infrastructure.Interfaces
{
    public interface IEmployeeAggregateRepository
    {
        Task Add(Employee entity);
        void Update(Employee entity);
        Task<bool> Exists(Guid id);
        Task<Employee> Load(Guid id);
    }
}