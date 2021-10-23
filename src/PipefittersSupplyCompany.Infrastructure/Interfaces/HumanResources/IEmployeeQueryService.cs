using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources;

namespace PipefittersSupplyCompany.Infrastructure.Interfaces.HumanResources
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