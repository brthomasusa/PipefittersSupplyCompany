using Microsoft.AspNetCore.Mvc;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources;

namespace PipefittersSupplyCompany.WebApi.Controllers.HumanResources
{
    public static class ActionResultCheckDependencyCommand
    {
        public static IActionResult CreateActionResult(EmployeeDependencyCheckResult queryResult)
        {
            if (queryResult is not null)
            {
                if (queryResult.Addresses > 0 || queryResult.Contacts > 0)
                {
                    string msg = "This employee has attached addresses and/or contacts and therefore can not be deleted.";
                    return new BadRequestObjectResult(new { Message = msg });
                }

                return new OkObjectResult(queryResult);
            }

            return new NotFoundObjectResult(new { Message = "No employee found that matches the search criteria." });
        }
    }
}