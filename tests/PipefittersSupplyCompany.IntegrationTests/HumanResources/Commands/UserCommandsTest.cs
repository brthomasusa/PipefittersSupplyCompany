using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using PipefittersSupplyCompany.Core.HumanResources;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.IntegrationTests.Base;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.IntegrationTests.HumanResources.Commands
{
    public class UserCommandsTests : IntegrationTestBase
    {
        public UserCommandsTests() => TestDataInitialization.InitializeData(_dbContext);

        [Fact]
        public void ShouldInsert_User()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);
            Guid userID = Guid.NewGuid();

            Employee employee = new Employee
            (
                employeeAgent,
                SupervisorId.Create(new Guid("4B900A74-E2D9-4837-B9A4-9E828752716E")),
                PersonName.Create("George", "Orwell", "J"),
                SSN.Create("523789999"),
                PhoneNumber.Create("817-987-1234"),
                MaritalStatus.Create("M"),
                TaxExemption.Create(5),
                PayRate.Create(40.00M),
                StartDate.Create(new DateTime(1998, 12, 2)),
                IsActive.Create(true)
            );

            User user = new User
            (
                userID,
                "george.m.orwell@pipefitterssupplycompany.com",
                "george.m.orwell@pipefitterssupplycompany.com",
                employee
            );

            _dbContext.ExternalAgents.Add(employeeAgent);
            _dbContext.Employees.Add(employee);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            var result = _dbContext.Users.Find(userID);

            Assert.IsType<User>(result);
        }
    }
}