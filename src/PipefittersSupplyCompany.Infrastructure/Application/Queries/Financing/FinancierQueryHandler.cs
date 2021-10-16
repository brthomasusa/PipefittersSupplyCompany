using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;

namespace PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing
{
    public class FinancierQueryHandler : IFinancierQueryHandler
    {
        private readonly IFinancierQueryService _qrySvc;

        public FinancierQueryHandler(IFinancierQueryService qrySvc)
        {
            _qrySvc = qrySvc;
        }

        public async Task<PagedList<FinancierListItem>> GetFinancierListItems(GetFinanciers queryParameters)
            => await _qrySvc.Query(queryParameters);

        public async Task<FinancierDetail> GetFinancierDetail(GetFinancier queryParameters)
            => await _qrySvc.Query(queryParameters);

        public async Task<PagedList<FinancierAddressListItem>> GetFinancierAddressListItems(GetFinancierAddresses queryParameters)
            => await _qrySvc.Query(queryParameters);

        public async Task<FinancierAddressDetail> GetFinancierAddressDetail(GetFinancierAddress queryParameters)
            => await _qrySvc.Query(queryParameters);

        public async Task<PagedList<FinancierContactListItem>> GetFinancierContactListItems(GetFinancierContacts queryParameters)
            => await _qrySvc.Query(queryParameters);

        public async Task<FinancierContactDetail> GetFinancierContactDetail(GetFinancierContact queryParameters)
            => await _qrySvc.Query(queryParameters);
    }
}