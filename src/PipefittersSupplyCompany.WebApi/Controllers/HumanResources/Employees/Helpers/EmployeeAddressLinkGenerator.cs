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
                    new Link(linkGenerator.GetUriByAction(httpContext, "GetEmployeeAddress", values: new { addressId = id }), "self", "GET"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "DeleteEmployeeAddressInfo", values: new {  }), "delete_employeeaddress", "DELETE"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "EditEmployeeAddressInfo", values: new {  }), "update_employeeaddress", "PUT")
                };

            return links;
        }
    }
}