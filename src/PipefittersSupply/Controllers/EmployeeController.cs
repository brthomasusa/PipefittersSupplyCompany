using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using PipefittersSupply.Infrastructure.Interfaces;
using PipefittersSupply.Infrastructure.Application.Services;
using static PipefittersSupply.Infrastructure.Application.Commands.HumanResources.EmployeeCommand;
using static PipefittersSupply.Infrastructure.Queries.HumanResources.QueryParameters;
using static PipefittersSupply.Infrastructure.Queries.HumanResources.ReadModels;

namespace PipefittersSupply.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly EmployeeAppicationService _employeeAppSvc;
        private readonly IEmployeeQueryService _employeeQrySvc;

        public EmployeeController
        (
            EmployeeAppicationService appSvc,
            IEmployeeQueryService _qrySvc,
            ILogger<EmployeeController> logger
        )
        {
            _employeeAppSvc = appSvc;
            _employeeQrySvc = _qrySvc;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post(V1.CreateEmployee request) =>
            await RequestHandler.HandleCommand<V1.CreateEmployee>(request, _employeeAppSvc.Handle, _logger);

        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateEmployee request) =>
            await RequestHandler.HandleCommand<V1.UpdateEmployee>(request, _employeeAppSvc.Handle, _logger);

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> Get([FromQuery] GetEmployees request) =>
            await RequestHandler.HandleQuery(() => _employeeQrySvc.Query(request), _logger);

        [HttpGet]
        [Route("supervisedby")]
        public async Task<IActionResult> Get([FromQuery] GetEmployeesSupervisedBy request) =>
            await RequestHandler.HandleQuery(() => _employeeQrySvc.Query(request), _logger);

        [HttpGet]
        [Route("employeesoftype")]
        public async Task<IActionResult> Get([FromQuery] GetEmployeesOfEmployeeType request) =>
            await RequestHandler.HandleQuery(() => _employeeQrySvc.Query(request), _logger);

        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> Get([FromQuery] GetEmployee request) =>
            await RequestHandler.HandleQuery(() => _employeeQrySvc.Query(request), _logger);
    }
}