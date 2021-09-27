using System.Collections.Generic;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.ReadModels;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.QueryParameters;

namespace PipefittersSupplyCompany.Infrastructure.Interfaces
{
    public interface IEmployeeQueryService
    {
        Task<PagedList<EmployeeListItem>> Query(GetEmployees queryParameters);
        Task<PagedList<EmployeeListItem>> Query(GetEmployeesSupervisedBy queryParameters);
        Task<PagedList<EmployeeListItemWithRoles>> Query(GetEmployeesOfRole queryParameters);
        Task<EmployeeDetail> Query(GetEmployee queryParameters);
        Task<PagedList<EmployeeAddressListItem>> Query(GetEmployeeAddresses queryParameters);
        Task<EmployeeAddressDetail> Query(GetEmployeeAddress queryParameters);
        Task<PagedList<EmployeeContactListItem>> Query(GetEmployeeContacts queryParameters);
        Task<EmployeeContactDetail> Query(GetEmployeeContact queryParameters);
    }
}