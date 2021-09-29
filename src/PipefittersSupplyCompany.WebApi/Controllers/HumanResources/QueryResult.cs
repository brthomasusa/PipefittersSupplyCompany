using System;
using Microsoft.AspNetCore.Http;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.WebApi.Controllers.HumanResources;

namespace PipefittersSupplyCompany.WebApi.Controllers.HumanResources
{
    public class QueryResult : IQueryResult
    {
        public IReadModel ReadModel { get; set; }
        public HttpContext CurrentHttpContext { get; set; }
        public ILinkGenerator HateOasLinksGenerator { get; set; }
        public ILinksWrapper Links { get; set; }
    }
}