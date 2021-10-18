using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing
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
                GetFinanciers param => HandleGetFinanciers(await _queryService.Query(param), httpContext),
                GetFinancier param => HandleGetFinancier(await _queryService.Query(param), httpContext),
                _ => throw new ArgumentException("Unknown Financier query parameter!", nameof(queryParam))
            };

        private IActionResult HandleGetFinanciers(PagedList<FinancierListItem> queryResult, HttpContext httpContext)
        {
            // Add paging info to response header
            IQueryResult<FinancierListItem> container = new QueryResult<FinancierListItem>();
            container.ReadModels = queryResult;
            container.CurrentHttpContext = httpContext;

            ResponseHeaderHandler<FinancierListItem> headerHandler = new ResponseHeaderHandler<FinancierListItem>();

            // Add hateoas links
            bool shouldGenerateLinks = ShouldGenerateLinks(httpContext);

            if (shouldGenerateLinks)
            {
                headerHandler.NextHandler = new LinkGenerationHandler<FinancierListItem>(_linkGenerator);
            }

            headerHandler.Process(ref container);

            // Create IActionResult return value 
            if (shouldGenerateLinks)
            {
                return new OkObjectResult(container.Links);
            }

            return new OkObjectResult(queryResult.ReadModels);
        }

        private IActionResult HandleGetFinancier(FinancierDetail queryResult, HttpContext httpContext)
        {
            IQueryResult<FinancierDetail> container = new QueryResult<FinancierDetail>();
            container.ReadModel = queryResult;
            container.CurrentHttpContext = httpContext;

            // Add hateoas links
            bool shouldGenerateLinks = ShouldGenerateLinks(httpContext);

            if (shouldGenerateLinks)
            {
                LinkGenerationHandler<FinancierDetail> linkGenerationHandler =
                    new LinkGenerationHandler<FinancierDetail>(_linkGenerator);
                linkGenerationHandler.Process(ref container);
                return new OkObjectResult(container.Links);
            }

            return new OkObjectResult(queryResult);
        }


        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = httpContext.Items["AcceptHeaderMediaType"] as MediaTypeHeaderValue;

            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}