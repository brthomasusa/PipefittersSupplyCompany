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
                    case FinancierAddressDetail:
                        queryResult.Links = new LinksWrapper<FinancierAddressDetail>
                        {
                            Value = queryResult.ReadModel as FinancierAddressDetail,
                            Links = CreateFinancierAddressLinks(queryResult.CurrentHttpContext, (queryResult.ReadModel as FinancierAddressDetail).AddressId)
                        };
                        break;
                    case FinancierContactDetail:
                        queryResult.Links = new LinksWrapper<FinancierContactDetail>
                        {
                            Value = queryResult.ReadModel as FinancierContactDetail,
                            Links = CreateFinancierContactLinks(queryResult.CurrentHttpContext, (queryResult.ReadModel as FinancierContactDetail).PersonId)
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
                    case PagedList<FinancierAddressListItem>:
                        LinksWrapperList<FinancierAddressListItem> addressLinksWrappers = new LinksWrapperList<FinancierAddressListItem>();
                        var financierAddresses = queryResult.ReadModels.ReadModels as IEnumerable<FinancierAddressListItem>;

                        foreach (var listItem in financierAddresses)
                        {
                            var links = CreateFinancierLinks(queryResult.CurrentHttpContext, listItem.FinancierId);

                            addressLinksWrappers.Values.Add
                            (
                                new LinksWrapper<FinancierAddressListItem>
                                {
                                    Value = listItem,
                                    Links = links
                                }
                            );
                        }

                        queryResult.Links = addressLinksWrappers;
                        break;
                    case PagedList<FinancierContactListItem>:
                        LinksWrapperList<FinancierContactListItem> contactLinksWrappers = new LinksWrapperList<FinancierContactListItem>();
                        var financierContacts = queryResult.ReadModels.ReadModels as IEnumerable<FinancierContactListItem>;

                        foreach (var listItem in financierContacts)
                        {
                            var links = CreateFinancierLinks(queryResult.CurrentHttpContext, listItem.FinancierId);

                            contactLinksWrappers.Values.Add
                            (
                                new LinksWrapper<FinancierContactListItem>
                                {
                                    Value = listItem,
                                    Links = links
                                }
                            );
                        }

                        queryResult.Links = contactLinksWrappers;
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
                    new Link(_linkGenerator.GetUriByAction(httpContext, "GetFinancierDetails", values: new { financierId = id }), "self", "GET"),
                    // new Link(_linkGenerator.GetUriByAction(httpContext, "deletefinancierinfo", values: new { financierId = id }), "delete_financier", "DELETE"),
                    // new Link(_linkGenerator.GetUriByAction(httpContext, "editfinancierinfo", values: new { financierId = id }), "update_financier", "PUT"),
                    // new Link(_linkGenerator.GetUriByAction(httpContext, "patchfinancierinfo", values: new { financierId = id }), "patch_financier", "PATCH")
                };

            return links;
        }

        private HashSet<Link> CreateFinancierAddressLinks(HttpContext httpContext, int id)
        {
            var links = new HashSet<Link>
                {
                    new Link(_linkGenerator.GetUriByAction(httpContext, "GetFinancierAddressDetails", values: new { addressId = id }), "self", "GET"),
                    // new Link(_linkGenerator.GetUriByAction(httpContext, "deletefinancieraddressinfo", values: new { addressId = id }), "delete_financier", "DELETE"),
                    // new Link(_linkGenerator.GetUriByAction(httpContext, "editfinancieraddressinfo", values: new { addressId = id }), "update_financier", "PUT"),
                    // new Link(_linkGenerator.GetUriByAction(httpContext, "patchfinancieraddressinfo", values: new { addressId = id }), "patch_financier", "PATCH")
                };

            return links;
        }

        private HashSet<Link> CreateFinancierContactLinks(HttpContext httpContext, int id)
        {
            var links = new HashSet<Link>
                {
                    new Link(_linkGenerator.GetUriByAction(httpContext, "GetFinancierContactDetails", values: new { personId = id }), "self", "GET"),
                    // new Link(_linkGenerator.GetUriByAction(httpContext, "deletefinancierContactinfo", values: new { personId = id }), "delete_financiercontact", "DELETE"),
                    // new Link(_linkGenerator.GetUriByAction(httpContext, "editfinanciercontactinfo", values: new { personId = id }), "update_financiercontact", "PUT"),
                    // new Link(_linkGenerator.GetUriByAction(httpContext, "patchfinanciercontactinfo", values: new { personId = id }), "patch_financiercontact", "PATCH")
                };

            return links;
        }
    }
}