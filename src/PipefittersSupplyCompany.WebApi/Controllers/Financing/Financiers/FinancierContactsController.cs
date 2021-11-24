using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.WebApi.ActionFilters;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;
using PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing.Financiers;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing.Financiers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/Financiers/")]
    public class FinancierContactsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IFinancierQueryRequestHandler _queryRequestHandler;
        private readonly FinancierAggregateCommandHandler _commandHandler;

        public FinancierContactsController
        (
            ILoggerManager logger,
            IFinancierQueryRequestHandler queryRequestHandler,
            FinancierAggregateCommandHandler commandHandler
        )
        {
            _logger = logger;
            _queryRequestHandler = queryRequestHandler;
            _commandHandler = commandHandler;
        }

        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [Route("contactdetails/{personId:int}")]
        public async Task<IActionResult> GetFinancierContactDetails(int personId, [FromQuery] PagingParameters pagingParams)
        {
            GetFinancierContact queryParams =
                new GetFinancierContact
                {
                    PersonID = personId,
                };

            var retValue = await _queryRequestHandler.Handle<GetFinancierContact>(queryParams, HttpContext, Response);

            return retValue;
        }

        [HttpPost]
        [Route("createfinanciercontactinfo")]
        public async Task<IActionResult> CreateFinancierContactInfo([FromBody] CreateFinancierContactInfo writeModel)
        {
            try
            {
                await _commandHandler.Handle(writeModel);
                GetFinancierContact queryParams = new GetFinancierContact { PersonID = writeModel.PersonId };

                IActionResult retValue = await _queryRequestHandler.Handle<GetFinancierContact>(queryParams, HttpContext, Response);

                return CreatedAtAction(nameof(GetFinancierContactDetails), new { personId = writeModel.PersonId }, (retValue as OkObjectResult).Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPut]
        [Route("editfinanciercontactinfo")]
        public async Task<IActionResult> EditFinancierContactInfo([FromBody] EditFinancierContactInfo writeModel)
        {
            try
            {
                await _commandHandler.Handle(writeModel);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deletefinanciercontactinfo")]
        public async Task<IActionResult> DeleteFinancierContactInfo([FromBody] DeleteFinancierContactInfo writeModel)
        {
            try
            {
                await _commandHandler.Handle(writeModel);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}