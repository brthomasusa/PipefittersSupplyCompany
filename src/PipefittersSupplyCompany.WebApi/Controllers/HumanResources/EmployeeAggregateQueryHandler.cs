using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.ReadModels;
using PipefittersSupplyCompany.Infrastructure.Application.LinkModels.HumanResources;

namespace PipefittersSupplyCompany.WebApi.Controllers.HumanResources
{
    public static class EmployeeAggregateQueryHandler
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
                if (queryResult is PagedList<EmployeeListItem>)
                {
                    httpContext
                        .Response
                        .Headers
                        .Add("X-Pagination", JsonSerializer.Serialize((queryResult as PagedList<EmployeeListItem>).MetaData));
                }
                else if (queryResult is PagedList<EmployeeListItemWithRoles>)
                {
                    httpContext
                        .Response
                        .Headers
                        .Add("X-Pagination", JsonSerializer.Serialize((queryResult as PagedList<EmployeeListItemWithRoles>).MetaData));
                }
                else if (queryResult is PagedList<EmployeeAddressListItem>)
                {
                    httpContext
                        .Response
                        .Headers
                        .Add("X-Pagination", JsonSerializer.Serialize((queryResult as PagedList<EmployeeAddressListItem>).MetaData));
                }
                else if (queryResult is PagedList<EmployeeContactListItem>)
                {
                    httpContext
                        .Response
                        .Headers
                        .Add("X-Pagination", JsonSerializer.Serialize((queryResult as PagedList<EmployeeContactListItem>).MetaData));
                }

                // Add HATEoas links
                if (shouldAddLinkInfo)
                {
                    if (queryResult is EmployeeDetail)
                    {
                        var linkWrapper = employeeLinksGenerator.GenerateLinks(queryResult as EmployeeDetail, httpContext);
                        return new OkObjectResult(linkWrapper);
                    }

                    if (queryResult is PagedList<EmployeeListItem>)
                    {
                        var linkWrappers = employeeLinksGenerator.GenerateLinks(queryResult as IEnumerable<EmployeeListItem>, httpContext);
                        return new OkObjectResult(linkWrappers);
                    }

                    if (queryResult is PagedList<EmployeeListItemWithRoles>)
                    {
                        var linkWrappers = employeeLinksGenerator.GenerateLinks(queryResult as IEnumerable<EmployeeListItemWithRoles>, httpContext);
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