using Microsoft.AspNetCore.Mvc;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing.Financiers
{
    public static class ActionResultCheckDependencyCommand
    {
        public static IActionResult CreateActionResult(FinancierDependencyCheckResult queryResult)
        {
            if (queryResult is not null)
            {
                if (queryResult.Addresses > 0 || queryResult.Contacts > 0)
                {
                    string msg = "This financier has attached addresses and/or contacts and therefore can not be deleted.";
                    return new BadRequestObjectResult(new { Message = msg });
                }

                return new OkObjectResult(queryResult);
            }

            return new NotFoundObjectResult(new { Message = "No financier found that matches the search criteria." });
        }
    }
}