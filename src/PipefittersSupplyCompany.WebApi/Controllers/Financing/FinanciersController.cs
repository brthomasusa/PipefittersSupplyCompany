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
        private readonly IFinancierQueryService _queryService;

        public FinanciersController(ILoggerManager logger, IFinancierQueryService queryService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _queryService = queryService ?? throw new ArgumentNullException(nameof(queryService));
        }

        // [HttpGet]
        // [Route("list")]
        // [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        // public async Task<IActionResult> GetFinanciers([FromQuery] PagingParameters pagingParams)
        // {
        //     GetFinanciers queryParams =
        //         new GetFinanciers
        //         {
        //             Page = pagingParams.Page,
        //             PageSize = pagingParams.PageSize
        //         };

        //     FinancierQueryRequestHander requestHander =
        //         new FinancierQueryRequestHander(_queryService);

        //     var retValue = await requestHander.Handle<GetFinanciers>(queryParams, HttpContext);

        //     return new OkObjectResult(retValue);
        // }

    }
}