using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing.ReadModels;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing.QueryParameters;

namespace PipefittersSupplyCompany.Infrastructure.Interfaces
{
    public interface IFinancierQueryService
    {
        Task<PagedList<FinancierListItem>> Query(GetFinanciers queryParameters);
        Task<FinancierDetail> Query(GetFinancier queryParameters);
        Task<PagedList<FinancierAddressListItem>> Query(GetFinancierAddresses queryParameters);
        Task<FinancierAddressDetail> Query(GetFinancierAddress queryParameters);
        Task<PagedList<FinancierContactListItem>> Query(GetFinancierContacts queryParameters);
        Task<FinancierContactDetail> Query(GetFinancierContact queryParameters);
    }
}