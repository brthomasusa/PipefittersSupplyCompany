using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using Xunit;
using PipefittersSupplyCompany.IntegrationTests.Base;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Utilities;
using PipefittersSupplyCompany.WebApi.Controllers.Financing;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Services.Financing;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;

namespace PipefittersSupplyCompany.IntegrationTests.Financing
{
    public class FinancierQueryRequestHandlerTests : IntegrationTestBaseDapper
    {
        private readonly FinancierQueryRequestHander _queryRequestHandler;

        public FinancierQueryRequestHandlerTests()
        {
            FinancierQueryService financierQrySvc = new FinancierQueryService(_dapperCtx);
            _queryRequestHandler = new FinancierQueryRequestHander(financierQrySvc);
        }

        [Fact]
        public async Task ShouldGet_FinancierListItems_FinancierQueryRequestHander()
        {
            var getFinanciers = new GetFinanciers { Page = 1, PageSize = 20 };

            var actionResult = await _queryRequestHandler.Handle<GetFinanciers>
                        (
                            getFinanciers,
                            new DefaultHttpContext()
                        );

            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var financierListItems = okObjectResult.Value as List<FinancierListItem>;
            Assert.True(financierListItems.Count >= 5);
        }

        [Fact]
        public async Task ShouldGet_FinancierDetail_FinancierQueryRequestHander()
        {
            var getFinancierDetails =
                new GetFinancier
                {
                    FinancierID = new Guid("94b1d516-a1c3-4df8-ae85-be1f34966601")
                };

            var actionResult = await _queryRequestHandler.Handle<GetFinancier>
                        (
                            getFinancierDetails,
                            new DefaultHttpContext()
                        );

            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var financierDetails = okObjectResult.Value as FinancierDetail;
            Assert.Equal("Paul Van Horn Enterprises", financierDetails.FinancierName);
        }
    }
}