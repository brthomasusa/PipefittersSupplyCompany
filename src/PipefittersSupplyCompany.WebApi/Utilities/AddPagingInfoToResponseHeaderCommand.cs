using System.Text.Json;
using Microsoft.AspNetCore.Http;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;

namespace PipefittersSupplyCompany.WebApi.Utilities
{
    public class AddPagingInfoToResponseHeaderCommand<T>
    {
        public static void Execute(PagedList<T> queryResult, HttpContext httpContext)
        {
            httpContext
                .Response
                .Headers
                .Add("X-Pagination", JsonSerializer.Serialize((queryResult as PagedList<T>).MetaData));
        }
    }
}