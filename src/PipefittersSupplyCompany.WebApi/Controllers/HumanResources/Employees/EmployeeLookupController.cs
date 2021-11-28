using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.HumanResources;

namespace PipefittersSupplyCompany.WebApi.Controllers.HumanResources.Employees
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/[controller]")]
    public class EmployeeLookupController : ControllerBase
    {
        private readonly IEmployeeQueryService _employeeQrySvc;
        private readonly ILoggerManager _logger;

        public EmployeeLookupController(IEmployeeQueryService employeeQrySvc, ILoggerManager logger)
        {
            _employeeQrySvc = employeeQrySvc;
            _logger = logger;
        }

        [HttpGet]
        [Route("supervisorlookup")]
        public async Task<IActionResult> GetSupervisorLookUps()
        {
            try
            {
                var retValue = await _employeeQrySvc.GetSupervisorLookups();
                if (retValue == null)
                {
                    return NotFound();
                }
                return Ok(retValue);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception has been thrown: {ex}");
                return BadRequest(ex.Message);
            }
        }
    }
}