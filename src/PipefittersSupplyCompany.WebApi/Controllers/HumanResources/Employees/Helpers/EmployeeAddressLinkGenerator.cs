using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.WebApi.Utilities;

namespace PipefittersSupplyCompany.WebApi.Controllers.HumanResources.Employees.Helpers
{
    public class EmployeeAddressLinkGenerator
    {
        public static HashSet<Link> CreateLinks(HttpContext httpContext, LinkGenerator linkGenerator, int id)
        {
            var links = new HashSet<Link>
                {
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          action: "GetEmployeeAddress",
                                                          controller: "EmployeeAddresses",
                                                          values: new { addressId = id }),
                                                          "self", "GET"),
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          action: "DeleteEmployeeAddressInfo",
                                                          controller: "EmployeeAddresses",
                                                          values: new {  }),
                                                          "delete_employeeaddress", "DELETE"),
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          action: "EditEmployeeAddressInfo",
                                                          controller: "EmployeeAddresses",
                                                          values: new {  }),
                                                          "update_employeeaddress", "PUT")
                };

            return links;
        }
    }
}