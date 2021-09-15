using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.ReadModels;
using PipefittersSupplyCompany.Infrastructure.Application.LinkModels;


namespace PipefittersSupplyCompany.Infrastructure.Application.LinkModels.HumanResources
{
    public class EmployeeLinks
    {
        private readonly LinkGenerator _linkGenerator;

        public EmployeeLinks(LinkGenerator generator) => _linkGenerator = generator;

        public LinkResponse TryGenerateLinks(EmployeeDetails employee, HttpContext httpContext)
        {


            return null;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<EmployeeListItems> employees, HttpContext httpContext)
        {
            if (ShouldGenerateLinks(httpContext))
            {
                //
            }

            return null;
        }

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = httpContext.Items["AcceptHeaderMediaType"] as MediaTypeHeaderValue;

            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private LinkResponse ReturnLinkedEmployees(List<EmployeeListItems> employees, HttpContext httpContext)
        {
            foreach (var listItem in employees)
            {
                var employeeLinks = CreateLinkForEmployeeListItem(httpContext, listItem.EmployeeId);
                listItem.Links.UnionWith(employeeLinks);
            }

            return null;
        }

        private HashSet<Link> CreateLinkForEmployeeListItem(HttpContext httpContext, Guid id)
        {
            var links = new HashSet<Link>
            {
                new Link(_linkGenerator.GetUriByAction(httpContext, "details", values: new { employeeId = id }), "self", "GET"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "deleteemployeeinfo", values: new { employeeId = id }), "delete_employee", "DELETE"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "editemployeeinfo", values: new { employeeId = id }), "update_employee", "PUT"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "patchemployeeinfo", values: new { employeeId = id }), "patch_employee", "PATCH")
            };

            return links;
        }
    }
}