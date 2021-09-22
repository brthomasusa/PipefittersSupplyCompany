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

        public LinksWrapper<EmployeeDetails> GenerateLinks(EmployeeDetails employee, HttpContext httpContext)
        {
            var LinksWrapper = new LinksWrapper<EmployeeDetails>
            {
                Value = employee,
                Links = CreateLinkForEmployeeListItem(httpContext, employee.EmployeeId)
            };

            return LinksWrapper;
        }

        public LinksWrapperList<EmployeeListItems> GenerateLinks(IEnumerable<EmployeeListItems> employees, HttpContext httpContext)
        {
            LinksWrapperList<EmployeeListItems> linksWrappers = new LinksWrapperList<EmployeeListItems>();

            foreach (var listItem in employees)
            {
                var links = CreateLinkForEmployeeListItem(httpContext, listItem.EmployeeId);

                linksWrappers.Values.Add
                (
                    new LinksWrapper<EmployeeListItems>
                    {
                        Value = listItem,
                        Links = links
                    }
                );
            }

            return linksWrappers;
        }

        public LinksWrapperList<EmployeeListItemsWithRoles> GenerateLinks(IEnumerable<EmployeeListItemsWithRoles> employees, HttpContext httpContext)
        {
            LinksWrapperList<EmployeeListItemsWithRoles> linksWrappers = new LinksWrapperList<EmployeeListItemsWithRoles>();

            foreach (var listItem in employees)
            {
                linksWrappers.Values.Add
                (
                    new LinksWrapper<EmployeeListItemsWithRoles>
                    {
                        Value = listItem,
                        Links = CreateLinkForEmployeeListItem(httpContext, listItem.EmployeeId)
                    }
                );
            }

            return linksWrappers;
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