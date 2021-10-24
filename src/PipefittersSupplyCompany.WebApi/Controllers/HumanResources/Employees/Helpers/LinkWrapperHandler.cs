using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;

namespace PipefittersSupplyCompany.WebApi.Controllers.HumanResources.Employees.Helpers
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
                    case EmployeeDetail:
                        queryResult.Links = new LinksWrapper<EmployeeDetail>
                        {
                            Value = queryResult.ReadModel as EmployeeDetail,
                            Links = EmployeeLinkGenerator.CreateLinks(queryResult.CurrentHttpContext,
                                                                      _linkGenerator,
                                                                      (queryResult.ReadModel as EmployeeDetail).EmployeeId)
                        };
                        break;
                    case EmployeeAddressDetail:
                        queryResult.Links = new LinksWrapper<EmployeeAddressDetail>
                        {
                            Value = queryResult.ReadModel as EmployeeAddressDetail,
                            Links = EmployeeAddressLinkGenerator.CreateLinks(queryResult.CurrentHttpContext,
                                                                             _linkGenerator,
                                                                             (queryResult.ReadModel as EmployeeAddressDetail).AddressId)
                        };
                        break;
                    case EmployeeContactDetail:
                        queryResult.Links = new LinksWrapper<EmployeeContactDetail>
                        {
                            Value = queryResult.ReadModel as EmployeeContactDetail,
                            Links = EmployeeContactLinkGenerator.CreateLinks(queryResult.CurrentHttpContext,
                                                                             _linkGenerator,
                                                                             (queryResult.ReadModel as EmployeeContactDetail).PersonId)
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
                    case PagedList<EmployeeListItem>:
                        LinksWrapperList<EmployeeListItem> linksWrappers = new LinksWrapperList<EmployeeListItem>();
                        var Employees = queryResult.ReadModels.ReadModels as IEnumerable<EmployeeListItem>;

                        foreach (var listItem in Employees)
                        {
                            var links = EmployeeLinkGenerator.CreateLinks(queryResult.CurrentHttpContext, _linkGenerator, listItem.EmployeeId);

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
                        break;
                    case PagedList<EmployeeAddressListItem>:
                        LinksWrapperList<EmployeeAddressListItem> addressLinksWrappers = new LinksWrapperList<EmployeeAddressListItem>();
                        var EmployeeAddresses = queryResult.ReadModels.ReadModels as IEnumerable<EmployeeAddressListItem>;

                        foreach (var listItem in EmployeeAddresses)
                        {
                            var links = EmployeeAddressLinkGenerator.CreateLinks(queryResult.CurrentHttpContext, _linkGenerator, listItem.AddressId);

                            addressLinksWrappers.Values.Add
                            (
                                new LinksWrapper<EmployeeAddressListItem>
                                {
                                    Value = listItem,
                                    Links = links
                                }
                            );
                        }

                        queryResult.Links = addressLinksWrappers;
                        break;
                    case PagedList<EmployeeContactListItem>:
                        LinksWrapperList<EmployeeContactListItem> contactLinksWrappers = new LinksWrapperList<EmployeeContactListItem>();
                        var EmployeeContacts = queryResult.ReadModels.ReadModels as IEnumerable<EmployeeContactListItem>;

                        foreach (var listItem in EmployeeContacts)
                        {
                            var links = EmployeeContactLinkGenerator.CreateLinks(queryResult.CurrentHttpContext, _linkGenerator, listItem.PersonId);

                            contactLinksWrappers.Values.Add
                            (
                                new LinksWrapper<EmployeeContactListItem>
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