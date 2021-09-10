using System.Collections.Generic;
using System.Threading.Tasks;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.ReadModels;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.QueryParameters;

namespace PipefittersSupplyCompany.Infrastructure.Interfaces
{
    public interface IEmployeeQueryService
    {
        Task<IEnumerable<EmployeeListItems>> Query(GetEmployees queryParameters);
        Task<IEnumerable<EmployeeListItems>> Query(GetEmployeesSupervisedBy queryParameters);
        Task<IEnumerable<EmployeeListItemsWithRoles>> Query(GetEmployeesOfRole queryParameters);
        Task<EmployeeDetails> Query(GetEmployee queryParameters);
    }
}