using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.ReadModels;

namespace PipefittersSupplyCompany.WebApi.Controllers.HumanResources
{
    public static class EmployeeAggregateRequestHandler
    {
        public static async Task<IActionResult> HandleCommand<TCommand>
        (
            TCommand request,
            Func<TCommand, Task> handler,
            ILoggerManager logger
        )
        {
            try
            {
                logger.LogDebug($"Handling HTTP request of type {typeof(TCommand).Name}");
                await handler(request);
                return new OkResult();
            }
            catch (Exception ex)
            {
                logger.LogError("Error handling the command.");
                return new BadRequestObjectResult(new
                {
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        public static async Task<IActionResult> HandleQuery<TQueryParam>
        (
            Func<Task<TQueryParam>> query,
            ILoggerManager logger,
            HttpContext httpContext,
            EmployeeLinks employeeLinksGenerator
        )
        {
            try
            {
                var result = await query();

                IQueryResult queryResult = new QueryResult();
                queryResult.ReadModel = result as IReadModel;
                queryResult.CurrentHttpContext = httpContext;
                queryResult.HateOasLinksGenerator = employeeLinksGenerator;

                IQueryResultHandler responseHeaderHandler = new ResponseHeaderHandler();
                IQueryResultHandler linkGenerationHandler = new LinkGenerationHandler();
                responseHeaderHandler.NextHandler = linkGenerationHandler;
                linkGenerationHandler.NextHandler = null;

                responseHeaderHandler.Process(ref queryResult);

                if (ShouldGenerateLinks(httpContext))
                {
                    if (result is EmployeeDetail)
                    {
                        return new OkObjectResult(queryResult.Links as LinksWrapper<EmployeeDetail>);
                    }
                    else if (result is PagedList<EmployeeListItem>)
                    {
                        return new OkObjectResult(queryResult.Links as LinksWrapperList<EmployeeListItem>);
                    }
                    else if (result is PagedList<EmployeeListItemWithRoles>)
                    {
                        return new OkObjectResult(queryResult.Links as LinksWrapperList<EmployeeListItemWithRoles>);
                    }
                }

                return new OkObjectResult(result);

            }
            catch (ArgumentException ex)
            {
                logger.LogError($"Query parameter not found in database: {ex}");
                return new NotFoundObjectResult(new
                {
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
            catch (Exception ex)
            {
                logger.LogError($"Error handling the query: {ex}");
                return new BadRequestObjectResult(new
                {
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        private static bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = httpContext.Items["AcceptHeaderMediaType"] as MediaTypeHeaderValue;

            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}