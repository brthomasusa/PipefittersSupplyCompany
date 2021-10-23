using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;

namespace PipefittersSupplyCompany.Infrastructure.Interfaces.Financing
{
    public interface IFinancierQueryService
    {
        Task<FinancierDependencyCheckResult> Query(DoFinancierDependencyCheck queryParameters);
        Task<PagedList<FinancierListItem>> Query(GetFinanciers queryParameters);
        Task<FinancierDetail> Query(GetFinancier queryParameters);
        Task<PagedList<FinancierAddressListItem>> Query(GetFinancierAddresses queryParameters);
        Task<FinancierAddressDetail> Query(GetFinancierAddress queryParameters);
        Task<PagedList<FinancierContactListItem>> Query(GetFinancierContacts queryParameters);
        Task<FinancierContactDetail> Query(GetFinancierContact queryParameters);
    }
}