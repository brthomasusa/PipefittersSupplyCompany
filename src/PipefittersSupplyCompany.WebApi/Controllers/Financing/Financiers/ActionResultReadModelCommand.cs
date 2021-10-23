using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
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
            if (queryResult as IReadModel is not null)
            {
                if (ShouldGenerateLinks(httpContext.Request.Headers))
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

            return new NotFoundObjectResult(new { Message = "Nothing found that matches the search criteria." });
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