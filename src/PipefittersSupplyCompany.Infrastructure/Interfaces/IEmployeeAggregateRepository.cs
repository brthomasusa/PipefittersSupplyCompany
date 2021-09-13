using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.SharedKernel.Interfaces;
namespace PipefittersSupplyCompany.Infrastructure.Interfaces
{
    public interface IEmployeeAggregateRepository : IRepository<Employee>
    {

    }
}