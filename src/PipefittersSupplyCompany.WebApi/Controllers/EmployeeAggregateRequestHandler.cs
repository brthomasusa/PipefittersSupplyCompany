using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.ReadModels;
using PipefittersSupplyCompany.Infrastructure.Application.LinkModels;
using PipefittersSupplyCompany.Infrastructure.Application.LinkModels.HumanResources;

namespace PipefittersSupplyCompany.WebApi.Controllers
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
                var queryResult = await query();
                var shouldAddLinkInfo = ShouldGenerateLinks(httpContext);

                // Add pagination info to response.header
                if (queryResult is PagedList<EmployeeListItems>)
                {
                    httpContext
                        .Response
                        .Headers
                        .Add("X-Pagination", JsonSerializer.Serialize((queryResult as PagedList<EmployeeListItems>).MetaData));
                }
                else if (queryResult is PagedList<EmployeeListItemsWithRoles>)
                {
                    httpContext
                        .Response
                        .Headers
                        .Add("X-Pagination", JsonSerializer.Serialize((queryResult as PagedList<EmployeeListItemsWithRoles>).MetaData));
                }

                // Add HATEoas links
                if (shouldAddLinkInfo)
                {
                    if (queryResult is EmployeeDetails)
                    {
                        var linkWrapper = employeeLinksGenerator.GenerateLinks(queryResult as EmployeeDetails, httpContext);
                        return new OkObjectResult(linkWrapper);
                    }

                    if (queryResult is PagedList<EmployeeListItems>)
                    {
                        var linkWrappers = employeeLinksGenerator.GenerateLinks(queryResult as IEnumerable<EmployeeListItems>, httpContext);
                        return new OkObjectResult(linkWrappers);
                    }

                    if (queryResult is PagedList<EmployeeListItemsWithRoles>)
                    {
                        var linkWrappers = employeeLinksGenerator.GenerateLinks(queryResult as IEnumerable<EmployeeListItemsWithRoles>, httpContext);
                        return new OkObjectResult(linkWrappers);
                    }
                }

                return new OkObjectResult(queryResult);

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