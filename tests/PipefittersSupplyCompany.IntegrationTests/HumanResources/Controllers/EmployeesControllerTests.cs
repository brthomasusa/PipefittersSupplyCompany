using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using PipefittersSupplyCompany.WebApi;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.IntegrationTests.Base;
using static PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources.EmployeeAggregateWriteModels;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.ReadModels;
using PipefittersSupplyCompany.WebApi.Utilities;

namespace PipefittersSupplyCompany.IntegrationTests.HumanResources.Controllers
{
    public class EmployeesControllerTests : IntegrationTestBaseEfCore, IClassFixture<WebApplicationFactory<PipefittersSupplyCompany.WebApi.Startup>>
    {
        private HttpClient _client;
        private readonly string _serviceAddress = "https://localhost:5001/";
        private readonly string _rootAddress = "api/1.0/employees";

        public EmployeesControllerTests(WebApplicationFactory<PipefittersSupplyCompany.WebApi.Startup> factory)
        {
            _client = factory.CreateClient();
            TestDataInitialization.InitializeData(_dbContext);
        }

        [Fact]
        public async Task GET_PagedList_All_EmployeeListItems()
        {
            var pagingParams = new PagingParameters { Page = 1, PageSize = 10 };

            var response = await _client.GetAsync($"{_serviceAddress}{_rootAddress}/list?Page={pagingParams.Page}&PageSize={pagingParams.PageSize}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var employeeListItems = JsonConvert.DeserializeObject<List<EmployeeListItem>>(jsonResponse);
            Assert.True(employeeListItems.Count >= 8);
        }

        [Fact]
        public async Task GET_PagedList_Of_EmployeeListItems_SupervisedBy()
        {
            var pagingParams = new PagingParameters { Page = 1, PageSize = 10 };
            Guid supervisorId = new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e");
            var response = await _client.GetAsync($"{_serviceAddress}{_rootAddress}/supervisedby/{supervisorId}?Page={pagingParams.Page}&PageSize={pagingParams.PageSize}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var employeeListItems = JsonConvert.DeserializeObject<List<EmployeeListItem>>(jsonResponse);
            Assert.True(employeeListItems.Count >= 2);
        }

        [Fact]
        public async Task GET_PagedList_Of_EmployeeListItems_InRole()
        {
            var pagingParams = new PagingParameters { Page = 1, PageSize = 10 };
            Guid roleId = new Guid("cad456c3-a6c8-4e7a-8be5-9aa0aedb8ec1");
            var response = await _client.GetAsync($"{_serviceAddress}{_rootAddress}/employeesofrole/{roleId}?Page={pagingParams.Page}&PageSize={pagingParams.PageSize}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var employeeListItems = JsonConvert.DeserializeObject<List<EmployeeListItem>>(jsonResponse);
            Assert.True(employeeListItems.Count >= 2);
        }

        [Fact]
        public async Task GET_EmployeeDetails_ForOneEmployee()
        {
            Guid employeeId = new Guid("0cf9de54-c2ca-417e-827c-a5b87be2d788");
            var response = await _client.GetAsync($"{_serviceAddress}{_rootAddress}/details/{employeeId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var employeeDetails = JsonConvert.DeserializeObject<EmployeeDetail>(jsonResponse);

            Assert.Equal("Jamie", employeeDetails.FirstName);
            Assert.Equal("Brown", employeeDetails.LastName);
        }



        [Fact]
        public async Task ShouldCreate_EmployeeInfo_UsingEmployeeController()
        {
            Guid id = Guid.NewGuid();
            var command = new CreateEmployeeInfo
            {
                Id = id,
                SupervisorId = id,
                LastName = "Hello",
                FirstName = "World",
                MiddleInitial = "Z",
                SSN = "523789999",
                Telephone = "214-654-9874",
                MaritalStatus = "S",
                Exemptions = 2,
                PayRate = 25.00M,
                StartDate = new DateTime(2021, 8, 29),
                IsActive = true
            };

            string jsonEmployee = JsonConvert.SerializeObject(command);
            HttpContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_serviceAddress}{_rootAddress}/createemployeeinfo", content);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task ShouldUpdate_EmployeeInfo_UsingEmployeeController()
        {
            var command = new EditEmployeeInfo
            {
                Id = new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"),
                SupervisorId = new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"),
                LastName = "Hello",
                FirstName = "World",
                MiddleInitial = "Z",
                SSN = "523019999",
                Telephone = "214-654-9874",
                MaritalStatus = "S",
                Exemptions = 2,
                PayRate = 25.00M,
                StartDate = new DateTime(2021, 8, 29),
                IsActive = true
            };

            string jsonEmployee = JsonConvert.SerializeObject(command);
            HttpContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{_serviceAddress}{_rootAddress}/editemployeeinfo/{command.Id}", content);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task ShouldDelete_EmployeeInfo_UsingEmployeeController()
        {
            var employeeId = new Guid("e6b86ea3-6479-48a2-b8d4-54bd6cbbdbc5");
            var response = await _client.GetAsync($"{serviceAddress}{_rootAddress}/details/{employeeId}");
            Assert.True(response.IsSuccessStatusCode);

            response = await _client.DeleteAsync($"{serviceAddress}{_rootAddress}/deleteemployeeinfo/{employeeId}");

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}