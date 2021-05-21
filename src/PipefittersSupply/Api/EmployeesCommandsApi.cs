using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PipefittersSupply.Contracts.HumanResources;

namespace PipefittersSupply.Api
{
    [Route("api/Employees")]
    [ApiController]
    public class EmployeesCommandsApi : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(Employee.V1.Create request)
        {
            // Handle request here
            return Ok();
        }
    }
}