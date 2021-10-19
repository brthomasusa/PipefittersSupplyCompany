using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing
{
    public static class ActionResultReadModelCommand
    {
        public static IActionResult CreateActionResult
        (
            IReadModel queryResult,
            HttpContext httpContext,
            LinkGenerator generator
        ) =>
            queryResult switch
            {
                FinancierDetail readModel => HandleGetFinancier(readModel, httpContext, generator),
                FinancierAddressDetail readModel => HandleGetFinancierAddressDetail(readModel, httpContext, generator),
                FinancierContactDetail readModel => HandleGetFinancierContactDetail(readModel, httpContext, generator),
                _ => throw new ArgumentOutOfRangeException("Unknown Financier query parameter!", nameof(queryResult))
            };

        private static IActionResult HandleGetFinancier(FinancierDetail queryResult, HttpContext httpContext, LinkGenerator generator)
        {
            IQueryResult<FinancierDetail> container = new QueryResult<FinancierDetail>();
            container.ReadModel = queryResult;
            container.CurrentHttpContext = httpContext;

            // Add hateoas links
            bool shouldGenerateLinks = ShouldGenerateLinks(httpContext);

            if (shouldGenerateLinks)
            {
                LinkGenerationHandler<FinancierDetail> linkGenerationHandler =
                    new LinkGenerationHandler<FinancierDetail>(generator);
                linkGenerationHandler.Process(ref container);
                return new OkObjectResult(container.Links);
            }

            return new OkObjectResult(queryResult);
        }

        private static IActionResult HandleGetFinancierAddressDetail(FinancierAddressDetail queryResult, HttpContext httpContext, LinkGenerator generator)
        {
            IQueryResult<FinancierAddressDetail> container = new QueryResult<FinancierAddressDetail>();
            container.ReadModel = queryResult;
            container.CurrentHttpContext = httpContext;

            // Add hateoas links
            bool shouldGenerateLinks = ShouldGenerateLinks(httpContext);

            if (shouldGenerateLinks)
            {
                LinkGenerationHandler<FinancierAddressDetail> linkGenerationHandler =
                    new LinkGenerationHandler<FinancierAddressDetail>(generator);
                linkGenerationHandler.Process(ref container);
                return new OkObjectResult(container.Links);
            }

            return new OkObjectResult(queryResult);
        }

        private static IActionResult HandleGetFinancierContactDetail(FinancierContactDetail queryResult, HttpContext httpContext, LinkGenerator generator)
        {
            IQueryResult<FinancierContactDetail> container = new QueryResult<FinancierContactDetail>();
            container.ReadModel = queryResult;
            container.CurrentHttpContext = httpContext;

            // Add hateoas links
            bool shouldGenerateLinks = ShouldGenerateLinks(httpContext);

            if (shouldGenerateLinks)
            {
                LinkGenerationHandler<FinancierContactDetail> linkGenerationHandler =
                    new LinkGenerationHandler<FinancierContactDetail>(generator);
                linkGenerationHandler.Process(ref container);
                return new OkObjectResult(container.Links);
            }

            return new OkObjectResult(queryResult);
        }

        private static bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = httpContext.Items["AcceptHeaderMediaType"] as MediaTypeHeaderValue;

            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}