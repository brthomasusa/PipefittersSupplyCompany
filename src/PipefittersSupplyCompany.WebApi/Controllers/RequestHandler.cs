using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.ReadModels;

namespace PipefittersSupplyCompany.WebApi.Controllers
{
    public static class RequestHandler
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
            HttpContext httpContext
        )
        {
            try
            {
                var returnValue = await query();
                if (returnValue.GetType().GetInterfaces().Where(s => s.Name == "IEnumerable") != null)
                {
                    httpContext.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize((returnValue as PagedList<EmployeeListItems>).MetaData));
                }

                return new OkObjectResult(returnValue);
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
    }
}