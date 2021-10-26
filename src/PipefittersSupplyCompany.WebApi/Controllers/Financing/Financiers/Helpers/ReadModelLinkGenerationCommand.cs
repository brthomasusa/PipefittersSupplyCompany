using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing.Financiers.Helpers
{
    public class ReadModelLinkGenerationCommand
    {
        public static ILinksWrapper Execute<TReadModel>
        (
            TReadModel queryResult,
            HttpContext httpContext,
            LinkGenerator generator
        )
        {
            switch (queryResult)
            {
                case FinancierDetail:
                    var employeeDetailLinks = new LinksWrapper<FinancierDetail>
                    {
                        Value = queryResult as FinancierDetail,
                        Links = FinancierLinkGenerator.CreateLinks(httpContext, generator, (queryResult as FinancierDetail).FinancierId)
                    };

                    return employeeDetailLinks;

                case FinancierAddressDetail:
                    var addressDetailLinks = new LinksWrapper<FinancierAddressDetail>
                    {
                        Value = queryResult as FinancierAddressDetail,
                        Links = FinancierAddressLinkGenerator.CreateLinks(httpContext, generator, (queryResult as FinancierAddressDetail).AddressId)
                    };

                    return addressDetailLinks;

                case FinancierContactDetail:
                    var contactDetailLinks = new LinksWrapper<FinancierContactDetail>
                    {
                        Value = queryResult as FinancierContactDetail,
                        Links = FinancierContactLinkGenerator.CreateLinks(httpContext, generator, (queryResult as FinancierContactDetail).PersonId)
                    };

                    return contactDetailLinks;

                default:
                    throw new ArgumentException("Unknown ReadModel", nameof(queryResult));
            }

            throw new NotImplementedException();
        }
    }
}