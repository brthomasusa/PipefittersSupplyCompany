using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.Data.SqlClient;
using PipefittersSupply.Infrastructure.Application.Services;
using static PipefittersSupply.Infrastructure.Queries.HumanResources.ReadModels;
using static PipefittersSupply.Infrastructure.Queries.HumanResources.QueryParameters;
using Xunit;

namespace PipefittersSupply.Tests.IntegrationTests.HumanResources
{
    public class EmployeeQueryServiceTest : IntegrationTestBase
    {
        public EmployeeQueryServiceTest() : base() { }

        [Fact]
        public async void ShouldReturn_EmployeeListItems_ReadModel()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var querySvc = new EmployeeQueryService(connection);

                var results = await querySvc.Query(new GetEmployees { Page = 1, PageSize = 10 });
                var count = results.Count();

                Assert.IsType<List<EmployeeListItems>>(results);
                Assert.Equal(8, count);
            }
        }

        [Fact]
        public async void ShouldReturn_EmployeeListItems_ReadModel_SupervisedBy()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var querySvc = new EmployeeQueryService(connection);

                var results = await querySvc.Query(new GetEmployeesSupervisedBy { SupervisorID = 1, Page = 1, PageSize = 10 });
                var count = results.Count();

                Assert.IsType<List<EmployeeListItems>>(results);
                Assert.Equal(6, count);
            }
        }

        [Fact]
        public async void ShouldReturn_EmployeeListItems_ReadModel_OfEmployeeType()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var querySvc = new EmployeeQueryService(connection);

                var results = await querySvc.Query(new GetEmployeesOfEmployeeType { EmployeeTypeID = 1, Page = 1, PageSize = 10 });
                var count = results.Count();

                Assert.IsType<List<EmployeeListItems>>(results);
                Assert.Equal(2, count);
            }
        }

        [Fact]
        public async void ShouldReturn_EmployeeDetails_ReadModel()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var querySvc = new EmployeeQueryService(connection);

                var results = await querySvc.Query(new GetEmployee { EmployeeID = 2 });

                Assert.IsType<EmployeeDetails>(results);
                Assert.Equal(2, results.EmployeeId);
                Assert.Equal("Phide", results.LastName);
                Assert.Equal("Terri", results.FirstName);
            }
        }
    }
}