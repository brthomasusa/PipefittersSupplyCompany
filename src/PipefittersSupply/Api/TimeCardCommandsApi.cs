using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static PipefittersSupply.Contracts.HumanResources.TimeCardCommand;
namespace PipefittersSupply.Api
{
    [Route("api/Timecards")]
    [ApiController]
    public class TimeCardCommandsApi : ControllerBase
    {
        private readonly TimeCardApplicationService _appSvc;

        public TimeCardCommandsApi(TimeCardApplicationService appSvc) => _appSvc = appSvc;

        [HttpPost]
        public async Task<IActionResult> Post(V1.Create request)
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
        public async Task<IActionResult> Put(V1.UpdateSupervisorId request)
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