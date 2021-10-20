using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;
using PipefittersSupplyCompany.WebApi.ActionFilters;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing.Financiers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    public class FinanciersController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IFinancierQueryRequestHandler _queryRequestHandler;

        public FinanciersController(ILoggerManager logger, IFinancierQueryRequestHandler queryRequestHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _queryRequestHandler = queryRequestHandler ?? throw new ArgumentNullException(nameof(queryRequestHandler));
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
    }
}