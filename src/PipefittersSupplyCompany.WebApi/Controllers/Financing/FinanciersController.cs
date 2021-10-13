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


using Microsoft.AspNetCore.Mvc;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinanciersController : ControllerBase
    {

    }
}