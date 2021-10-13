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
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing.FinancierQueryParameters;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing.FinancierReadModels;

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
            var result = await query();

            return new OkObjectResult(result);
        }

        private async static Task HandleGetFinanciers
        (
            Func<Task<FinancierListItem>> query,
            ILoggerManager logger,
            HttpContext httpContext,
            LinkGenerator generator
        )
        {
            throw new NotImplementedException();
        }

        private static bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = httpContext.Items["AcceptHeaderMediaType"] as MediaTypeHeaderValue;

            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}