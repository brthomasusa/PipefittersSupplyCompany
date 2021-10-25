using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;

namespace PipefittersSupplyCompany.WebApi.Controllers.HumanResources.Employees.Helpers
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
                case PagedList<EmployeeListItem>:
                    LinksWrapperList<EmployeeListItem> linksWrappers = new LinksWrapperList<EmployeeListItem>();
                    var Employees = queryResult.ReadModels as IEnumerable<EmployeeListItem>;

                    foreach (var listItem in Employees)
                    {
                        var links = EmployeeLinkGenerator.CreateLinks(httpContext, generator, listItem.EmployeeId);

                        linksWrappers.Values.Add
                        (
                            new LinksWrapper<EmployeeListItem>
                            {
                                Value = listItem,
                                Links = links
                            }
                        );
                    }

                    return linksWrappers;

                case PagedList<EmployeeListItemWithRoles>:
                    LinksWrapperList<EmployeeListItemWithRoles> roleMemberLinksWrappers = new LinksWrapperList<EmployeeListItemWithRoles>();
                    var roleMembers = queryResult.ReadModels as IEnumerable<EmployeeListItemWithRoles>;

                    foreach (var listItem in roleMembers)
                    {
                        var links = EmployeeLinkGenerator.CreateLinks(httpContext, generator, listItem.RoleId);

                        roleMemberLinksWrappers.Values.Add
                        (
                            new LinksWrapper<EmployeeListItemWithRoles>
                            {
                                Value = listItem,
                                Links = links
                            }
                        );
                    }

                    return roleMemberLinksWrappers;

                case PagedList<EmployeeAddressListItem>:
                    LinksWrapperList<EmployeeAddressListItem> addressLinksWrappers = new LinksWrapperList<EmployeeAddressListItem>();
                    var EmployeeAddresses = queryResult.ReadModels as IEnumerable<EmployeeAddressListItem>;

                    foreach (var listItem in EmployeeAddresses)
                    {
                        var links = EmployeeAddressLinkGenerator.CreateLinks(httpContext, generator, listItem.AddressId);

                        addressLinksWrappers.Values.Add
                        (
                            new LinksWrapper<EmployeeAddressListItem>
                            {
                                Value = listItem,
                                Links = links
                            }
                        );
                    }

                    return addressLinksWrappers;

                case PagedList<EmployeeContactListItem>:
                    LinksWrapperList<EmployeeContactListItem> contactLinksWrappers = new LinksWrapperList<EmployeeContactListItem>();
                    var EmployeeContacts = queryResult.ReadModels as IEnumerable<EmployeeContactListItem>;

                    foreach (var listItem in EmployeeContacts)
                    {
                        var links = EmployeeContactLinkGenerator.CreateLinks(httpContext, generator, listItem.PersonId);

                        contactLinksWrappers.Values.Add
                        (
                            new LinksWrapper<EmployeeContactListItem>
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