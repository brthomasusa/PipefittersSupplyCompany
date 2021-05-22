using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PipefittersSupply.Contracts.HumanResources;

namespace PipefittersSupply.Api
{
    [Route("api/Employees")]
    [ApiController]
    public class EmployeesCommandsApi : ControllerBase
    {
        private readonly EmployeeAppicationService _employeeAppSvc;

        public EmployeesCommandsApi(EmployeeAppicationService appSvc) => _employeeAppSvc = appSvc;
        [HttpPost]
        public async Task<IActionResult> Post(EmployeeCommand.V1.Create request)
        {
            await _employeeAppSvc.Handle(request);
            return Ok();
        }
    }
}