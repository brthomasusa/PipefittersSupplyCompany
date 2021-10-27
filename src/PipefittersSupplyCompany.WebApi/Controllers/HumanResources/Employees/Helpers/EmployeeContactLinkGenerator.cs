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
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          action: "GetEmployeeContact",
                                                          controller: "EmployeeContacts" ,
                                                          values: new { personId = id }),
                                                          "self", "GET"),
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          action: "DeleteEmployeeContactInfo",
                                                          controller: "EmployeeContacts",
                                                          values: new {  }),
                                                          "delete_employeecontact", "DELETE"),
                    new Link(linkGenerator.GetUriByAction(httpContext,
                                                          action: "EditEmployeeContactInfo",
                                                          controller: "EmployeeContacts" ,
                                                          values: new {  }),
                                                          "update_employeecontact", "PUT")
                };

            return links;
        }
    }
}