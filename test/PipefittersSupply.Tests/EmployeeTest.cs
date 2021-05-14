using System;
using PipefittersSupply.Domain.HumanResources;
using Xunit;

namespace PipefittersSupply.Tests
{
    public class EmployeeTest
    {
        [Fact]
        public void ShouldCreateValidEmployee()
        {
            var ee = new Employee(new EmployeeId(Guid.NewGuid()), "Test");

            Assert.NotNull(ee);
            Assert.Equal("Test", ee.LastName);
        }

        [Fact]
        public void ShouldBeEqual()
        {
            Guid eeID = Guid.NewGuid();

            var employee1 = new Employee(new EmployeeId(eeID), "Test");
            var employee2 = new Employee(new EmployeeId(eeID), "Test");

            Assert.Equal(employee1.Id, employee2.Id);
        }
    }
}