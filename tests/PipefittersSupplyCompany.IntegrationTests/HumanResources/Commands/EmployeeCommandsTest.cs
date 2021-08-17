using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.IntegrationTests.Base;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

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
                SupervisorId.Create(employeeAgent.Id),
                PersonName.Create("George", "Orwell", "J"),
                SSN.Create("323789999"),
                PhoneNumber.Create("817-987-1234"),
                MaritalStatus.Create("M"),
                TaxExemption.Create(5),
                PayRate.Create(40.00M),
                StartDate.Create(new DateTime(1998, 12, 2)),
                IsActive.Create(true)
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
                SupervisorId.Create(employeeAgent.Id),
                PersonName.Create("George", "Orwell", "J"),
                SSN.Create("523789999"),
                PhoneNumber.Create("817-987-1234"),
                MaritalStatus.Create("M"),
                TaxExemption.Create(5),
                PayRate.Create(40.00M),
                StartDate.Create(new DateTime(1998, 12, 2)),
                IsActive.Create(true)
            );

            _dbContext.ExternalAgents.Add(employeeAgent);
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();

            var result = _dbContext.ExternalAgents
                        .Where(a => a.Id.Equals(employeeAgent.Id))
                        .Include(e => e.Employee)
                        .FirstOrDefault();

            Assert.NotNull(result);
            Assert.Equal("Orwell", result.Employee.EmployeeName.LastName);
        }
    }
}