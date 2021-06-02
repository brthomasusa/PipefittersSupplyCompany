using System;
using PipefittersSupply.Domain.Common;
using PipefittersSupply.Domain.HumanResources.Employees;
using Xunit;

namespace PipefittersSupply.Tests
{
    public class EmployeeTypeTest
    {
        [Fact]
        public void ShouldReturnValid_EmployeeType_UsingCtor()
        {
            var empTypeID = EmployeeTypeIdentifier.FromInterger(7);
            var empTypeName = EmployeeTypeName.FromString("Human Resources");
            var employeeType = new EmployeeType(empTypeID, empTypeName);

            Assert.IsType<EmployeeType>(employeeType);
            Assert.Equal(7, employeeType.Id);
            Assert.Equal("Human Resources", employeeType.EmployeeTypeName);
        }

        [Fact]
        public void ShouldRaiseError_EmployeeTypeName_TooLong()
        {
            Action action = () => EmployeeTypeName.FromString("Human Resources Human Resources Human Resources Human Resources");

            var caughtException = Assert.Throws<ArgumentOutOfRangeException>(action);

            Assert.Contains("Employee type name can not be longer than 25 characters.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_EmployeeTypeName_EmptyString()
        {
            Action action = () => EmployeeTypeName.FromString("");

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The employee type name is required.", caughtException.Message);
        }
    }
}