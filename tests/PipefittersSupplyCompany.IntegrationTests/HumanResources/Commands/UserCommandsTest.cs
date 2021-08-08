using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using PipefittersSupplyCompany.Core.HumanResources;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.IntegrationTests.Base;

namespace PipefittersSupplyCompany.IntegrationTests.HumanResources.Commands
{
    public class UserCommandsTests : IntegrationTestBase
    {
        [Fact]
        public void ShouldInsert_User()
        {
            Guid employeeID = Guid.NewGuid();
            Guid userID = Guid.NewGuid();

            Employee employee = new Employee
            (
                employeeID,
                new Guid("4B900A74-E2D9-4837-B9A4-9E828752716E"),
                "Doe",
                "John",
                "J",
                "321 Main Street",
                "#2",
                "Fort Worth",
                "TX",
                "78965",
                "223789999",
                "817-987-1234",
                "M",
                5,
                40.00M,
                new DateTime(2021, 8, 8),
                true
            );

            User user = new User
            (
                userID,
                "john.j.doe@pipefitterssupplycompany.com",
                "john.j.doe@pipefitterssupplycompany.com",
                employee
            );

            _dbContext.Employees.Add(employee);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            var result = _dbContext.Users.Find(userID);

            Assert.IsType<User>(result);
        }
    }
}