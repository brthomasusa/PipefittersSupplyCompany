using System.Text.Json;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;

namespace PipefittersSupplyCompany.WebApi.Utilities
{
    public class ResponseHeaderHandler<TReadModel> : IQueryResultHandler<TReadModel>
    {
        public IQueryResultHandler<TReadModel> NextHandler { get; set; }

        public void Process(ref IQueryResult<TReadModel> queryResult)
        {
            // Add pagination info to response.header
            queryResult.CurrentHttpContext
                .Response
                .Headers
                .Add("X-Pagination", JsonSerializer.Serialize((queryResult.ReadModels as PagedList<TReadModel>).MetaData));

            if (NextHandler != null)
            {
                NextHandler.Process(ref queryResult);
            }
        }
    }
}