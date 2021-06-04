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
        public async Task<IActionResult> Post(V1.CreateTimeCard request)
        {
            await _appSvc.Handle(request);
            return Ok();
        }

        [Route("employeeid")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateEmployeeId request)
        {
            await _appSvc.Handle(request);
            return Ok();
        }

        [Route("supervisorid")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdatTimeCardSupervisorId request)
        {
            await _appSvc.Handle(request);
            return Ok();
        }

        [Route("payperiodended")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdatePayPeriodEndDate request)
        {
            await _appSvc.Handle(request);
            return Ok();
        }

        [Route("regularhours")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateRegularHours request)
        {
            await _appSvc.Handle(request);
            return Ok();
        }

        [Route("overtimehours")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateOvertimeHours request)
        {
            await _appSvc.Handle(request);
            return Ok();
        }
    }
}