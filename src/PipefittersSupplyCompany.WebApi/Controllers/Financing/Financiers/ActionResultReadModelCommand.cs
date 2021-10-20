using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;
using PipefittersSupplyCompany.Infrastructure.Interfaces;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing.Financiers
{
    public static class FinancierReadModelCommand
    {
        public static IActionResult CreateActionResult<T>
        (
            T queryResult,
            HttpContext httpContext,
            LinkGenerator generator
        )
        {
            if (ShouldGenerateLinks(httpContext))
            {
                IQueryResult<T> container = new QueryResult<T>();
                container.ReadModel = queryResult as IReadModel;
                container.CurrentHttpContext = httpContext;

                LinkGenerationHandler<T> linkGenerationHandler =
                    new LinkGenerationHandler<T>(generator);

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