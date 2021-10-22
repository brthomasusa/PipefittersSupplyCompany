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
        [Route("{financierId:Guid}/addresses")]
        public async Task<IActionResult> GetFinancierAddresses(Guid financierId, [FromQuery] PagingParameters pagingParams)
        {
            GetFinancierAddresses queryParams =
                new GetFinancierAddresses
                {
                    FinancierID = financierId,
                    Page = pagingParams.Page,
                    PageSize = pagingParams.PageSize
                };

            var retValue = await _queryRequestHandler.Handle<GetFinancierAddresses>(queryParams, HttpContext);

            return retValue;
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

            var retValue = await _queryRequestHandler.Handle<GetFinancierAddress>(queryParams, HttpContext);

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

                IActionResult retValue = await _queryRequestHandler.Handle<GetFinancierAddress>(queryParams, HttpContext);

                return CreatedAtAction(nameof(GetFinancierAddressDetails), new { addressId = writeModel.AddressId }, (retValue as OkObjectResult).Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}