using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.WebApi.Utilities;

namespace PipefittersSupplyCompany.WebApi.Controllers.HumanResources.Employees.Helpers
{
    public class EmployeeLinkGenerator
    {
        public static HashSet<Link> CreateLinks(HttpContext httpContext, LinkGenerator linkGenerator, Guid id)
        {
            var links = new HashSet<Link>
                {
                    new Link(linkGenerator.GetUriByAction(httpContext, "Details", values: new {employeeId = id }), "self", "GET"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "DeleteEmployeeInfo", values: new {  }), "delete_employee", "DELETE"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "EditEmployeeInfo", values: new {  }), "update_employee", "PUT"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "PatchEmployeeInfo", values: new { employeeId = id }), "patch_employee", "PATCH"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "GetEmployeeAddresses", values: new { employeeId = id }), "addresses", "GET"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "GetEmployeeContacts", values: new { employeeId = id }), "contacts", "GET"),
                };

            return links;
        }
    }
}