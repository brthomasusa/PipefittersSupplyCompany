using System.Text.Json;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources;

namespace PipefittersSupplyCompany.WebApi.Controllers.HumanResources.Employees.Helpers
{
    public class ResponseHeaderHandler<TReadModel> : IQueryResultHandler<TReadModel>
    {
        public IQueryResultHandler<TReadModel> NextHandler { get; set; }

        public void Process(ref IQueryResult<TReadModel> queryResult)
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