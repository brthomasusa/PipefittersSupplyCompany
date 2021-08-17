using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using PipefittersSupplyCompany.Core.HumanResources;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.IntegrationTests.Base;

namespace PipefittersSupplyCompany.IntegrationTests.HumanResources
{
    public class EntityConfigurationTests : IntegrationTestBase
    {
        [Fact]
        public void ShouldReturnAtLeast_7_Roles()
        {
            List<Role> roles = _dbContext.Roles.AsNoTracking().ToList();
            int count = roles.Count;

            Assert.True(count >= 7);
        }

        [Fact]
        public void ShouldReturnAtLeast_8_ExternalAgentEmployees()
        {
            List<ExternalAgent> agents = _dbContext.ExternalAgents.AsNoTracking().ToList();
            int count = agents.Count;

            Assert.True(count >= 8);
        }

        [Fact]
        public void ShouldReturnAtLeast_8_Employees()
        {
            List<Employee> employees = _dbContext.Employees
                    .AsNoTracking()
                    .ToList();

            int count = employees.Count;

            Assert.True(count >= 8);
        }

        [Fact]
        public void ShouldReturn_Employee_WithAddresses()
        {
            Employee employee = _dbContext.Employees
                    // .AsNoTracking()  Lazy loading does not work if you use AsNoTracking 
                    .Where(e => e.Id.Equals(new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e")))
                    // .Include(e => e.ExternalAgent).ThenInclude(a => a.Addresses)
                    .FirstOrDefault();

            int count = employee.ExternalAgent.Addresses.Count;

            Assert.True(count >= 1);
        }

        [Fact]
        public void ShouldReturn_Employee_WithContactPeople()
        {
            Employee employee = _dbContext.Employees
                    .Where(e => e.Id.Equals(new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e")))
                    // .Include(e => e.ExternalAgent).ThenInclude(c => c.ContactPersons)
                    .FirstOrDefault();

            int count = employee.ExternalAgent.ContactPersons.Count;

            Assert.True(count >= 1);
        }

        [Fact]
        public void ShouldReturnAtLeast_8_Users()
        {
            List<User> users = _dbContext.Users.AsNoTracking().ToList();
            int count = users.Count;

            Assert.True(count >= 8);
        }

        [Fact]
        public void ShouldReturnAtLeast_16_UserRoles()
        {
            List<UserRole> userRoles = _dbContext.UserRoles.AsNoTracking().ToList();
            int count = userRoles.Count;

            Assert.True(count >= 16);
        }
    }
}