using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using static PipefittersSupply.Infrastructure.Application.Commands.HumanResources.TimeCardCommand;
using PipefittersSupply.AppServices;

namespace PipefittersSupply.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeCardController : ControllerBase
    {
        private readonly ILogger<TimeCardController> _logger;
        private readonly TimeCardApplicationService _appSvc;

        public TimeCardController(TimeCardApplicationService appSvc, ILogger<TimeCardController> logger)
        {
            _appSvc = appSvc;
            _logger = logger;
            _logger.LogInformation("TimeCardController...");
        }

        [HttpPost]
        public async Task<IActionResult> Post(V1.CreateTimeCard request) => await HandleRequest(request, _appSvc.Handle);

        private async Task<IActionResult> HandleRequest<T>(T request, Func<T, Task> handler)
        {
            try
            {
                _logger.LogDebug("Handling HTTP request of type {}", typeof(T).Name);

                await handler(request);

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError("Error handling the request.", e);

                return new BadRequestObjectResult(new { error = e.Message, stackTrace = e.StackTrace });
            }
        }
    }
}