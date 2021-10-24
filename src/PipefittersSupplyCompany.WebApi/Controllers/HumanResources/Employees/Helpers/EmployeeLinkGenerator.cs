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
                    new Link(linkGenerator.GetUriByAction(httpContext, "GetEmployeeDetails", values: new { EmployeeId = id }), "self", "GET"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "DeleteEmployeeInfo", values: new { EmployeeId = id }), "delete_Employee", "DELETE"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "EditEmployeeInfo", values: new { EmployeeId = id }), "update_Employee", "PUT"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "PatchEmployeeInfo", values: new { EmployeeId = id }), "patch_Employee", "PATCH"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "GetEmployeeAddresses", values: new { EmployeeId = id }), "addresses", "GET"),
                    new Link(linkGenerator.GetUriByAction(httpContext, "GetEmployeeContacts", values: new { EmployeeId = id }), "contacts", "GET"),
                };

            return links;
        }
    }
}