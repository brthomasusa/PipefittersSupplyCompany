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
    [Route("api/{v:apiVersion}/[controller]")]
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

            var retValue = await _queryRequestHandler.Handle<GetFinancierContact>(queryParams, HttpContext);

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

        [HttpPost]
        [Route("createfinanciercontactinfo")]
        public async Task<IActionResult> CreateFinancierContactInfo([FromBody] CreateFinancierContactInfo writeModel)
        {
            try
            {
                await _commandHandler.Handle(writeModel);
                GetFinancierContact queryParams = new GetFinancierContact { PersonID = writeModel.PersonId };

                IActionResult retValue = await _queryRequestHandler.Handle<GetFinancierContact>(queryParams, HttpContext);

                return CreatedAtAction(nameof(GetFinancierContactDetails), new { personId = writeModel.PersonId }, (retValue as OkObjectResult).Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

    }
}