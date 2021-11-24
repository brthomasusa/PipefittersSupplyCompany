using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PipefittersSupplyCompany.Infrastructure.Interfaces;

namespace PipefittersSupplyCompany.WebApi.Interfaces
{
    public interface IQueryRequestHandler
    {
        Task<IActionResult> Handle<TQueryParam>(TQueryParam queryParam, HttpContext httpContext, HttpResponse httpResponse);
    }
}