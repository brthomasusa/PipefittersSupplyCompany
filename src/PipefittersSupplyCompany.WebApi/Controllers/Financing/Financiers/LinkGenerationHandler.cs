using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing.Financiers
{
    public class LinkGenerationHandler<TReadModel> : IQueryResultHandler<TReadModel>
    {
        private readonly LinkGenerator _linkGenerator;

        public LinkGenerationHandler(LinkGenerator generator) => _linkGenerator = generator;

        public IQueryResultHandler<TReadModel> NextHandler { get; set; }

        public void Process(ref IQueryResult<TReadModel> queryResult)
        {
            if (queryResult.ReadModel is not null)  // This is a single IReadModel
            {
                switch (queryResult.ReadModel)
                {
                    case FinancierDetail:
                        queryResult.Links = new LinksWrapper<FinancierDetail>
                        {
                            Value = queryResult.ReadModel as FinancierDetail,
                            Links = CreateFinancierLinks(queryResult.CurrentHttpContext, (queryResult.ReadModel as FinancierDetail).FinancierId)
                        };
                        break;
                    default:
                        throw new ArgumentException("Unknown ReadModel", nameof(queryResult.ReadModel));
                }
            }
            else if (queryResult.ReadModels is not null) // This is a PagedList<IReadModel>
            {
                switch (queryResult.ReadModels)
                {
                    case PagedList<FinancierListItem>:
                        LinksWrapperList<FinancierListItem> linksWrappers = new LinksWrapperList<FinancierListItem>();
                        var financiers = queryResult.ReadModels.ReadModels as IEnumerable<FinancierListItem>;

                        foreach (var listItem in financiers)
                        {
                            var links = CreateFinancierLinks(queryResult.CurrentHttpContext, listItem.FinancierId);

                            linksWrappers.Values.Add
                            (
                                new LinksWrapper<FinancierListItem>
                                {
                                    Value = listItem,
                                    Links = links
                                }
                            );
                        }

                        queryResult.Links = linksWrappers;
                        break;
                    default:
                        throw new ArgumentException("Unknown ReadModel", nameof(queryResult.ReadModels));
                }
            }
            else
            {
                throw new ArgumentException("The query result lacks a ReadModel or PagedList");
            }

            if (NextHandler != null)
            {
                NextHandler.Process(ref queryResult);
            }
        }

        private HashSet<Link> CreateFinancierLinks(HttpContext httpContext, Guid id)
        {
            var links = new HashSet<Link>
                {
                    new Link(_linkGenerator.GetUriByAction(httpContext, "details", values: new { financierId = id }), "self", "GET"),
                    // new Link(_linkGenerator.GetUriByAction(httpContext, "deletefinancierinfo", values: new { financierId = id }), "delete_financier", "DELETE"),
                    // new Link(_linkGenerator.GetUriByAction(httpContext, "editfinancierinfo", values: new { financierId = id }), "update_financier", "PUT"),
                    // new Link(_linkGenerator.GetUriByAction(httpContext, "patchfinancierinfo", values: new { financierId = id }), "patch_financier", "PATCH")
                };

            return links;
        }
    }
}