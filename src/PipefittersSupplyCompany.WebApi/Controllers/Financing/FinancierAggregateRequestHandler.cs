using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing.ReadModels;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing
{
    public static class FinancierAggregateRequestHandler
    {
        public static async Task<IActionResult> HandleCommand<TWriteModel>
        (
            TWriteModel writeModel,
            Func<TWriteModel, Task> handler,
            ILoggerManager logger
        )
        {
            try
            {
                logger.LogDebug($"Handling HTTP request of type {typeof(TWriteModel).Name}");
                await handler(writeModel);
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
            LinkGenerator generator
        )
        {
            try
            {
                var result = await query();
                var readModelString = result.GetType().FullName + ", PipefittersSupplyCompany.Infrastructure";
                Type readModel = Type.GetType(readModelString, true, true);

                IQueryResult queryResult = new QueryResult();
                queryResult.ReadModel = result as IReadModel;
                queryResult.CurrentHttpContext = httpContext;

                var genericBase = typeof(ResponseHeaderHandler<>);
                var combinedType = genericBase.MakeGenericType(readModel);
                var responseHeaderHandler = Activator.CreateInstance(combinedType);

                // Type readModelType = (queryResult.ReadModel).GetType();
                // ResponseHeaderHandler<PagedList<FinancierListItem>> responseHeaderHandler =
                //     new ResponseHeaderHandler<PagedList<FinancierListItem>>();

                // ResponseHeaderHandler<PagedList<FinancierListItem>> responseHeaderHandler = new ResponseHeaderHandler<PagedList<FinancierListItem>>();

                // IQueryResultHandler responseHeaderHandler = new ResponseHeaderHandler();

                // if (ShouldGenerateLinks(httpContext))
                // {
                //     responseHeaderHandler.NextHandler = new LinkGenerationHandler(generator);
                // }

                // responseHeaderHandler.Process(ref queryResult);

                // if (ShouldGenerateLinks(httpContext))
                // {
                //     if (result is EmployeeDetail)
                //     {
                //         return new OkObjectResult(queryResult.Links as LinksWrapper<EmployeeDetail>);
                //     }
                //     else if (result is PagedList<EmployeeListItem>)
                //     {
                //         return new OkObjectResult(queryResult.Links as LinksWrapperList<EmployeeListItem>);
                //     }
                //     else if (result is PagedList<EmployeeListItemWithRoles>)
                //     {
                //         return new OkObjectResult(queryResult.Links as LinksWrapperList<EmployeeListItemWithRoles>);
                //     }
                // }

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