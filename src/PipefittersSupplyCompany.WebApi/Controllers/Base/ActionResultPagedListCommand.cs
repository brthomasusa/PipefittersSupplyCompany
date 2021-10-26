using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;

namespace PipefittersSupplyCompany.WebApi.Controllers.Base
{
    public delegate ILinksWrapper PagedListLinkGenerationCommandDelegate<TReadModel>
        (
            PagedList<TReadModel> queryResult,
            HttpContext httpContext,
            LinkGenerator generator
        );

    public class ActionResultPagedListCommand
    {
        public static IActionResult CreateActionResult<TReadModel>
        (
            PagedList<TReadModel> queryResult,
            HttpContext httpContext,
            LinkGenerator generator,
            PagedListLinkGenerationCommandDelegate<TReadModel> funcPointer
        )
        {
            // 1 Add paging info to response header
            AddPagingInfoToResponseHeaderCommand<TReadModel>.Execute(queryResult, httpContext);

            // 2 Add hateoas links
            // 3 Add LinkWrapper to IActionResult and return it to caller
            if (ShouldGenerateLinks(httpContext.Request.Headers))
            {
                var linksWrapper = funcPointer(queryResult, httpContext, generator);
                return new OkObjectResult(linksWrapper);
            }

            // 4 Return IActionResult
            return new OkObjectResult(queryResult.ReadModels);
        }

        private static bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = httpContext.Items["AcceptHeaderMediaType"] as MediaTypeHeaderValue;

            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private static bool ShouldGenerateLinks(IHeaderDictionary dict)
        {
            foreach (StringValues keys in dict.Keys)
            {
                if (dict[keys].ToString().Contains("application/vnd.btechnical-consulting.hateoas+json", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}