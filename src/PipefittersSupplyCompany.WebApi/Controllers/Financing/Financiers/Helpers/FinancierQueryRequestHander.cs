using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Controllers.Base;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing.Financiers.Helpers
{
    public class FinancierQueryRequestHandler : IFinancierQueryRequestHandler
    {
        private readonly IFinancierQueryService _queryService;
        private readonly LinkGenerator _linkGenerator;

        public FinancierQueryRequestHandler(IFinancierQueryService queryService, LinkGenerator generator)
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
                GetFinanciers param =>
                    ActionResultPagedListCommand.CreateActionResult<FinancierListItem>(await _queryService.Query(param),
                                                                                       httpContext,
                                                                                       _linkGenerator,
                                                                                       PagedListLinkGenerationCommand.Execute<FinancierListItem>),
                GetFinancier param
                    => ActionResultReadModelCommand.CreateActionResult<FinancierDetail>(await _queryService.Query(param),
                                                                                       httpContext,
                                                                                       _linkGenerator,
                                                                                       ReadModelLinkGenerationCommand.Execute<FinancierDetail>),
                GetFinancierAddresses param
                    => ActionResultPagedListCommand.CreateActionResult<FinancierAddressListItem>(await _queryService.Query(param),
                                                                                                 httpContext, _linkGenerator,
                                                                                                 PagedListLinkGenerationCommand.Execute<FinancierAddressListItem>),
                GetFinancierAddress param
                    => ActionResultReadModelCommand.CreateActionResult<FinancierAddressDetail>(await _queryService.Query(param),
                                                                                               httpContext,
                                                                                               _linkGenerator,
                                                                                               ReadModelLinkGenerationCommand.Execute<FinancierAddressDetail>),
                GetFinancierContacts param
                    => ActionResultPagedListCommand.CreateActionResult<FinancierContactListItem>(await _queryService.Query(param),
                                                                                                 httpContext,
                                                                                                 _linkGenerator,
                                                                                                 PagedListLinkGenerationCommand.Execute<FinancierContactListItem>),
                GetFinancierContact param
                    => ActionResultReadModelCommand.CreateActionResult<FinancierContactDetail>(await _queryService.Query(param),
                                                                                               httpContext,
                                                                                               _linkGenerator,
                                                                                               ReadModelLinkGenerationCommand.Execute<FinancierContactDetail>),

                DoFinancierDependencyCheck param => ActionResultCheckDependencyCommand.CreateActionResult(await _queryService.Query(param)),
                _ => throw new ArgumentOutOfRangeException("Unknown Financier query parameter!", nameof(queryParam))
            };
    }
}