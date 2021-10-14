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
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing.FinancierQueryParameters;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing.FinancierReadModels;

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
            ILoggerManager logger,
            HttpContext httpContext
        )
        {
            switch (queryParam)
            {
                case GetFinanciers qry:
                    // Run the query
                    var result = await HandleGetFinanciers(qry, logger, httpContext);
                    var actionResult = new OkObjectResult(result);
                    // Add paging info to response header
                    // Add hateoas links
                    // Create IActionResult return value
                    break;
                case FinancierDetail qry:
                    break;
                default:
                    // TODO Return a BadRequestResult with specific error message
                    throw new ArgumentException("Unknown FinancierQueryParameter!", nameof(queryParam));
            };

            return new OkObjectResult(null);
        }

        private async Task<PagedList<FinancierListItem>> HandleGetFinanciers
        (
            GetFinanciers queryParam,
            ILoggerManager logger,
            HttpContext httpContext
        )
        {
            return await _qryHdlr.GetFinancierListItems(queryParam);
        }

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = httpContext.Items["AcceptHeaderMediaType"] as MediaTypeHeaderValue;

            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}