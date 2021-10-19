using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing
{
    public static class ActionResultPagedListCommand
    {
        public static IActionResult CreateActionResult<T>(PagedList<T> queryResult, HttpContext httpContext, LinkGenerator generator)
        {
            IQueryResult<T> container = new QueryResult<T>();
            container.ReadModels = queryResult;
            container.CurrentHttpContext = httpContext;

            ResponseHeaderHandler<T> headerHandler = new ResponseHeaderHandler<T>();

            bool shouldGenerateLinks = ShouldGenerateLinks(httpContext);

            if (shouldGenerateLinks)
            {
                headerHandler.NextHandler = new LinkGenerationHandler<T>(generator);
            }

            headerHandler.Process(ref container);

            // Create IActionResult return value 
            if (shouldGenerateLinks)
            {
                return new OkObjectResult(container.Links);
            }

            return new OkObjectResult(queryResult.ReadModels);
        }

        private static bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = httpContext.Items["AcceptHeaderMediaType"] as MediaTypeHeaderValue;

            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}