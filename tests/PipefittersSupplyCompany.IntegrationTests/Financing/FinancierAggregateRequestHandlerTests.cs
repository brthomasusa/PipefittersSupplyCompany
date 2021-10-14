using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Xunit;
using PipefittersSupplyCompany.IntegrationTests.Base;
using PipefittersSupplyCompany.WebApi.Utilities;
using PipefittersSupplyCompany.WebApi.Controllers.Financing;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Services.Financing;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;

using static PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing.FinancierQueryParameters;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing.FinancierReadModels;

namespace PipefittersSupplyCompany.IntegrationTests.Financing
{
    public class FinancierAggregateRequestHandlerTests : IntegrationTestBaseDapper
    {
        private readonly ILoggerManager _logger;
        private readonly FinancierAggregateRequestHandler _requestHandler;

        public FinancierAggregateRequestHandlerTests()
        {
            FinancierQueryService financierQrySvc = new FinancierQueryService(_dapperCtx);
            IFinancierQueryHandler qryHdlr = new FinancierQueryHandler(financierQrySvc);
            _requestHandler = new FinancierAggregateRequestHandler(qryHdlr);
            _logger = new PipefittersSupplyCompany.Infrastructure.LoggerManager();
        }

        [Fact]
        public async Task ShouldGet_FinancierListItems_UsingGetFinanciersQueryParam()
        {
            var getFinanciers = new GetFinanciers { Page = 1, PageSize = 20 };

            var actionResult = await _requestHandler.HandleQuery
                        (
                            getFinanciers,
                            _logger,
                            new DefaultHttpContext()
                        );

            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var financierListItems = okObjectResult.Value as List<FinancierListItem>;
            Assert.True(financierListItems.Count >= 5);
        }
    }
}