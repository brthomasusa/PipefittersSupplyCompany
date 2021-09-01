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
using PipefittersSupplyCompany.WebApi.Controllers;
using PipefittersSupplyCompany.Infrastructure.Persistence;
using PipefittersSupplyCompany.IntegrationTests.Base;
using static PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources.EmployeeAggregateCommand;

namespace PipefittersSupplyCompany.IntegrationTests.HumanResources.Controllers
{
    public class EmployeesControllerTests : IClassFixture<WebApplicationFactory<PipefittersSupplyCompany.WebApi.Startup>>
    {
        private HttpClient _client;
        private readonly string _serviceAddress = "https://localhost:5001/";
        private readonly string _rootAddress = "api/employees";

        public EmployeesControllerTests(WebApplicationFactory<PipefittersSupplyCompany.WebApi.Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GET_retrieves_weather_forecast()
        {
            var response = await _client.GetAsync("/weatherforecast");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var forecast = JsonConvert.DeserializeObject<WeatherForecast[]>(await response.Content.ReadAsStringAsync());

            Assert.Equal(5, forecast.Count());
        }

        [Fact]
        public async Task ShouldInsert_Employee_UsingEmployeeController()
        {
            Guid id = Guid.NewGuid();
            var command = new V1.CreateEmployeeInfo
            {
                Id = id,
                SupervisorId = id,
                LastName = "Hello",
                FirstName = "World",
                MiddleInitial = "Z",
                SSN = "523789999",
                AddressLine1 = "555 Fifth Street",
                AddressLine2 = "Apt 555",
                City = "Richardson",
                StateCode = "TX",
                Zipcode = "75213",
                Telephone = "214-654-9874",
                MaritalStatus = "S",
                Exemptions = 2,
                PayRate = 25.00M,
                StartDate = new DateTime(2021, 8, 29),
                IsActive = true
            };

            string jsonEmployee = JsonConvert.SerializeObject(command);
            HttpContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{_serviceAddress}{_rootAddress}/", content);

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}