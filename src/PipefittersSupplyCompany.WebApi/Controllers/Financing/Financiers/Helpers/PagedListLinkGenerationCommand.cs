using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing.Financiers.Helpers
{
    public class PagedListLinkGenerationCommand
    {
        public static ILinksWrapper Execute<TReadModel>
        (
            PagedList<TReadModel> queryResult,
            HttpContext httpContext,
            LinkGenerator generator
        )
        {
            switch (queryResult)
            {
                case PagedList<FinancierListItem>:
                    LinksWrapperList<FinancierListItem> linksWrappers = new LinksWrapperList<FinancierListItem>();
                    var Employees = queryResult.ReadModels as IEnumerable<FinancierListItem>;

                    foreach (var listItem in Employees)
                    {
                        var links = FinancierLinkGenerator.CreateLinks(httpContext, generator, listItem.FinancierId);


                        linksWrappers.Values.Add
                        (
                            new LinksWrapper<FinancierListItem>
                            {
                                Value = listItem,
                                Links = links
                            }
                        );
                    }

                    return linksWrappers;

                case PagedList<FinancierAddressListItem>:
                    LinksWrapperList<FinancierAddressListItem> addressLinksWrappers = new LinksWrapperList<FinancierAddressListItem>();
                    var financierAddresses = queryResult.ReadModels as IEnumerable<FinancierAddressListItem>;

                    foreach (var listItem in financierAddresses)
                    {
                        var links = FinancierAddressLinkGenerator.CreateLinks(httpContext, generator, listItem.AddressId);

                        addressLinksWrappers.Values.Add
                        (
                            new LinksWrapper<FinancierAddressListItem>
                            {
                                Value = listItem,
                                Links = links
                            }
                        );
                    }

                    return addressLinksWrappers;

                case PagedList<FinancierContactListItem>:
                    LinksWrapperList<FinancierContactListItem> contactLinksWrappers = new LinksWrapperList<FinancierContactListItem>();
                    var EmployeeContacts = queryResult.ReadModels as IEnumerable<FinancierContactListItem>;

                    foreach (var listItem in EmployeeContacts)
                    {
                        var links = FinancierContactLinkGenerator.CreateLinks(httpContext, generator, listItem.PersonId);

                        contactLinksWrappers.Values.Add
                        (
                            new LinksWrapper<FinancierContactListItem>
                            {
                                Value = listItem,
                                Links = links
                            }
                        );
                    }

                    return contactLinksWrappers;

                default:
                    throw new ArgumentException("Unknown ReadModel", nameof(queryResult));
            }
        }
    }
}