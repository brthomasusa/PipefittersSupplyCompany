using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.EmployeeReadModels;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;

namespace PipefittersSupplyCompany.WebApi.Controllers.HumanResources
{
    public class LinkGenerationHandler<TReadModel> : IQueryResultHandler<TReadModel>
    {
        private readonly LinkGenerator _linkGenerator;

        public LinkGenerationHandler(LinkGenerator generator) => _linkGenerator = generator;

        public IQueryResultHandler<TReadModel> NextHandler { get; set; }

        public void Process(ref IQueryResult<TReadModel> queryResult)
        {
            if (queryResult.ReadModel is EmployeeDetail)
            {
                queryResult.Links = new LinksWrapper<EmployeeDetail>
                {
                    Value = queryResult.ReadModel as EmployeeDetail,
                    Links = CreateLinks(queryResult.CurrentHttpContext, (queryResult.ReadModel as EmployeeDetail).EmployeeId)
                };
            }
            else if (queryResult.ReadModel is PagedList<EmployeeListItem>)
            {
                LinksWrapperList<EmployeeListItem> linksWrappers = new LinksWrapperList<EmployeeListItem>();
                var employees = queryResult.ReadModel as IEnumerable<EmployeeListItem>;

                foreach (var listItem in employees)
                {
                    var links = CreateLinks(queryResult.CurrentHttpContext, listItem.EmployeeId);

                    linksWrappers.Values.Add
                    (
                        new LinksWrapper<EmployeeListItem>
                        {
                            Value = listItem,
                            Links = links
                        }
                    );
                }

                queryResult.Links = linksWrappers;
            }
            else if (queryResult.ReadModel is PagedList<EmployeeListItemWithRoles>)
            {
                LinksWrapperList<EmployeeListItemWithRoles> linksWrappers = new LinksWrapperList<EmployeeListItemWithRoles>();
                var employees = queryResult.ReadModel as IEnumerable<EmployeeListItemWithRoles>;

                foreach (var listItem in employees)
                {
                    var links = CreateLinks(queryResult.CurrentHttpContext, listItem.EmployeeId);

                    linksWrappers.Values.Add
                    (
                        new LinksWrapper<EmployeeListItemWithRoles>
                        {
                            Value = listItem,
                            Links = links
                        }
                    );
                }

                queryResult.Links = linksWrappers;
            }

            if (NextHandler != null)
            {
                NextHandler.Process(ref queryResult);
            }
        }

        private HashSet<Link> CreateLinks(HttpContext httpContext, Guid id)
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