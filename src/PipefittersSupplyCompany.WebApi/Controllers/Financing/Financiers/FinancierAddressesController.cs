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
    public class FinancierAddressesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IFinancierQueryRequestHandler _queryRequestHandler;
        private readonly FinancierAggregateCommandHandler _commandHandler;

        public FinancierAddressesController
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
        [Route("addressdetails/{addressId:int}")]
        public async Task<IActionResult> GetFinancierAddressDetails(int addressId, [FromQuery] PagingParameters pagingParams)
        {
            GetFinancierAddress queryParams =
                new GetFinancierAddress
                {
                    AddressID = addressId,
                };

            var retValue = await _queryRequestHandler.Handle<GetFinancierAddress>(queryParams, HttpContext, Response);

            return retValue;
        }

        [HttpPost]
        [Route("createfinancieraddressinfo")]
        public async Task<IActionResult> CreateFinancierAddressInfo([FromBody] CreateFinancierAddressInfo writeModel)
        {
            try
            {
                await _commandHandler.Handle(writeModel);
                GetFinancierAddress queryParams = new GetFinancierAddress { AddressID = writeModel.AddressId };

                IActionResult retValue = await _queryRequestHandler.Handle<GetFinancierAddress>(queryParams, HttpContext, Response);

                return CreatedAtAction(nameof(GetFinancierAddressDetails), new { addressId = writeModel.AddressId }, (retValue as OkObjectResult).Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPut]
        [Route("editfinancieraddressinfo")]
        public async Task<IActionResult> EditFinancierAddressInfo([FromBody] EditFinancierAddressInfo writeModel)
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
        [Route("deletefinancieraddressinfo")]
        public async Task<IActionResult> DeleteFinancierAddressInfo([FromBody] DeleteFinancierAddressInfo writeModel)
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