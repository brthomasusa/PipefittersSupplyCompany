using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPatch]
        [Route("patchfinancierinfo/{financierId:Guid}")]
        [ServiceFilter(typeof(FinancierPatchActionAttribute))]
        public async Task<IActionResult> PatchFinancierInfo(Guid financierId, [FromBody] JsonPatchDocument<EditFinancierInfo> patchDoc)
        {
            try
            {
                if (patchDoc is null)
                {
                    _logger.LogError("patchDoc object sent from client is null.");
                    return BadRequest("patchDoc object is null.");
                }

                var writeModel = HttpContext.Items["EditFinancierInfo"] as EditFinancierInfo;
                patchDoc.ApplyTo(writeModel);

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
                DoFinancierDependencyCheck queryParams =
                    new DoFinancierDependencyCheck
                    {
                        FinancierID = writeModel.Id
                    };

                IActionResult retValue = await _queryRequestHandler.Handle<DoFinancierDependencyCheck>(queryParams, HttpContext);
                if (retValue is BadRequestObjectResult)
                {
                    return retValue;
                }

                await _commandHandler.Handle(writeModel);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // Get Address and Contact list

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
        [Route("{financierId:Guid}/contacts")]
        public async Task<IActionResult> GetFinancierContacts(Guid financierId, [FromQuery] PagingParameters pagingParams)
        {
            GetFinancierContacts queryParams =
                new GetFinancierContacts
                {
                    FinancierID = financierId,
                    Page = pagingParams.Page,
                    PageSize = pagingParams.PageSize
                };

            var retValue = await _queryRequestHandler.Handle<GetFinancierContacts>(queryParams, HttpContext);

            return retValue;
        }
    }
}