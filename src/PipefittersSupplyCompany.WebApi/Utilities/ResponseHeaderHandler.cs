using System.Text.Json;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.EmployeeReadModels;

namespace PipefittersSupplyCompany.WebApi.Utilities
{
    public class ResponseHeaderHandler<TReadModel> : IQueryResultHandler where TReadModel : IReadModel
    {
        public IQueryResultHandler NextHandler { get; set; }

        public void Process(ref IQueryResult queryResult)
        {
            // Add pagination info to response.header
            queryResult.CurrentHttpContext
                .Response
                .Headers
                .Add("X-Pagination", JsonSerializer.Serialize((queryResult.ReadModel as PagedList<TReadModel>).MetaData));

            if (NextHandler != null)
            {
                NextHandler.Process(ref queryResult);
            }
        }
    }
}