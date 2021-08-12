using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using PipefittersSupplyCompany.Core.HumanResources;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.IntegrationTests.Base;
using PipefittersSupplyCompany.Core.Shared;

namespace PipefittersSupplyCompany.IntegrationTests.HumanResources.Commands
{
    public class EmployeeCommandsTest : IntegrationTestBase
    {

        [Fact]
        public void ShouldInsert_ExternalAgentAndEmployee()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Employee employee = new Employee
            (
                employeeAgent,
                employeeAgent.Id,
                "DeSantis",
                "Ron",
                "J",
                "321 Tarrant Pl",
                null,
                "Fort Worth",
                "TX",
                "78965",
                "223789999",
                "817-987-1234",
                "M",
                5,
                7.50M,
                new DateTime(2021, 8, 11),
                true
            );

            _dbContext.ExternalAgents.Add(employeeAgent);
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();

            var employeeResult = _dbContext.Employees.Find(employeeAgent.Id);
            var agentResult = _dbContext.ExternalAgents.Find(employeeAgent.Id);

            Assert.IsType<ExternalAgent>(agentResult);
            Assert.IsType<Employee>(employeeResult);

            // Assert.Null(agentResult.Employee); It is doing eager loading!
        }

        [Fact]
        public void ShouldRetrieve_ExternalAgentAndIncludeEmployee()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Employee employee = new Employee
            (
                employeeAgent,
                employeeAgent.Id,
                "DeSantis",
                "Ron",
                "J",
                "321 Tarrant Pl",
                null,
                "Fort Worth",
                "TX",
                "78965",
                "223789999",
                "817-987-1234",
                "M",
                5,
                7.50M,
                new DateTime(2021, 8, 11),
                true
            );

            _dbContext.ExternalAgents.Add(employeeAgent);
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();

            var result = _dbContext.ExternalAgents
                        .Where(a => a.Id.Equals(employeeAgent.Id))
                        .Include(e => e.Employee)
                        .FirstOrDefault();

            Assert.NotNull(result);
            Assert.Equal("DeSantis", result.Employee.LastName);
        }
    }
}