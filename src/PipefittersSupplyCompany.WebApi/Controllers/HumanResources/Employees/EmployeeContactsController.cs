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
    public class EmployeeContactsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly EmployeeAggregateCommandHandler _employeeCmdHdlr;
        private readonly IEmployeeQueryRequestHandler _employeeQryReqHdler;

        public EmployeeContactsController
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
        [Route("contact/{personId:int}")]
        public async Task<IActionResult> GetEmployeeContact(int personId, [FromQuery] PagingParameters pagingParams)
        {
            GetEmployeeContact queryParams =
                new GetEmployeeContact
                {
                    PersonID = personId,
                };

            try
            {
                var retValue = await _employeeQryReqHdler.Handle<GetEmployeeContact>(queryParams, HttpContext);
                return retValue;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception has been thrown: {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("createemployeecontactinfo")]
        public async Task<IActionResult> CreateEmployeeContactInfo([FromBody] CreateEmployeeContactInfo writeModel)
        {
            try
            {
                await _employeeCmdHdlr.Handle(writeModel);

                GetEmployeeContact queryParams = new GetEmployeeContact { PersonID = writeModel.PersonId };

                IActionResult retValue = await _employeeQryReqHdler.Handle<GetEmployeeContact>(queryParams, HttpContext);

                return CreatedAtAction(nameof(GetEmployeeContact), new { personId = writeModel.PersonId }, (retValue as OkObjectResult).Value);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception has been thrown: {ex}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("editemployeecontactinfo")]
        public async Task<IActionResult> EditEmployeeContactInfo([FromBody] EditEmployeeContactInfo writeModel)
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
        [Route("deleteemployeecontactinfo")]
        public async Task<IActionResult> DeleteEmployeeContactInfo([FromBody] DeleteEmployeeContactInfo writeModel)
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