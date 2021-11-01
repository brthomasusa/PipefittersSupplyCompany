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
using PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing.LoanAgreements;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing.LoanAgreements
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/LoanAgreements")]
    [ApiController]
    public class LoanAgreementsController : ControllerBase
    {

        private readonly ILoggerManager _logger;
        // private readonly ILoanAgreementQueryRequestHandler _queryRequestHandler;
        private readonly LoanAgreementAggregateCommandHandler _commandHandler;

        public LoanAgreementsController
        (
            ILoggerManager logger,
            LoanAgreementAggregateCommandHandler commandHandler
        )
        {
            _logger = logger;
            _commandHandler = commandHandler;
        }

        // [Route("createfinancierinfo")]
        // public async Task<IActionResult> CreateLoanAgreementInfo([FromBody] CreateLoanAgreementInfo writeModel)
        // {
        //     try
        //     {
        //         await _commandHandler.Handle(writeModel);

        //         GetFinancier queryParams = new GetFinancier { FinancierID = writeModel.Id };

        //         IActionResult retValue = await _queryRequestHandler.Handle<GetFinancier>(queryParams, HttpContext);

        //         return CreatedAtAction(nameof(GetFinancierDetails), new { financierId = writeModel.Id }, (retValue as OkObjectResult).Value);

        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex.Message);
        //         return BadRequest(ex.Message);
        //     }
        // }
    }
}