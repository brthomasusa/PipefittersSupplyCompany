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
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          action: "Details",
                                                          controller: "Employees",
                                                          values: new {employeeId = id }),
                                                          "self", "GET"),
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          action: "DeleteEmployeeInfo",
                                                          controller: "Employees",
                                                          values: new {  }),
                                                          "delete_employee", "DELETE"),
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          action:  "EditEmployeeInfo",
                                                          controller: "Employees",
                                                          values: new {  }),
                                                          "update_employee", "PUT"),
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          action:  "PatchEmployeeInfo",
                                                          controller: "Employees",
                                                          values: new { employeeId = id }),
                                                          "patch_employee", "PATCH"),
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          action:  "GetEmployeeAddresses",
                                                          controller: "Employees",
                                                          values: new { employeeId = id }),
                                                          "addresses", "GET"),
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          action:  "GetEmployeeContacts",
                                                          controller: "Employees",
                                                          values: new { employeeId = id }),
                                                          "contacts", "GET"),
                };

            return links;
        }
    }
}