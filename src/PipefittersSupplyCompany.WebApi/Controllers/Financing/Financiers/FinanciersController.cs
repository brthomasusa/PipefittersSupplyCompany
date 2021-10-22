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
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/Financiers")]
    [ApiController]
    public class FinanciersController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IFinancierQueryRequestHandler _queryRequestHandler;
        private readonly FinancierAggregateCommandHandler _commandHandler;

        public FinanciersController
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
        [Route("list")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetFinanciers([FromQuery] PagingParameters pagingParams)
        {
            GetFinanciers queryParams =
                new GetFinanciers
                {
                    Page = pagingParams.Page,
                    PageSize = pagingParams.PageSize
                };

            var retValue = await _queryRequestHandler.Handle<GetFinanciers>(queryParams, HttpContext);

            return retValue;
        }

        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        [Route("details/{financierId}")]
        public async Task<IActionResult> GetFinancierDetails(Guid financierId)
        {
            GetFinancier queryParams =
                new GetFinancier
                {
                    FinancierID = financierId
                };

            var retValue = await _queryRequestHandler.Handle<GetFinancier>(queryParams, HttpContext);

            return retValue;
        }

        [HttpPost]
        [Route("createfinancierinfo")]
        public async Task<IActionResult> CreateFinancierInfo([FromBody] CreateFinancierInfo writeModel)
        {
            try
            {
                await _commandHandler.Handle(writeModel);

                GetFinancier queryParams = new GetFinancier { FinancierID = writeModel.Id };

                IActionResult retValue = await _queryRequestHandler.Handle<GetFinancier>(queryParams, HttpContext);

                return CreatedAtAction(nameof(GetFinancierDetails), new { financierId = writeModel.Id }, (retValue as OkObjectResult).Value);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPut]
        [Route("editfinancierinfo")]
        public async Task<IActionResult> EditFinancierInfo([FromBody] EditFinancierInfo writeModel)
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
        [Route("deletefinancierinfo")]
        public async Task<IActionResult> DeleteFinancierInfo([FromBody] DeleteFinancierInfo writeModel)
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