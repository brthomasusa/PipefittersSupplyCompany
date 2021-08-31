using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources;
using static PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources.EmployeeAggregateCommand;

namespace PipefittersSupplyCompany.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly EmployeeAggregateCommandHandler _employeeCmdHdlr;

        public EmployeesController
        (
            EmployeeAggregateCommandHandler cmdHdlr,
            ILogger<EmployeesController> logger
        )
        {
            _employeeCmdHdlr = cmdHdlr;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post(V1.CreateEmployeeInfo request) =>
            await RequestHandler.HandleCommand<V1.CreateEmployeeInfo>(request, _employeeCmdHdlr.Handle, _logger);
    }
}