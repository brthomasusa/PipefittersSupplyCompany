// using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;

namespace PipefittersSupplyCompany.WebApi.Controllers.Base
{
    public class AddPagingInfoToResponseHeaderCommand<T>
    {
        public static void Execute(PagedList<T> queryResult, HttpResponse httpResponse)
        {
            httpResponse
                .Headers
                .Add("X-Pagination", JsonConvert.SerializeObject((queryResult as PagedList<T>).MetaData));
        }
    }
}