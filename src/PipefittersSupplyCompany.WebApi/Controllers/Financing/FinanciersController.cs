using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;
using PipefittersSupplyCompany.WebApi.ActionFilters;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing
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
        public async Task<IActionResult> Details(Guid financierId)
        {
            GetFinancier queryParams =
                new GetFinancier
                {
                    FinancierID = financierId
                };

            var retValue = await _queryRequestHandler.Handle<GetFinancier>(queryParams, HttpContext);

            return retValue;
        }

    }
}