using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;


namespace PipefittersSupplyCompany.WebApi.Controllers.Financing.Financiers.Helpers
{
    public class LinkWrapperHandler<TReadModel> : IQueryResultHandler<TReadModel>
    {
        private readonly LinkGenerator _linkGenerator;

        public LinkWrapperHandler(LinkGenerator generator) => _linkGenerator = generator;

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
                            Links = FinancierLinkGenerator.CreateLinks(queryResult.CurrentHttpContext,
                                                                        _linkGenerator,
                                                                        (queryResult.ReadModel as FinancierDetail).FinancierId)
                        };
                        break;
                    case FinancierAddressDetail:
                        queryResult.Links = new LinksWrapper<FinancierAddressDetail>
                        {
                            Value = queryResult.ReadModel as FinancierAddressDetail,
                            Links = FinancierAddressLinkGenerator.CreateLinks(queryResult.CurrentHttpContext,
                                                                               _linkGenerator,
                                                                               (queryResult.ReadModel as FinancierAddressDetail).AddressId)
                        };
                        break;
                    case FinancierContactDetail:
                        queryResult.Links = new LinksWrapper<FinancierContactDetail>
                        {
                            Value = queryResult.ReadModel as FinancierContactDetail,
                            Links = FinancierContactLinkGenerator.CreateLinks(queryResult.CurrentHttpContext,
                                                                               _linkGenerator,
                                                                               (queryResult.ReadModel as FinancierContactDetail).PersonId)
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
                            var links = FinancierLinkGenerator.CreateLinks(queryResult.CurrentHttpContext, _linkGenerator, listItem.FinancierId);

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
                            var links = FinancierAddressLinkGenerator.CreateLinks(queryResult.CurrentHttpContext, _linkGenerator, listItem.AddressId);

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
                            var links = FinancierContactLinkGenerator.CreateLinks(queryResult.CurrentHttpContext, _linkGenerator, listItem.PersonId);

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
    }
}