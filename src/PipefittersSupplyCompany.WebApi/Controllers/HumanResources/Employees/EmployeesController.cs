using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.JsonPatch;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.HumanResources;
using PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;
using PipefittersSupplyCompany.WebApi.ActionFilters;
using PipefittersSupplyCompany.WebApi.Controllers.HumanResources.Employees.Helpers;

namespace PipefittersSupplyCompany.WebApi.Controllers.HumanResources.Employees
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly EmployeeAggregateCommandHandler _employeeCmdHdlr;
        private readonly IEmployeeQueryService _employeeQrySvc;
        private readonly IEmployeeQueryRequestHandler _employeeQryReqHdler;
        private readonly LinkGenerator _linkGenerator;

        public EmployeesController
        (
            EmployeeAggregateCommandHandler cmdHdlr,
            IEmployeeQueryService qrySvc,
            IEmployeeQueryRequestHandler employeeQryReqHdler,
            ILoggerManager logger,
            LinkGenerator generator
        )
        {
            _employeeCmdHdlr = cmdHdlr;
            _employeeQrySvc = qrySvc;
            _employeeQryReqHdler = employeeQryReqHdler;
            _logger = logger;
            _linkGenerator = generator;
        }

        [HttpGet]
        [Route("list")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetEmployees([FromQuery] PagingParameters pagingParams)
        {
            GetEmployees queryParams =
                new GetEmployees
                {
                    Page = pagingParams.Page,
                    PageSize = pagingParams.PageSize
                };

            try
            {
                var retValue = await _employeeQryReqHdler.Handle<GetEmployees>(queryParams, HttpContext);
                return retValue;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception has been thrown: {ex}");
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [Route("supervisedby/{supervisorId:Guid}")]
        public async Task<IActionResult> GetSupervisedBy(Guid supervisorId, [FromQuery] PagingParameters pagingParams)
        {
            GetEmployeesSupervisedBy queryParams =
                new GetEmployeesSupervisedBy
                {
                    SupervisorID = supervisorId,
                    Page = pagingParams.Page,
                    PageSize = pagingParams.PageSize
                };

            try
            {
                var retValue = await _employeeQryReqHdler.Handle<GetEmployeesSupervisedBy>(queryParams, HttpContext);
                return retValue;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception has been thrown: {ex}");
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [Route("employeesofrole/{roleId:Guid}")]
        public async Task<IActionResult> GetRoleMembers(Guid roleId, [FromQuery] PagingParameters pagingParams)
        {
            GetEmployeesOfRole queryParams =
                new GetEmployeesOfRole
                {
                    RoleID = roleId,
                    Page = pagingParams.Page,
                    PageSize = pagingParams.PageSize
                };

            try
            {
                var retValue = await _employeeQryReqHdler.Handle<GetEmployeesOfRole>(queryParams, HttpContext);
                return retValue;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception has been thrown: {ex}");
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [Route("details/{employeeId}")]
        public async Task<IActionResult> Details(Guid employeeId)
        {
            GetEmployee queryParams =
                new GetEmployee
                {
                    EmployeeID = employeeId
                };

            try
            {
                var retValue = await _employeeQryReqHdler.Handle<GetEmployee>(queryParams, HttpContext);
                return retValue;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception has been thrown: {ex}");
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost]
        [Route("createemployeeinfo")]
        public async Task<IActionResult> CreateEmployeeInfo([FromBody] CreateEmployeeInfo writeModel) =>
            await EmployeeAggregateRequestHandler.HandleCommand<CreateEmployeeInfo>
            (
                writeModel,
                _employeeCmdHdlr.Handle,
                _logger
            );

        [HttpPut]
        [Route("editemployeeinfo/{employeeId}")]
        public async Task<IActionResult> EditEmployeeInfo(Guid employeeId, [FromBody] EditEmployeeInfo writeModel) =>
            await EmployeeAggregateRequestHandler.HandleCommand<EditEmployeeInfo>
            (
                writeModel,
                _employeeCmdHdlr.Handle,
                _logger
            );

        [HttpDelete]
        [Route("deleteemployeeinfo/{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeInfo(Guid employeeId)
        {
            var writeModel = new DeleteEmployeeInfo { Id = employeeId };

            DoEmployeeDependencyCheck queryParams =
                new DoEmployeeDependencyCheck
                {
                    EmployeeID = writeModel.Id
                };

            IActionResult retValue = await _employeeQryReqHdler.Handle<DoEmployeeDependencyCheck>(queryParams, HttpContext);
            if (retValue is BadRequestObjectResult)
            {
                return retValue;
            }

            return await EmployeeAggregateRequestHandler.HandleCommand<DeleteEmployeeInfo>
            (
                writeModel,
                _employeeCmdHdlr.Handle,
                _logger
            );
        }

        [HttpPatch]
        [Route("patchemployeeinfo/{employeeId:Guid}")]
        [ServiceFilter(typeof(EmployeePatchActionAttribute))]
        public async Task<IActionResult> PatchEmployeeInfo(Guid employeeId, [FromBody] JsonPatchDocument<EditEmployeeInfo> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null.");
            }

            var writeModel = HttpContext.Items["EditEmployeeInfo"] as EditEmployeeInfo;
            patchDoc.ApplyTo(writeModel);

            return await EmployeeAggregateRequestHandler.HandleCommand<EditEmployeeInfo>
            (
                writeModel,
                _employeeCmdHdlr.Handle,
                _logger
            );
        }
    }
}