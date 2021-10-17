using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;

namespace PipefittersSupplyCompany.WebApi.Interfaces
{
    public interface IQueryResult<TReadModel>
    {
        PagedList<TReadModel> ReadModels { get; set; }
        IReadModel ReadModel { get; set; }
        HttpContext CurrentHttpContext { get; set; }
        ILinksWrapper Links { get; set; }
    }
}