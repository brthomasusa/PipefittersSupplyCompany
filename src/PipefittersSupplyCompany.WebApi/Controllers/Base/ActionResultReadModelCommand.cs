using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.WebApi.Interfaces;

namespace PipefittersSupplyCompany.WebApi.Controllers.Base
{
    public delegate ILinksWrapper ReadModelLinkGenerationCommandDelegate<TReadModel>
        (
            TReadModel queryResult,
            HttpContext httpContext,
            LinkGenerator generator
        );

    public class ActionResultReadModelCommand
    {
        public static IActionResult CreateActionResult<TReadModel>
        (
            TReadModel queryResult,
            HttpContext httpContext,
            LinkGenerator generator,
            ReadModelLinkGenerationCommandDelegate<TReadModel> funcPointer
        )
        {
            // 1 Add hateoas links
            // 2 Add LinkWrapper to IActionResult and return it to caller
            if (ShouldGenerateLinks(httpContext))
            {
                var linksWrapper = funcPointer(queryResult, httpContext, generator);
                return new OkObjectResult(linksWrapper);
            }

            // 3 Return IActionResult
            return new OkObjectResult(queryResult);
        }

        private static bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = httpContext.Items["AcceptHeaderMediaType"] as MediaTypeHeaderValue;

            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}