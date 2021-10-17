using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing
{
    public class FinancierQueryRequestHander : IQueryRequestHandler
    {
        private readonly IFinancierQueryService _queryService;

        public FinancierQueryRequestHander(IFinancierQueryService queryService)
            => _queryService = queryService;

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
            IQueryResult<FinancierListItem> model = new QueryResult<FinancierListItem>();
            model.ReadModels = queryResult;
            model.CurrentHttpContext = httpContext;
            ResponseHeaderHandler<FinancierListItem> headerHandler = new ResponseHeaderHandler<FinancierListItem>();
            headerHandler.Process(ref model);

            // Add hateoas links

            // Create IActionResult return value            
            return new OkObjectResult(queryResult.ReadModels);
        }

        private IActionResult HandleGetFinancier(FinancierDetail queryResult, HttpContext httpContext)
        {
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