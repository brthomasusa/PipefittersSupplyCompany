using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Controllers.Base;
using PipefittersSupplyCompany.Infrastructure.Interfaces.HumanResources;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources;


namespace PipefittersSupplyCompany.WebApi.Controllers.HumanResources.Employees.Helpers
{
    public class EmployeeQueryRequestHandler : IEmployeeQueryRequestHandler
    {
        private readonly IEmployeeQueryService _queryService;
        private readonly LinkGenerator _linkGenerator;

        public EmployeeQueryRequestHandler(IEmployeeQueryService queryService, LinkGenerator generator)
        {
            _queryService = queryService;
            _linkGenerator = generator;
        }

        public async Task<IActionResult> Handle<TQueryParam>
        (
            TQueryParam queryParam,
            HttpContext httpContext
        ) =>
            queryParam switch
            {
                GetEmployees param =>
                    ActionResultPagedListCommand.CreateActionResult<EmployeeListItem>(await _queryService.Query(param),
                                                                                      httpContext,
                                                                                      _linkGenerator,
                                                                                      PagedListLinkGenerationCommand.Execute<EmployeeListItem>),
                GetEmployee param =>
                    ActionResultReadModelCommand.CreateActionResult<EmployeeDetail>(await _queryService.Query(param),
                                                                                    httpContext,
                                                                                    _linkGenerator,
                                                                                    ReadModelLinkGenerationCommand.Execute<EmployeeDetail>),
                // GetFinancierAddresses param
                //     => ActionResultPagedListCommand.CreateActionResult<FinancierAddressListItem>(await _queryService.Query(param), httpContext, _linkGenerator),
                // GetFinancierAddress param
                //     => FinancierReadModelCommand.CreateActionResult<FinancierAddressDetail>(await _queryService.Query(param), httpContext, _linkGenerator),
                // GetFinancierContacts param
                //     => ActionResultPagedListCommand.CreateActionResult<FinancierContactListItem>(await _queryService.Query(param), httpContext, _linkGenerator),
                // GetFinancierContact param
                //     => FinancierReadModelCommand.CreateActionResult<FinancierContactDetail>(await _queryService.Query(param), httpContext, _linkGenerator),
                // DoFinancierDependencyCheck param => ActionResultCheckDependencyCommand.CreateActionResult(await _queryService.Query(param)),
                _ => throw new ArgumentOutOfRangeException("Unknown Financier query parameter!", nameof(queryParam))
            };
    }
}