using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing.FinancierQueryParameters;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing.FinancierReadModels;

namespace PipefittersSupplyCompany.Infrastructure.Interfaces
{
    public interface IFinancierQueryHandler
    {
        Task<PagedList<FinancierListItem>> GetFinancierListItems(GetFinanciers queryParameters);
        Task<FinancierDetail> GetFinancierDetail(GetFinancier queryParameters);
        Task<PagedList<FinancierAddressListItem>> GetFinancierAddressListItems(GetFinancierAddresses queryParameters);
        Task<FinancierAddressDetail> GetFinancierAddressDetail(GetFinancierAddress queryParameters);
        Task<PagedList<FinancierContactListItem>> GetFinancierContactListItems(GetFinancierContacts queryParameters);
        Task<FinancierContactDetail> GetFinancierContactDetail(GetFinancierContact queryParameters);
    }
}