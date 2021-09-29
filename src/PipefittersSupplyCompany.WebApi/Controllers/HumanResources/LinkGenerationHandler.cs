using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.ReadModels;
using PipefittersSupplyCompany.WebApi.Interfaces;

namespace PipefittersSupplyCompany.WebApi.Controllers.HumanResources
{
    public class LinkGenerationHandler : IQueryResultHandler
    {
        public IQueryResultHandler NextHandler { get; set; }

        public void Process(ref IQueryResult queryResult)
        {
            EmployeeLinks linkGenerator = queryResult.HateOasLinksGenerator as EmployeeLinks;

            if (ShouldGenerateLinks(queryResult.CurrentHttpContext))
            {
                if (queryResult.ReadModel is EmployeeDetail)
                {
                    queryResult.Links = linkGenerator.GenerateLinks(queryResult.ReadModel as EmployeeDetail,
                                                                    queryResult.CurrentHttpContext);
                }

                if (queryResult.ReadModel is PagedList<EmployeeListItem>)
                {
                    queryResult.Links = linkGenerator.GenerateLinks(queryResult.ReadModel as IEnumerable<EmployeeListItem>,
                                                                    queryResult.CurrentHttpContext);
                }

                if (queryResult.ReadModel is PagedList<EmployeeListItemWithRoles>)
                {
                    queryResult.Links = linkGenerator.GenerateLinks(queryResult.ReadModel as IEnumerable<EmployeeListItemWithRoles>,
                                                                    queryResult.CurrentHttpContext);
                }
            }

            if (NextHandler != null)
            {
                NextHandler.Process(ref queryResult);
            }
        }

        private static bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = httpContext.Items["AcceptHeaderMediaType"] as MediaTypeHeaderValue;

            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}