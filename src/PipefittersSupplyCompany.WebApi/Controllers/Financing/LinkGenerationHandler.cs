using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing
{
    public class LinkGenerationHandler<TReadModel>
    {
        private readonly LinkGenerator _linkGenerator;

        public LinkGenerationHandler(LinkGenerator generator) => _linkGenerator = generator;

        public IQueryResultHandler<TReadModel> NextHandler { get; set; }

        // public void Process<TReadModel>(ref IQueryResult<TReadModel> queryResult)
        // {
        //     if (queryResult.ReadModel is FinancierDetail)
        //     {
        //         queryResult.Links = new LinksWrapper<FinancierDetail>
        //         {
        //             Value = queryResult.ReadModel as FinancierDetail,
        //             Links = CreateLinks(queryResult.CurrentHttpContext, (queryResult.ReadModel as FinancierDetail).FinancierId)
        //         };
        //     }
        //     else if (queryResult.ReadModel is PagedList<FinancierListItem>)
        //     {
        //         LinksWrapperList<FinancierListItem> linksWrappers = new LinksWrapperList<FinancierListItem>();
        //         var employees = queryResult.ReadModel as IEnumerable<FinancierListItem>;

        //         foreach (var listItem in employees)
        //         {
        //             var links = CreateLinks(queryResult.CurrentHttpContext, listItem.FinancierId);

        //             linksWrappers.Values.Add
        //             (
        //                 new LinksWrapper<FinancierListItem>
        //                 {
        //                     Value = listItem,
        //                     Links = links
        //                 }
        //             );
        //         }

        //         queryResult.Links = linksWrappers;
        //     }

        //     if (NextHandler != null)
        //     {
        //         NextHandler.Process(ref queryResult);
        //     }
        // }

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