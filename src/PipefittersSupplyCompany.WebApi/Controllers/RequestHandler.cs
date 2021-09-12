using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;

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
            ILoggerManager logger
        )
        {
            try
            {
                return new OkObjectResult(await query());
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