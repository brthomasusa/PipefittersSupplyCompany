using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;
using PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing.Financiers;

namespace PipefittersSupplyCompany.WebApi.ActionFilters
{
    public class FinancierPatchActionAttribute : IAsyncActionFilter
    {
        private readonly IFinancierQueryService _queryService;
        private readonly ILoggerManager _logger;

        public FinancierPatchActionAttribute(IFinancierQueryService queryService, ILoggerManager logger)
        {
            _queryService = queryService;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var financierId = (Guid)context.ActionArguments["financierId"];
            GetFinancier queryParams = new GetFinancier { FinancierID = financierId };
            FinancierDetail readModel = await _queryService.Query(queryParams);

            if (readModel is null)
            {
                _logger.LogInfo($"A financier with id: {financierId} does not exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                EditFinancierInfo editFinancierInfo = new EditFinancierInfo
                {
                    Id = readModel.FinancierId,
                    FinancierName = readModel.FinancierName,
                    Telephone = readModel.Telephone,
                    IsActive = readModel.IsActive,
                    UserId = readModel.UserId
                };

                context.HttpContext.Items.Add("EditFinancierInfo", editFinancierInfo);
                await next();
            }
        }
    }
}