using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.JsonPatch;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing;
using static PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing.FinancierAggregateWriteModels;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing.FinancierQueryParameters;
using PipefittersSupplyCompany.WebApi.Utilities;
using PipefittersSupplyCompany.WebApi.ActionFilters;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    public class FinanciersController : ControllerBase
    {
        private readonly ILoggerManager _logger;

        public FinanciersController(ILoggerManager logger) =>
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));


    }
}