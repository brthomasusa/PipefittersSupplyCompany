using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using PipefittersSupply.AppServices;
using static PipefittersSupply.Contracts.HumanResources.EmployeeCommand;

namespace PipefittersSupply.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly EmployeeAppicationService _employeeAppSvc;

        public EmployeeController(EmployeeAppicationService appSvc, ILogger<EmployeeController> logger)
        {
            _employeeAppSvc = appSvc;
            _logger = logger;
            _logger.LogInformation("EmployeeController...");
        }

        [HttpPost]
        public Task<IActionResult> Post(V1.CreateEmployee request) => HandleRequest(request, _employeeAppSvc.Handle);

        [HttpPut]
        public Task<IActionResult> Put(V1.UpdateEmployee request) => HandleRequest(request, _employeeAppSvc.Handle);

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