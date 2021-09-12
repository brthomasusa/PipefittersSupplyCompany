using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources;
using static PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources.EmployeeAggregateCommand;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.QueryParameters;
using PipefittersSupplyCompany.WebApi.Controllers.ActionFilters;

namespace PipefittersSupplyCompany.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly EmployeeAggregateCommandHandler _employeeCmdHdlr;
        private readonly IEmployeeQueryService _employeeQrySvc;

        public EmployeesController
        (
            EmployeeAggregateCommandHandler cmdHdlr,
            IEmployeeQueryService qrySvc,
            ILoggerManager logger
        )
        {
            _employeeCmdHdlr = cmdHdlr;
            _employeeQrySvc = qrySvc;
            _logger = logger;
        }

        [HttpGet]
        [Route("list")]
        // [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetEmployees([FromQuery] PagingParameters pagingParams)
        {
            GetEmployees queryParams =
                new GetEmployees
                {
                    Page = pagingParams.Page ?? 1,
                    PageSize = pagingParams.PageSize ?? 4
                };

            return await RequestHandler.HandleQuery
                        (
                            () => _employeeQrySvc.Query(queryParams),
                            _logger
                        );
        }

        [HttpGet]
        [Route("supervisedby/{supervisorId:Guid}")]
        public async Task<IActionResult> GetSupervisedBy(Guid supervisorId, [FromQuery] PagingParameters pagingParams)
        {
            GetEmployeesSupervisedBy queryParams =
                new GetEmployeesSupervisedBy
                {
                    SupervisorID = supervisorId,
                    Page = pagingParams.Page ?? 1,
                    PageSize = pagingParams.PageSize ?? 4
                };

            return await RequestHandler.HandleQuery
            (
                () => _employeeQrySvc.Query(queryParams),
                _logger
            );
        }

        [HttpGet]
        [Route("employeesofrole/{roleId:Guid}")]
        public async Task<IActionResult> GetRoleMembers(Guid roleId, [FromQuery] PagingParameters pagingParams)
        {
            GetEmployeesOfRole queryParams =
                new GetEmployeesOfRole
                {
                    RoleID = roleId,
                    Page = pagingParams.Page ?? 1,
                    PageSize = pagingParams.PageSize ?? 4
                };

            return await RequestHandler.HandleQuery
            (
                () => _employeeQrySvc.Query(queryParams),
                _logger
            );
        }

        [HttpGet]
        [Route("details/{employeeId:Guid}")]
        public async Task<IActionResult> GetEmployee(Guid employeeId)
        {
            GetEmployee queryParams =
                new GetEmployee
                {
                    EmployeeID = employeeId
                };

            return await RequestHandler.HandleQuery
            (
                () => _employeeQrySvc.Query(queryParams),
                _logger
            );
        }

        [HttpPost]
        [Route("createemployeeinfo")]
        public async Task<IActionResult> CreateEmployeeInfo([FromBody] V1.CreateEmployeeInfo command) =>
            await RequestHandler.HandleCommand<V1.CreateEmployeeInfo>
            (
                command,
                _employeeCmdHdlr.Handle,
                _logger
            );

        [HttpPut]
        [Route("editemployeeinfo")]
        public async Task<IActionResult> UpdateEmployeeInfo([FromBody] V1.EditEmployeeInfo command) =>
            await RequestHandler.HandleCommand<V1.EditEmployeeInfo>
            (
                command,
                _employeeCmdHdlr.Handle,
                _logger
            );

        [HttpDelete]
        [Route("deleteemployeeinfo/{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeInfo(Guid employeeId)
        {
            var command = new V1.DeleteEmployeeInfo { Id = employeeId };

            return await RequestHandler.HandleCommand<V1.DeleteEmployeeInfo>
            (
                command,
                _employeeCmdHdlr.Handle,
                _logger
            );
        }


        [HttpPatch]
        [Route("patchemployeeinfo/{employeeId:Guid}")]
        [ServiceFilter(typeof(EmployeePatchActionAttribute))]
        public async Task<IActionResult> PatchEmployeeInfo(Guid employeeId, [FromBody] JsonPatchDocument<V1.EditEmployeeInfo> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null.");
            }

            var command = HttpContext.Items["EditEmployeeInfo"] as V1.EditEmployeeInfo;
            patchDoc.ApplyTo(command);

            return await RequestHandler.HandleCommand<V1.EditEmployeeInfo>
            (
                command,
                _employeeCmdHdlr.Handle,
                _logger
            );
        }
    }
}