using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using PipefittersSupplyCompany.Core.HumanResources;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
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
        public void ShouldReturnAtLeast_8_Employees()
        {
            List<Employee> employees = _dbContext.Employees.AsNoTracking().ToList();
            int count = employees.Count;

            Assert.True(count >= 8);
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