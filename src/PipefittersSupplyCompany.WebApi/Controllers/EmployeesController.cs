using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources;
using static PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources.EmployeeAggregateCommand;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.QueryParameters;

namespace PipefittersSupplyCompany.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly EmployeeAggregateCommandHandler _employeeCmdHdlr;
        private readonly IEmployeeQueryService _employeeQrySvc;

        public EmployeesController
        (
            EmployeeAggregateCommandHandler cmdHdlr,
            IEmployeeQueryService _qrySvc,
            ILogger<EmployeesController> logger
        )
        {
            _employeeCmdHdlr = cmdHdlr;
            _employeeQrySvc = _qrySvc;
            _logger = logger;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> Get([FromQuery] GetEmployees request) =>
            await RequestHandler.HandleQuery
            (
                () => _employeeQrySvc.Query(request),
                _logger
            );

        [HttpGet]
        [Route("supervisedby")]
        public async Task<IActionResult> Get([FromQuery] GetEmployeesSupervisedBy request) =>
            await RequestHandler.HandleQuery
            (
                () => _employeeQrySvc.Query(request),
                _logger
            );

        [HttpGet]
        [Route("employeesofrole")]
        public async Task<IActionResult> Get([FromQuery] GetEmployeesOfRole request) =>
            await RequestHandler.HandleQuery
            (
                () => _employeeQrySvc.Query(request),
                _logger
            );

        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> Get([FromQuery] GetEmployee request) =>
            await RequestHandler.HandleQuery
            (
                () => _employeeQrySvc.Query(request),
                _logger
            );

        [HttpPost]
        public async Task<IActionResult> Post(V1.CreateEmployeeInfo request) =>
            await RequestHandler.HandleCommand<V1.CreateEmployeeInfo>
            (
                request,
                _employeeCmdHdlr.Handle,
                _logger
            );
    }
}