using System.Collections.Generic;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.ReadModels;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.QueryParameters;

namespace PipefittersSupplyCompany.Infrastructure.Interfaces
{
    public interface IEmployeeQueryService
    {
        Task<PagedList<EmployeeListItems>> Query(GetEmployees queryParameters);
        Task<PagedList<EmployeeListItems>> Query(GetEmployeesSupervisedBy queryParameters);
        Task<PagedList<EmployeeListItemsWithRoles>> Query(GetEmployeesOfRole queryParameters);
        Task<EmployeeDetails> Query(GetEmployee queryParameters);
    }
}