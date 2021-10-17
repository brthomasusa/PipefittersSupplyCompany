using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using PipefittersSupplyCompany.IntegrationTests.Base;
using PipefittersSupplyCompany.Infrastructure.Application.Services.HumanResources;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.EmployeeQueryParameters;

namespace PipefittersSupplyCompany.IntegrationTests.HumanResources.Queries
{
    public class EmployeeQueryServiceTests : IntegrationTestBaseDapper
    {
        private readonly EmployeeQueryService _employeeQrySvc;

        public EmployeeQueryServiceTests() => _employeeQrySvc = new EmployeeQueryService(_dapperCtx);

        [Fact]
        public async Task ShouldGet_EmployeeListItems_UsingGetEmployeesQueryParameters()
        {
            var getEmployees = new GetEmployees { Page = 1, PageSize = 20 };

            var result = await _employeeQrySvc.Query(getEmployees);

            int resultCount = result.ReadModels.ToList().Count;

            Assert.True(resultCount > 0);
        }

        [Fact]
        public async Task ShouldGet_EmployeeListItems_UsingGetEmployeesSupervisedByQueryParameters()
        {
            var getEmployeesSupervisedBy =
                new GetEmployeesSupervisedBy
                {
                    SupervisorID = new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"),
                    Page = 1,
                    PageSize = 20
                };

            var result = await _employeeQrySvc.Query(getEmployeesSupervisedBy);

            int resultCount = result.ReadModels.ToList().Count;

            Assert.True(resultCount >= 2);
        }

        [Fact]
        public async Task ShouldRaiseError_UsingGetEmployeesSupervisedByWithInvalidQueryParameters()
        {
            var getEmployeesSupervisedBy =
                new GetEmployeesSupervisedBy
                {
                    SupervisorID = new Guid("54321a74-e2d9-4837-b9a4-9e828752716e"),
                    Page = 1,
                    PageSize = 20
                };

            await Assert.ThrowsAsync<ArgumentException>(async () => await _employeeQrySvc.Query(getEmployeesSupervisedBy));
        }

        [Fact]
        public async Task ShouldGet_EmployeeListItems_UsingGetEmployeesOfRoleQueryParameters()
        {
            var getEmployeesOfRole =
                new GetEmployeesOfRole
                {
                    RoleID = new Guid("cad456c3-a6c8-4e7a-8be5-9aa0aedb8ec1"),
                    Page = 1,
                    PageSize = 20
                };

            var result = await _employeeQrySvc.Query(getEmployeesOfRole);

            int resultCount = result.ReadModels.ToList().Count;

            Assert.True(resultCount >= 2);
        }

        [Fact]
        public async Task ShouldRaiseError_UsingGetEmployeesOfRoleWithInvalidQueryParameters()
        {
            var getEmployeesOfRole =
                new GetEmployeesOfRole
                {
                    RoleID = new Guid("666456c3-a6c8-4e7a-8be5-9aa0aedb8ec1"),
                    Page = 1,
                    PageSize = 20
                };

            await Assert.ThrowsAsync<ArgumentException>(async () => await _employeeQrySvc.Query(getEmployeesOfRole));
        }

        [Fact]
        public async Task ShouldGet_EmployeeDetails_UsingGetEmployeeDetailsQueryParameters()
        {
            var getEmployeesDetails =
                new GetEmployee
                {
                    EmployeeID = new Guid("AEDC617C-D035-4213-B55A-DAE5CDFCA366")
                };

            var employeeDetails = await _employeeQrySvc.Query(getEmployeesDetails);

            Assert.Equal("Jozef", employeeDetails.FirstName);
            Assert.Equal("Goldberg", employeeDetails.LastName);
        }

        [Fact]
        public async Task ShouldRaiseError_UsingGetEmployeeDetailsWithInvalidQueryParameters()
        {
            var getEmployeesDetails =
                new GetEmployee
                {
                    EmployeeID = new Guid("1234517C-D035-4213-B55A-DAE5CDFCA366")
                };

            await Assert.ThrowsAsync<ArgumentException>(async () => await _employeeQrySvc.Query(getEmployeesDetails));
        }

        [Fact]
        public async Task ShouldRaiseError_UsingGetEmployeeDetailsWithDefaultGuidQueryParameters()
        {
            var getEmployeesDetails =
                new GetEmployee
                {
                    EmployeeID = Guid.NewGuid()
                };

            await Assert.ThrowsAsync<ArgumentException>(async () => await _employeeQrySvc.Query(getEmployeesDetails));
        }

        [Fact]
        public async Task ShouldGet_EmployeeAddressListItems_Using_GetEmployeeAddresses_Parameters()
        {
            var getEmployeeAddresses = new GetEmployeeAddresses
            {
                EmployeeID = new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"),
                Page = 1,
                PageSize = 4
            };

            var result = await _employeeQrySvc.Query(getEmployeeAddresses);

            int resultCount = result.ReadModels.ToList().Count;

            Assert.True(resultCount > 0);
        }




    }
}