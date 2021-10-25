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
                var retValue = await _employeeQryReqHdler.Handle<GetEmployeeContacts>(queryParams, HttpContext);
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
                return new BadRequestObjectResult(ex.Message);
            }
        }


    }
}