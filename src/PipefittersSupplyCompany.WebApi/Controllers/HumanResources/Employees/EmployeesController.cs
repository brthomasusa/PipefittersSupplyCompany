using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.JsonPatch;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;
using PipefittersSupplyCompany.WebApi.ActionFilters;

namespace PipefittersSupplyCompany.WebApi.Controllers.HumanResources.Employees
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly EmployeeAggregateCommandHandler _employeeCmdHdlr;
        private readonly IEmployeeQueryRequestHandler _employeeQryReqHdler;

        public EmployeesController
        (
            EmployeeAggregateCommandHandler cmdHdlr,
            IEmployeeQueryRequestHandler employeeQryReqHdler,
            ILoggerManager logger
        )
        {
            _employeeCmdHdlr = cmdHdlr;
            _employeeQryReqHdler = employeeQryReqHdler;
            _logger = logger;
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
                var retValue = await _employeeQryReqHdler.Handle<GetEmployees>(queryParams, HttpContext, Response);
                return retValue;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception has been thrown: {ex}");
                return BadRequest(ex.Message);
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
                var retValue = await _employeeQryReqHdler.Handle<GetEmployeesSupervisedBy>(queryParams, HttpContext, Response);
                return retValue;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception has been thrown: {ex}");
                return BadRequest(ex.Message);
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
                var retValue = await _employeeQryReqHdler.Handle<GetEmployeesOfRole>(queryParams, HttpContext, Response);
                return retValue;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception has been thrown: {ex}");
                return BadRequest(ex.Message);
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
                var retValue = await _employeeQryReqHdler.Handle<GetEmployee>(queryParams, HttpContext, Response);
                return retValue;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception has been thrown: {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("createemployeeinfo")]
        public async Task<IActionResult> CreateEmployeeInfo([FromBody] CreateEmployeeInfo writeModel)
        {
            try
            {
                await _employeeCmdHdlr.Handle(writeModel);

                GetEmployee queryParams = new GetEmployee { EmployeeID = writeModel.Id };

                IActionResult retValue = await _employeeQryReqHdler.Handle<GetEmployee>(queryParams, HttpContext, Response);

                return CreatedAtAction(nameof(Details), new { employeeId = writeModel.Id }, (retValue as OkObjectResult).Value);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("editemployeeinfo")]
        public async Task<IActionResult> EditEmployeeInfo([FromBody] EditEmployeeInfo writeModel)
        {
            try
            {
                await _employeeCmdHdlr.Handle(writeModel);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteemployeeinfo")]
        public async Task<IActionResult> DeleteEmployeeInfo([FromBody] DeleteEmployeeInfo writeModel)
        {
            try
            {
                DoEmployeeDependencyCheck queryParams =
                    new DoEmployeeDependencyCheck
                    {
                        EmployeeID = writeModel.Id
                    };

                IActionResult retValue = await _employeeQryReqHdler.Handle<DoEmployeeDependencyCheck>(queryParams, HttpContext, Response);
                if (retValue is BadRequestObjectResult)
                {
                    return retValue;
                }
                await _employeeCmdHdlr.Handle(writeModel);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("patchemployeeinfo/{employeeId:Guid}")]
        [ServiceFilter(typeof(EmployeePatchActionAttribute))]
        public async Task<IActionResult> PatchEmployeeInfo(Guid employeeId, [FromBody] JsonPatchDocument<EditEmployeeInfo> patchDoc)
        {
            try
            {
                if (patchDoc is null)
                {
                    _logger.LogError("patchDoc object sent from client is null.");
                    return BadRequest("patchDoc object is null.");
                }

                var writeModel = HttpContext.Items["EditEmployeeInfo"] as EditEmployeeInfo;
                patchDoc.ApplyTo(writeModel);

                await _employeeCmdHdlr.Handle(writeModel);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [Route("{employeeId:Guid}/addresses")]
        public async Task<IActionResult> GetEmployeeAddresses(Guid employeeId, [FromQuery] PagingParameters pagingParams)
        {
            GetEmployeeAddresses queryParams =
                new GetEmployeeAddresses
                {
                    EmployeeID = employeeId,
                    Page = pagingParams.Page,
                    PageSize = pagingParams.PageSize
                };

            try
            {
                var retValue = await _employeeQryReqHdler.Handle<GetEmployeeAddresses>(queryParams, HttpContext, Response);
                return retValue;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception has been thrown: {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [Route("{employeeId:Guid}/contacts")]
        public async Task<IActionResult> GetEmployeeContacts(Guid employeeId, [FromQuery] PagingParameters pagingParams)
        {
            GetEmployeeContacts queryParams =
                new GetEmployeeContacts
                {
                    EmployeeID = employeeId,
                    Page = pagingParams.Page,
                    PageSize = pagingParams.PageSize
                };

            try
            {
                var retValue = await _employeeQryReqHdler.Handle<GetEmployeeContacts>(queryParams, HttpContext, Response);
                return retValue;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception has been thrown: {ex}");
                return BadRequest(ex.Message);
            }
        }


        //TODO Create common exception handler to return BadRequest

    }
}