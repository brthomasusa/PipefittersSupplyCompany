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
        private readonly FinancierAggregateCommandHandler _cmdHdlr;
        private readonly IFinancierQueryService _qrySvc;

        public FinancierAggregateRequestHandler(FinancierAggregateCommandHandler cmdHdlr, IFinancierQueryService qrySvc)
        {
            _cmdHdlr = cmdHdlr;
            _qrySvc = qrySvc;
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
                case FinancierListItem qry:
                    // Run the query
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

        private async Task HandleGetFinanciers
        (
            Func<Task<FinancierListItem>> query,
            ILoggerManager logger,
            HttpContext httpContext,
            LinkGenerator generator
        )
        {
            throw new NotImplementedException();
        }

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = httpContext.Items["AcceptHeaderMediaType"] as MediaTypeHeaderValue;

            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}