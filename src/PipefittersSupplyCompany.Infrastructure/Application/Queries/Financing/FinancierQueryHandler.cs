using System;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Services.Financing;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing.FinancierQueryParameters;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing.FinancierReadModels;

namespace PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing
{
    public class FinancierQueryHandler<TQueryParam, TReadModel> : IQueryHandler<TQueryParam, TReadModel>
    {
        private readonly IFinancierQueryService _qrySvc;

        public FinancierQueryHandler(IFinancierQueryService qrySvc)
        {
            _qrySvc = qrySvc;
        }

        public Task<TReadModel> Handle(TQueryParam queryParam)
        {
            throw new NotImplementedException();
        }
    }
}