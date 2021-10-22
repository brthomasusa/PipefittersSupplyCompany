using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Xunit;
using PipefittersSupplyCompany.IntegrationTests.Base;
using PipefittersSupplyCompany.WebApi.Controllers;
using PipefittersSupplyCompany.Infrastructure;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Interfaces.HumanResources;
using PipefittersSupplyCompany.Infrastructure.Application.Services.HumanResources;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.EmployeeReadModels;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.EmployeeQueryParameters;

namespace PipefittersSupplyCompany.IntegrationTests.HumanResources.Queries
{
    public class RequestHandlerHandleQueryTests : IntegrationTestBaseDapper
    {
        private readonly IEmployeeQueryService _employeeQrySvc;
        private readonly ILoggerManager _logger;

        public RequestHandlerHandleQueryTests()
        {
            _employeeQrySvc = new EmployeeQueryService(_dapperCtx);
            _logger = new LoggerManager();
        }

        // [Fact]
        // public async Task GetEmployees_ShouldReturn_OkObjectResult_Containing_EmployeeListItems()
        // {
        //     GetEmployees queryParams =
        //         new GetEmployees
        //         {
        //             Page = 1,
        //             PageSize = 4
        //         };

        //     var actionResult = await EmployeeAggregateRequestHandler.HandleQuery
        //                 (
        //                     () => _employeeQrySvc.Query(queryParams),
        //                     _logger,
        //                     new DefaultHttpContext()
        //                 );

        //     var okObjectResult = (OkObjectResult)actionResult;
        //     Assert.NotNull(okObjectResult);

        //     var employeeListItems = okObjectResult.Value as List<EmployeeListItems>;
        //     Assert.True(employeeListItems.Count > 0);
        // }

        // [Fact]
        // public async Task GetSupervisedBy_ShouldReturn_OkObjectResult_Containing_EmployeeListItems()
        // {
        //     GetEmployeesSupervisedBy queryParams =
        //         new GetEmployeesSupervisedBy
        //         {
        //             SupervisorID = new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"),
        //             Page = 1,
        //             PageSize = 4
        //         };

        //     var actionResult = await EmployeeAggregateRequestHandler.HandleQuery
        //     (
        //         () => _employeeQrySvc.Query(queryParams),
        //         _logger,
        //         new DefaultHttpContext()
        //     );

        //     Assert.IsType<OkObjectResult>(actionResult);
        //     var okObjectResult = (OkObjectResult)actionResult;

        //     var employeeListItems = okObjectResult.Value as List<EmployeeListItems>;
        //     Assert.True(employeeListItems.Count > 0);
        // }

        // [Fact]
        // public async Task GetSupervisedBy_ShouldReturn_NotFoundObjectResult_InvalidSupervisorID()
        // {
        //     GetEmployeesSupervisedBy queryParams =
        //         new GetEmployeesSupervisedBy
        //         {
        //             SupervisorID = new Guid("12345a74-e2d9-4837-b9a4-9e828752716e"),
        //             Page = 1,
        //             PageSize = 4
        //         };

        //     var actionResult = await EmployeeAggregateRequestHandler.HandleQuery
        //     (
        //         () => _employeeQrySvc.Query(queryParams),
        //         _logger,
        //         new DefaultHttpContext()
        //     );

        //     Assert.IsType<NotFoundObjectResult>(actionResult);
        //     var message = Newtonsoft.Json.JsonConvert.SerializeObject((actionResult as NotFoundObjectResult).Value);
        //     Assert.Contains("12345a74-e2d9-4837-b9a4-9e828752716e is not a valid supervisor Id.", message);
        // }

        // [Fact]
        // public async Task GetEmployeesOfRole_ShouldReturn_OkObjectResult_Containing_EmployeeListItems()
        // {
        //     GetEmployeesOfRole queryParams =
        //         new GetEmployeesOfRole
        //         {
        //             RoleID = new Guid("cad456c3-a6c8-4e7a-8be5-9aa0aedb8ec1"),
        //             Page = 1,
        //             PageSize = 4
        //         };

        //     var actionResult = await EmployeeAggregateRequestHandler.HandleQuery
        //     (
        //         () => _employeeQrySvc.Query(queryParams),
        //         _logger,
        //         new DefaultHttpContext()
        //     );

        //     Assert.IsType<OkObjectResult>(actionResult);
        //     var okObjectResult = (OkObjectResult)actionResult;

        //     var employeeListItems = okObjectResult.Value as List<EmployeeListItemsWithRoles>;
        //     Assert.True(employeeListItems.Count > 0);
        // }

        // [Fact]
        // public async Task GetEmployeesOfRole_ShouldReturn_NotFoundObjectResult_InvalidRoleID()
        // {
        //     GetEmployeesOfRole queryParams =
        //         new GetEmployeesOfRole
        //         {
        //             RoleID = new Guid("123456c3-a6c8-4e7a-8be5-9aa0aedb8ec1"),
        //             Page = 1,
        //             PageSize = 4
        //         };

        //     var actionResult = await EmployeeAggregateRequestHandler.HandleQuery
        //     (
        //         () => _employeeQrySvc.Query(queryParams),
        //         _logger,
        //         new DefaultHttpContext()
        //     );

        //     Assert.IsType<NotFoundObjectResult>(actionResult);
        //     var message = Newtonsoft.Json.JsonConvert.SerializeObject((actionResult as NotFoundObjectResult).Value);
        //     Assert.Contains("123456c3-a6c8-4e7a-8be5-9aa0aedb8ec1 is not a valid role Id.", message);
        // }
    }
}