using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace PipefittersSupplyCompany.WebApi.Controllers
{
    public static class RequestHandler
    {
        public static async Task<IActionResult> HandleCommand<TCommand>
        (
            TCommand request,
            Func<TCommand, Task> handler,
            ILogger logger
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
                logger.LogError(ex, "Error handling the command.");
                return new BadRequestObjectResult(new { error = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        public static async Task<IActionResult> HandleQuery<TQueryParam>
        (
            Func<Task<TQueryParam>> query,
            ILogger logger
        )
        {
            try
            {
                return new OkObjectResult(await query());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error handling the query.");
                return new BadRequestObjectResult(new
                {
                    error = ex.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }
    }
}