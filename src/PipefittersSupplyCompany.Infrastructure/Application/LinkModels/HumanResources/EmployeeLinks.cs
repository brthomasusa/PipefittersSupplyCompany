using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources;
using PipefittersSupplyCompany.Infrastructure.Application.LinkModels;


namespace PipefittersSupplyCompany.Infrastructure.Application.LinkModels.HumanResources
{
    public class EmployeeLinks
    {
        private readonly LinkGenerator _linkGenerator;

        public EmployeeLinks(LinkGenerator generator) => _linkGenerator = generator;

        public LinkResponse TryGenerateLinks(IEnumerable<ReadModels.EmployeeDetails> employees, HttpContext httpContext)
        {


            return null;
        }

        private LinkResponse ReturnLinkedEmployees(IEnumerable<ReadModels.EmployeeDetails> employees, HttpContext httpContext)
        {


            return null;
        }
    }
}