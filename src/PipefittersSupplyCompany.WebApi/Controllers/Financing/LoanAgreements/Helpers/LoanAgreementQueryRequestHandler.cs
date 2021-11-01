using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Controllers.Base;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;

namespace PipefittersSupplyCompany.WebApi.Controllers.Financing.LoanAgreements.Helpers
{
    public class LoanAgreementQueryRequestHandler
    {

        private readonly IFinancierQueryService _queryService;
        private readonly LinkGenerator _linkGenerator;

        public LoanAgreementQueryRequestHandler(IFinancierQueryService queryService, LinkGenerator generator)
        {
            _queryService = queryService;
            _linkGenerator = generator;
        }
    }
}