using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PipefittersSupply.AppServices;

using static PipefittersSupply.Contracts.HumanResources.EmployeeCommand;

namespace PipefittersSupply.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeAppicationService _employeeAppSvc;

        public EmployeeController(EmployeeAppicationService appSvc) => _employeeAppSvc = appSvc;

        [HttpPost]
        public async Task<IActionResult> Post(V1.Create request)
        {
            await _employeeAppSvc.Handle(request);
            return Ok();
        }

        [Route("employeetypeid")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateEmployeeTypeId request)
        {
            await _employeeAppSvc.Handle(request);
            return Ok();
        }

        [Route("supervisorid")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateSupervisorId request)
        {
            await _employeeAppSvc.Handle(request);
            return Ok();
        }

        [Route("lastname")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateEmployeeLastName request)
        {
            await _employeeAppSvc.Handle(request);
            return Ok();
        }

        [Route("firstname")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateEmployeeFirstName request)
        {
            await _employeeAppSvc.Handle(request);
            return Ok();
        }

        [Route("middleinitial")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateEmployeeMiddleInitial request)
        {
            await _employeeAppSvc.Handle(request);
            return Ok();
        }

        [Route("ssn")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateEmployeeSSN request)
        {
            await _employeeAppSvc.Handle(request);
            return Ok();
        }

        [Route("addressline1")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateAddressLine1 request)
        {
            await _employeeAppSvc.Handle(request);
            return Ok();
        }

        [Route("addressline2")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateAddressLine2 request)
        {
            await _employeeAppSvc.Handle(request);
            return Ok();
        }

        [Route("city")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateCity request)
        {
            await _employeeAppSvc.Handle(request);
            return Ok();
        }

        [Route("stateprovincecode")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateStateProvinceCode request)
        {
            await _employeeAppSvc.Handle(request);
            return Ok();
        }

        [Route("zipcode")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateZipcode request)
        {
            await _employeeAppSvc.Handle(request);
            return Ok();
        }

        [Route("telephone")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateTelephone request)
        {
            await _employeeAppSvc.Handle(request);
            return Ok();
        }

        [Route("maritalstatus")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateMaritalStatus request)
        {
            await _employeeAppSvc.Handle(request);
            return Ok();
        }

        [Route("taxexemptions")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateTaxExemption request)
        {
            await _employeeAppSvc.Handle(request);
            return Ok();
        }

        [Route("payrate")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateEmployeePayRate request)
        {
            await _employeeAppSvc.Handle(request);
            return Ok();
        }

        [Route("startdate")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateEmployeeStartDate request)
        {
            await _employeeAppSvc.Handle(request);
            return Ok();
        }

        [Route("isactive")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateIsActive request)
        {
            await _employeeAppSvc.Handle(request);
            return Ok();
        }
    }
}