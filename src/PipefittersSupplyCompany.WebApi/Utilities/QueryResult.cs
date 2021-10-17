using Microsoft.AspNetCore.Http;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;

namespace PipefittersSupplyCompany.WebApi.Utilities
{
    public class QueryResult<TReadModel> : IQueryResult<TReadModel>
    {
        public PagedList<TReadModel> ReadModels { get; set; }
        public IReadModel ReadModel { get; set; }
        public HttpContext CurrentHttpContext { get; set; }
        public ILinksWrapper Links { get; set; }
    }
}