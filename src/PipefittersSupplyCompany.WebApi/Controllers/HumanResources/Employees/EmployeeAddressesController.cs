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
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/Employees/")]
    public class EmployeeAddressesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly EmployeeAggregateCommandHandler _employeeCmdHdlr;
        private readonly IEmployeeQueryRequestHandler _employeeQryReqHdler;

        public EmployeeAddressesController
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
                var retValue = await _employeeQryReqHdler.Handle<GetEmployeeAddresses>(queryParams, HttpContext);
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
        [Route("address/{addressId:int}")]
        public async Task<IActionResult> GetEmployeeAddress(int addressId, [FromQuery] PagingParameters pagingParams)
        {
            GetEmployeeAddress queryParams =
                new GetEmployeeAddress
                {
                    AddressID = addressId,
                };

            try
            {
                var retValue = await _employeeQryReqHdler.Handle<GetEmployeeAddress>(queryParams, HttpContext);
                return retValue;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception has been thrown: {ex}");
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost]
        [Route("{employeeId:Guid}/createemployeeaddressinfo")]
        public async Task<IActionResult> CreateEmployeeAddressInfo([FromBody] CreateEmployeeAddressInfo writeModel) =>
            await EmployeeAggregateRequestHandler.HandleCommand<CreateEmployeeAddressInfo>
            (
                writeModel,
                _employeeCmdHdlr.Handle,
                _logger
            );
    }
}