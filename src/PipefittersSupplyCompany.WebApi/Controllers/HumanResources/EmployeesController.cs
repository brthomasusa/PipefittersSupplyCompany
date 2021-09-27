using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources;
using static PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources.EmployeeAggregateCommand;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.QueryParameters;
using PipefittersSupplyCompany.Infrastructure.Application.LinkModels.HumanResources;
using PipefittersSupplyCompany.WebApi.Controllers.ActionFilters;

namespace PipefittersSupplyCompany.WebApi.Controllers.HumanResources
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly EmployeeAggregateCommandHandler _employeeCmdHdlr;
        private readonly IEmployeeQueryService _employeeQrySvc;
        private readonly EmployeeLinks _employeeLinks;

        public EmployeesController
        (
            EmployeeAggregateCommandHandler cmdHdlr,
            IEmployeeQueryService qrySvc,
            ILoggerManager logger,
            EmployeeLinks employeeLinks
        )
        {
            _employeeCmdHdlr = cmdHdlr;
            _employeeQrySvc = qrySvc;
            _logger = logger;
            _employeeLinks = employeeLinks;
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


            var retValue = await EmployeeAggregateQueryHandler.HandleQuery
                        (
                            () => _employeeQrySvc.Query(queryParams),
                            _logger,
                            HttpContext,
                            _employeeLinks
                        );

            return retValue;
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

            return await EmployeeAggregateQueryHandler.HandleQuery
            (
                () => _employeeQrySvc.Query(queryParams),
                _logger,
                HttpContext,
                _employeeLinks
            );
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

            return await EmployeeAggregateQueryHandler.HandleQuery
            (
                () => _employeeQrySvc.Query(queryParams),
                _logger,
                HttpContext,
                _employeeLinks
            );
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

            return await EmployeeAggregateQueryHandler.HandleQuery
            (
                () => _employeeQrySvc.Query(queryParams),
                _logger,
                HttpContext,
                _employeeLinks
            );
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

            return await EmployeeAggregateQueryHandler.HandleQuery
            (
                () => _employeeQrySvc.Query(queryParams),
                _logger,
                HttpContext,
                _employeeLinks
            );
        }

        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [Route("address/{addressId:int}")]
        public async Task<IActionResult> GetEmployeeAddress(int addressId, [FromQuery] PagingParameters pagingParams)
        {
            GetEmployeeAddress queryParams =
                new GetEmployeeAddress
                {
                    AddressID = addressId,
                };

            return await EmployeeAggregateQueryHandler.HandleQuery
            (
                () => _employeeQrySvc.Query(queryParams),
                _logger,
                HttpContext,
                _employeeLinks
            );
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

            return await EmployeeAggregateQueryHandler.HandleQuery
            (
                () => _employeeQrySvc.Query(queryParams),
                _logger,
                HttpContext,
                _employeeLinks
            );
        }

        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [Route("contact/{personId:int}")]
        public async Task<IActionResult> GetEmployeeContact(int personId, [FromQuery] PagingParameters pagingParams)
        {
            GetEmployeeContact queryParams =
                new GetEmployeeContact
                {
                    PersonID = personId,
                };

            return await EmployeeAggregateQueryHandler.HandleQuery
            (
                () => _employeeQrySvc.Query(queryParams),
                _logger,
                HttpContext,
                _employeeLinks
            );
        }

        [HttpPost]
        [Route("createemployeeinfo")]
        public async Task<IActionResult> CreateEmployeeInfo([FromBody] CreateEmployeeInfo command) =>
            await EmployeeAggregateQueryHandler.HandleCommand<CreateEmployeeInfo>
            (
                command,
                _employeeCmdHdlr.Handle,
                _logger
            );

        [HttpPut]
        [Route("editemployeeinfo/{employeeId}")]
        public async Task<IActionResult> EditEmployeeInfo(Guid employeeId, [FromBody] EditEmployeeInfo command) =>
            await EmployeeAggregateQueryHandler.HandleCommand<EditEmployeeInfo>
            (
                command,
                _employeeCmdHdlr.Handle,
                _logger
            );

        [HttpDelete]
        [Route("deleteemployeeinfo/{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeInfo(Guid employeeId)
        {
            var command = new DeleteEmployeeInfo { Id = employeeId };

            return await EmployeeAggregateQueryHandler.HandleCommand<DeleteEmployeeInfo>
            (
                command,
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

            var command = HttpContext.Items["EditEmployeeInfo"] as EditEmployeeInfo;
            patchDoc.ApplyTo(command);

            return await EmployeeAggregateQueryHandler.HandleCommand<EditEmployeeInfo>
            (
                command,
                _employeeCmdHdlr.Handle,
                _logger
            );
        }

        [HttpPost]
        [Route("{employeeId:Guid}/createemployeeaddressinfo")]
        public async Task<IActionResult> CreateEmployeeAddressInfo([FromBody] CreateEmployeeAddressInfo command) =>
            await EmployeeAggregateQueryHandler.HandleCommand<CreateEmployeeAddressInfo>
            (
                command,
                _employeeCmdHdlr.Handle,
                _logger
            );





















    }
}