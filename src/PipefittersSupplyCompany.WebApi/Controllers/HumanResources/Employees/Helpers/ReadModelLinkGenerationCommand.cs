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
                case EmployeeDetail:
                    var employeeDetailLinks = new LinksWrapper<EmployeeDetail>
                    {
                        Value = queryResult as EmployeeDetail,
                        Links = EmployeeLinkGenerator.CreateLinks(httpContext, generator, (queryResult as EmployeeDetail).EmployeeId)
                    };

                    return employeeDetailLinks;

                case EmployeeAddressDetail:
                    var addressDetailLinks = new LinksWrapper<EmployeeAddressDetail>
                    {
                        Value = queryResult as EmployeeAddressDetail,
                        Links = EmployeeAddressLinkGenerator.CreateLinks(httpContext, generator, (queryResult as EmployeeAddressDetail).AddressId)
                    };

                    return addressDetailLinks;

                case EmployeeContactDetail:
                    var contactDetailLinks = new LinksWrapper<EmployeeContactDetail>
                    {
                        Value = queryResult as EmployeeContactDetail,
                        Links = EmployeeContactLinkGenerator.CreateLinks(httpContext, generator, (queryResult as EmployeeContactDetail).PersonId)
                    };

                    return contactDetailLinks;

                default:
                    throw new ArgumentException("Unknown ReadModel", nameof(queryResult));
            }

            throw new NotImplementedException();
        }
    }
}