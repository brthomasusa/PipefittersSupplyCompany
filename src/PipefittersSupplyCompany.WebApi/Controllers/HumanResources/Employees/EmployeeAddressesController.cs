using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;
using PipefittersSupplyCompany.WebApi.ActionFilters;

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
                var retValue = await _employeeQryReqHdler.Handle<GetEmployeeAddress>(queryParams, HttpContext, Response);
                return retValue;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception has been thrown: {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("createemployeeaddressinfo")]
        public async Task<IActionResult> CreateEmployeeAddressInfo([FromBody] CreateEmployeeAddressInfo writeModel)
        {
            try
            {
                await _employeeCmdHdlr.Handle(writeModel);

                GetEmployeeAddress queryParams = new GetEmployeeAddress { AddressID = writeModel.AddressId };

                IActionResult retValue = await _employeeQryReqHdler.Handle<GetEmployeeAddress>(queryParams, HttpContext, Response);

                return CreatedAtAction(nameof(GetEmployeeAddress), new { addressId = writeModel.AddressId }, (retValue as OkObjectResult).Value);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception has been thrown: {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("editemployeeaddressinfo")]
        public async Task<IActionResult> EditEmployeeAddressInfo([FromBody] EditEmployeeAddressInfo writeModel)
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
        [Route("deleteemployeeaddressinfo")]
        public async Task<IActionResult> DeleteEmployeeAddressInfo([FromBody] DeleteEmployeeAddressInfo writeModel)
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
    }
}
