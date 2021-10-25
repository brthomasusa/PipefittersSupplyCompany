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
                GetEmployeesSupervisedBy param =>
                    ActionResultPagedListCommand.CreateActionResult<EmployeeListItem>(await _queryService.Query(param),
                                                                                      httpContext,
                                                                                      _linkGenerator,
                                                                                      PagedListLinkGenerationCommand.Execute<EmployeeListItem>),
                GetEmployeesOfRole param =>
                    ActionResultPagedListCommand.CreateActionResult<EmployeeListItemWithRoles>(await _queryService.Query(param),
                                                                                               httpContext,
                                                                                               _linkGenerator,
                                                                                               PagedListLinkGenerationCommand.Execute<EmployeeListItemWithRoles>),
                GetEmployee param =>
                    ActionResultReadModelCommand.CreateActionResult<EmployeeDetail>(await _queryService.Query(param),
                                                                                    httpContext,
                                                                                    _linkGenerator,
                                                                                    ReadModelLinkGenerationCommand.Execute<EmployeeDetail>),
                GetEmployeeAddresses param =>
                    ActionResultPagedListCommand.CreateActionResult<EmployeeAddressListItem>(await _queryService.Query(param),
                                                                                      httpContext,
                                                                                      _linkGenerator,
                                                                                      PagedListLinkGenerationCommand.Execute<EmployeeAddressListItem>),
                GetEmployeeAddress param =>
                    ActionResultReadModelCommand.CreateActionResult<EmployeeAddressDetail>(await _queryService.Query(param),
                                                                                    httpContext,
                                                                                    _linkGenerator,
                                                                                    ReadModelLinkGenerationCommand.Execute<EmployeeAddressDetail>),
                GetEmployeeContacts param =>
                    ActionResultPagedListCommand.CreateActionResult<EmployeeContactListItem>(await _queryService.Query(param),
                                                                                      httpContext,
                                                                                      _linkGenerator,
                                                                                      PagedListLinkGenerationCommand.Execute<EmployeeContactListItem>),
                GetEmployeeContact param =>
                    ActionResultReadModelCommand.CreateActionResult<EmployeeContactDetail>(await _queryService.Query(param),
                                                                                    httpContext,
                                                                                    _linkGenerator,
                                                                                    ReadModelLinkGenerationCommand.Execute<EmployeeContactDetail>),

                DoEmployeeDependencyCheck param => ActionResultCheckDependencyCommand.CreateActionResult(await _queryService.Query(param)),

                _ => throw new ArgumentOutOfRangeException("Unknown Financier query parameter!", nameof(queryParam))
            };
    }
}