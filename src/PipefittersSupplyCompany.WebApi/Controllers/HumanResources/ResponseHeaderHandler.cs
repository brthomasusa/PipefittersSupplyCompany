using System.Text.Json;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.EmployeeReadModels;
namespace PipefittersSupplyCompany.WebApi.Controllers.HumanResources
{
    public class ResponseHeaderHandler : IQueryResultHandler
    {
        public IQueryResultHandler NextHandler { get; set; }

        public void Process(ref IQueryResult queryResult)
        {
            // Add pagination info to response.header
            if (queryResult.ReadModel is PagedList<EmployeeListItem>)
            {
                queryResult.CurrentHttpContext
                    .Response
                    .Headers
                    .Add("X-Pagination", JsonSerializer.Serialize((queryResult.ReadModel as PagedList<EmployeeListItem>).MetaData));
            }
            else if (queryResult.ReadModel is PagedList<EmployeeListItemWithRoles>)
            {
                queryResult.CurrentHttpContext
                    .Response
                    .Headers
                    .Add("X-Pagination", JsonSerializer.Serialize((queryResult.ReadModel as PagedList<EmployeeListItemWithRoles>).MetaData));
            }
            else if (queryResult.ReadModel is PagedList<EmployeeAddressListItem>)
            {
                queryResult.CurrentHttpContext
                    .Response
                    .Headers
                    .Add("X-Pagination", JsonSerializer.Serialize((queryResult.ReadModel as PagedList<EmployeeAddressListItem>).MetaData));
            }
            else if (queryResult.ReadModel is PagedList<EmployeeContactListItem>)
            {
                queryResult.CurrentHttpContext
                    .Response
                    .Headers
                    .Add("X-Pagination", JsonSerializer.Serialize((queryResult.ReadModel as PagedList<EmployeeContactListItem>).MetaData));
            }

            if (NextHandler != null)
            {
                NextHandler.Process(ref queryResult);
            }
        }
    }
}