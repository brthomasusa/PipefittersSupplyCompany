using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing
{
    public class FinancierAggregateRequestHandler
    {
        // private readonly FinancierAggregateCommandHandler _cmdHdlr;
        private readonly IFinancierQueryHandler _qryHdlr;

        public FinancierAggregateRequestHandler
        (
            // FinancierAggregateCommandHandler cmdHdlr,
            IFinancierQueryHandler qryHdlr
        )
        {
            // _cmdHdlr = cmdHdlr;
            _qryHdlr = qryHdlr;
        }

        public async Task<IActionResult> HandleQuery<TQueryParam>
        (
            TQueryParam queryParam,
            HttpContext httpContext
        ) =>
            queryParam switch
            {
                GetFinanciers param => await HandleGetFinanciers(param, httpContext),
                GetFinancier param => await HandleGetFinancier(param, httpContext),
                _ => throw new ArgumentException("Unknown FinancierQueryParameter!", nameof(queryParam))
            };

        private async Task<IActionResult> HandleGetFinanciers(GetFinanciers queryParam, HttpContext httpContext)
        {
            // Run the query
            var queryResult = await _qryHdlr.GetFinancierListItems(queryParam);

            // Add paging info to response header

            // Add hateoas links

            // Create IActionResult return value            
            return new OkObjectResult(queryResult);
        }

        private async Task<IActionResult> HandleGetFinancier(GetFinancier queryParam, HttpContext httpContext)
        {
            // Run the query
            var queryResult = await _qryHdlr.GetFinancierDetail(queryParam);

            // Add hateoas links

            // Create IActionResult return value            
            return new OkObjectResult(queryResult);
        }

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = httpContext.Items["AcceptHeaderMediaType"] as MediaTypeHeaderValue;

            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}