using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing.Financiers
{
    public class FinancierQueryRequestHander : IFinancierQueryRequestHandler
    {
        private readonly IFinancierQueryService _queryService;
        private readonly LinkGenerator _linkGenerator;

        public FinancierQueryRequestHander(IFinancierQueryService queryService, LinkGenerator generator)
        {
            _queryService = queryService;
            _linkGenerator = generator;
        }

        public async Task<IActionResult> Handle<TQueryParam>
        (
            TQueryParam queryParam,
            HttpContext httpContext
        ) =>
            queryParam switch
            {
                GetFinanciers param =>
                    FinancierPagedListCommand.CreateActionResult<FinancierListItem>(await _queryService.Query(param), httpContext, _linkGenerator),
                GetFinancier param
                    => FinancierReadModelCommand.CreateActionResult<FinancierDetail>(await _queryService.Query(param), httpContext, _linkGenerator),
                _ => throw new ArgumentOutOfRangeException("Unknown Financier query parameter!", nameof(queryParam))
            };
    }
}