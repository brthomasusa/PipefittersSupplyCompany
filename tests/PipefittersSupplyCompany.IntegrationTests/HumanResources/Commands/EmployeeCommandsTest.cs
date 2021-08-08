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
    public class EmployeeCommandsTest : IntegrationTestBase
    {

        [Fact]
        public void ShouldInsert_Employee()
        {
            Guid employeeID = Guid.NewGuid();

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

            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();

            var result = _dbContext.Employees.Find(employeeID);

            Assert.IsType<Employee>(result);
        }

        [Fact]
        public void ShouldUpdate_Employee()
        {
            Guid employeeID = new Guid("4B900A74-E2D9-4837-B9A4-9E828752716E");

        }
    }
}