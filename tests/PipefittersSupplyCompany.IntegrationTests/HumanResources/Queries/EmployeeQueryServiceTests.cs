using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using PipefittersSupplyCompany.IntegrationTests.Base;
using PipefittersSupplyCompany.Infrastructure.Application.Services;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.ReadModels;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.QueryParameters;

namespace PipefittersSupplyCompany.IntegrationTests.HumanResources.Queries
{
    public class EmployeeQueryServiceTests : IntegrationTestBaseDapper
    {
        private readonly EmployeeQueryService _employeeQrySvc;

        public EmployeeQueryServiceTests() => _employeeQrySvc = new EmployeeQueryService(_dapperCtx);

        [Fact]
        public async Task ShouldRetrieve_EmployeeListItems_UsingGetEmployeesQueryParameters()
        {
            var getEmployees = new GetEmployees { Page = 1, PageSize = 20 };

            var result = await _employeeQrySvc.Query(getEmployees);

            int resultCount = result.ToList().Count;

            Assert.True(resultCount > 0);
        }

        [Fact]
        public async Task ShouldGet_EmployeeListItems_UsingGetEmployeesSupervisedByQueryParameters()
        {
            var getEmployeesSupervisedBy = new GetEmployeesSupervisedBy { SupervisorID = new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"), Page = 1, PageSize = 20 };

            var result = await _employeeQrySvc.Query(getEmployeesSupervisedBy);

            int resultCount = result.ToList().Count;

            Assert.True(resultCount >= 2);
        }

    }
}