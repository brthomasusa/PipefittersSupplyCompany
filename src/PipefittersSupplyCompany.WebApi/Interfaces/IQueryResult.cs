using Microsoft.AspNetCore.Http;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Controllers.HumanResources;

namespace PipefittersSupplyCompany.WebApi.Interfaces
{
    public interface IQueryResult
    {
        IReadModel ReadModel { get; set; }
        HttpContext CurrentHttpContext { get; set; }
        EmployeeLinks EmployeeLinksGenerator { get; set; }
        ILinksWrapper Links { get; set; }
    }
}