using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.WebApi.Utilities;

namespace PipefittersSupplyCompany.WebApi.Controllers.HumanResources.Employees.Helpers
{
    public class EmployeeContactLinkGenerator
    {
        public static HashSet<Link> CreateLinks(HttpContext httpContext, LinkGenerator linkGenerator, int id)
        {
            var links = new HashSet<Link>
                {
                    new Link(linkGenerator.GetUriByAction(httpContext, "GetEmployeeContactDetails", values: new { personId = id }), "self", "GET"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "DeleteEmployeeContactInfo", values: new { personId = id }), "delete_employeecontact", "DELETE"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "EditEmployeeContactInfo", values: new { personId = id }), "update_employeecontact", "PUT")
                };

            return links;
        }
    }
}