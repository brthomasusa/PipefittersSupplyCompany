using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Testing;
// using Microsoft.Net.Http.Headers;
using Xunit;
using PipefittersSupplyCompany.WebApi;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.IntegrationTests.Base;
using PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources;
using PipefittersSupplyCompany.WebApi.Utilities;

namespace PipefittersSupplyCompany.IntegrationTests.HumanResources.Controllers
{
    public class EmployeesControllerTests : IntegrationTestBaseEfCore, IClassFixture<WebApplicationFactory<PipefittersSupplyCompany.WebApi.Startup>>
    {
        private HttpClient _client;
        private readonly string _serviceAddress = "https://localhost:5011/";
        private readonly string _rootAddress = "api/1.0/employees";

        public EmployeesControllerTests(WebApplicationFactory<PipefittersSupplyCompany.WebApi.Startup> factory)
        {
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Add("Accept", "*/*");
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
            HttpContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/vnd.btechnical-consulting.hateoas+json");
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

        [Theory]
        [InlineData("4b900a74-e2d9-4837-b9a4-9e828752716e", 2)]
        [InlineData("5c60f693-bef5-e011-a485-80ee7300c695", 1)]
        [InlineData("660bb318-649e-470d-9d2b-693bfb0b2744", 1)]
        [InlineData("9f7b902d-566c-4db6-b07b-716dd4e04340", 1)]
        [InlineData("aedc617c-d035-4213-b55a-dae5cdfca366", 1)]
        [InlineData("0cf9de54-c2ca-417e-827c-a5b87be2d788", 1)]
        [InlineData("e716ac28-e354-4d8d-94e4-ec51f08b1af8", 1)]
        [InlineData("604536a1-e734-49c4-96b3-9dfef7417f9a", 1)]
        public async Task GET_PagedList_All_AddressesForOneEmployee(Guid employeeId, int numberOfAddresses)
        {
            var pagingParams = new PagingParameters { Page = 1, PageSize = 10 };

            var response = await _client.GetAsync($"{_serviceAddress}{_rootAddress}/{employeeId}/addresses?Page={pagingParams.Page}&PageSize={pagingParams.PageSize}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var employeeListItems = JsonConvert.DeserializeObject<List<EmployeeAddressListItem>>(jsonResponse);

            Assert.Equal(numberOfAddresses, employeeListItems.Count);
        }

        [Fact]
        public async Task ShouldCreate_EmployeeAddressInfo_UsingEmployeeController()
        {
            Guid employeeId = new Guid("604536a1-e734-49c4-96b3-9dfef7417f9a");
            var command = new CreateEmployeeAddressInfo
            {
                EmployeeId = employeeId,
                AddressLine1 = "5558 Reiger Street",
                AddressLine2 = "3rd Floor",
                City = "Dallas",
                StateCode = "TX",
                Zipcode = "75214"
            };

            string jsonEmployee = JsonConvert.SerializeObject(command);
            HttpContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_serviceAddress}{_rootAddress}/{employeeId}/createemployeeaddressinfo", content);

            Assert.True(response.IsSuccessStatusCode);
        }










    }
}