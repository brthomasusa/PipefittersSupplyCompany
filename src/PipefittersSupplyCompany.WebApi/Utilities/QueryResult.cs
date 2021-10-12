using Microsoft.AspNetCore.Http;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces;

namespace PipefittersSupplyCompany.WebApi.Utilities
{
    public class QueryResult : IQueryResult
    {
        public IReadModel ReadModel { get; set; }
        public HttpContext CurrentHttpContext { get; set; }
        public ILinksWrapper Links { get; set; }
    }
}