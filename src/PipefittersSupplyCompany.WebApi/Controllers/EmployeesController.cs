using System;
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
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
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
        public async Task<IActionResult> Get([FromQuery] PagingParameters pagingParams)
        {
            GetEmployees queryParams = new GetEmployees { Page = pagingParams.Page ?? 1, PageSize = pagingParams.PageSize ?? 4 };

            return await RequestHandler.HandleQuery
                        (
                            () => _employeeQrySvc.Query(queryParams),
                            _logger
                        );
        }

        [HttpGet]
        [Route("supervisedby")]
        public async Task<IActionResult> Get([FromQuery] GetEmployeesSupervisedBy queryParams) =>
            await RequestHandler.HandleQuery
            (
                () => _employeeQrySvc.Query(queryParams),
                _logger
            );

        [HttpGet]
        [Route("employeesofrole")]
        public async Task<IActionResult> Get([FromQuery] GetEmployeesOfRole queryParams) =>
            await RequestHandler.HandleQuery
            (
                () => _employeeQrySvc.Query(queryParams),
                _logger
            );

        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> Get([FromQuery] GetEmployee queryParams) =>
            await RequestHandler.HandleQuery
            (
                () => _employeeQrySvc.Query(queryParams),
                _logger
            );

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] V1.CreateEmployeeInfo command) =>
            await RequestHandler.HandleCommand<V1.CreateEmployeeInfo>
            (
                command,
                _employeeCmdHdlr.Handle,
                _logger
            );
    }
}