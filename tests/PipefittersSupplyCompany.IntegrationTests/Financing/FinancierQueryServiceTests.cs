using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using PipefittersSupplyCompany.IntegrationTests.Base;
using PipefittersSupplyCompany.WebApi.Utilities;
using PipefittersSupplyCompany.Infrastructure.Application.Services.Financing;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;

namespace PipefittersSupplyCompany.IntegrationTests.Financing
{
    public class FinancierQueryServiceTests : IntegrationTestBaseDapper
    {
        private readonly FinancierQueryService _financierQrySvc;

        public FinancierQueryServiceTests() => _financierQrySvc = new FinancierQueryService(_dapperCtx);

        [Fact]
        public async Task ShouldGet_FinancierListItems_UsingGetFinanciersQueryParameters()
        {
            var getFinanciers = new GetFinanciers { Page = 1, PageSize = 20 };

            var result = await _financierQrySvc.Query(getFinanciers);

            int resultCount = result.ToList().Count;

            Assert.True(resultCount >= 5);
        }

        [Fact]
        public async Task ShouldGet_FinancierDetails_UsingGetFinancierDetailsQueryParameters()
        {
            var getFinancierDetails =
                new GetFinancier
                {
                    FinancierID = new Guid("94b1d516-a1c3-4df8-ae85-be1f34966601")
                };

            var financierDetails = await _financierQrySvc.Query(getFinancierDetails);

            Assert.Equal("Paul Van Horn Enterprises", financierDetails.FinancierName);
            Assert.Equal("415-328-9870", financierDetails.Telephone);
        }

        [Fact]
        public async Task ShouldGet_FinancierAddressListItems_Using_GetFinancierAddresses_Parameters()
        {
            var getFinancierAddresses = new GetFinancierAddresses
            {
                FinancierID = new Guid("12998229-7ede-4834-825a-0c55bde75695"),
                Page = 1,
                PageSize = 4
            };

            var result = await _financierQrySvc.Query(getFinancierAddresses);

            int resultCount = result.ToList().Count;

            Assert.True(resultCount >= 2);
        }

        [Fact]
        public async Task ShouldGet_FinancierAddressDetail_Using_GetFinancierAddress_Parameters()
        {
            var getFinancierAddress = new GetFinancierAddress
            {
                AddressID = 14
            };

            var result = await _financierQrySvc.Query(getFinancierAddress);

            Assert.Equal(new Guid("b49471a0-5c1e-4a4d-97e7-288fb0f6338a"), result.FinancierId);
        }

        [Fact]
        public async Task ShouldGet_FinancierContactListItems_Using_GetFinancierContacts_Parameters()
        {
            var getFinancierContacts = new GetFinancierContacts
            {
                FinancierID = new Guid("12998229-7ede-4834-825a-0c55bde75695"),
                Page = 1,
                PageSize = 4
            };

            var result = await _financierQrySvc.Query(getFinancierContacts);

            int resultCount = result.ToList().Count;

            Assert.True(resultCount >= 2);
        }

        [Fact]
        public async Task ShouldGet_FinancierContactDetails_UsingGetFinancierContactParameters()
        {
            var getFinancierContact =
                new GetFinancierContact
                {
                    PersonID = 13
                };

            var contactDetails = await _financierQrySvc.Query(getFinancierContact);

            Assert.Equal(new Guid("bf19cf34-f6ba-4fb2-b70e-ab19d3371886"), contactDetails.FinancierId);
        }

        [Fact]
        public async Task Reflection_Playground()
        {
            var getFinanciers = new GetFinanciers { Page = 1, PageSize = 20 };

            var result = await _financierQrySvc.Query(getFinanciers);
            var readModelString = result.GetType().FullName + ", PipefittersSupplyCompany.Infrastructure";
            Type readModel = Type.GetType(readModelString, true, true);

            var genericBase = typeof(ResponseHeaderHandler<>);
            var combinedType = genericBase.MakeGenericType(readModel);
            var responseHeaderHandler = Activator.CreateInstance(combinedType);

            // Assert.True(readModelString.Contains("FinancierListItem"));
            Assert.IsType<PagedList<FinancierListItem>>(result);
        }

    }
}