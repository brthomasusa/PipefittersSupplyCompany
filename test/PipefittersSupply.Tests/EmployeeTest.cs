using System;
using PipefittersSupply.Domain.HumanResources.Employee;
using Xunit;

namespace PipefittersSupply.Tests
{
    public class EmployeeTest
    {
        [Fact]
        public void ShouldBeEqual()
        {
            var employee1 = new Employee(new EmployeeId(1));
            var employee2 = new Employee(new EmployeeId(1));

            Assert.Equal(employee1.Id, employee2.Id);
        }

        [Fact]
        public void ShouldReturnEmployeeLastNameAsValueObject()
        {
            var lastName = EmployeeLastName.FromString("Santana");
            Assert.NotNull(lastName);
        }

        [Fact]
        public void ShouldRaiseError_EmptyLastNameParameter()
        {
            Action action = () => EmployeeLastName.FromString("");

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Equal("The employee last name is required.", caughtException.ParamName);
        }

        [Fact]
        public void ShouldRaiseError_LastNameParameterLengthTooLong()
        {
            Action action = () => EmployeeLastName.FromString("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");

            var caughtException = Assert.Throws<ArgumentOutOfRangeException>(action);

            Assert.Equal("Last name can not be longer than 25 characters.", caughtException.ParamName);
        }

        [Fact]
        public void ShouldRaiseError_EmployeeTypeId_EqualsZero()
        {
            Action action = () => EmployeeTypeIdentifier.FromInterger(0);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Employee type must be specified.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_EmployeeTypeId_NegativeNumberNotValid()
        {
            Action action = () => EmployeeTypeIdentifier.FromInterger(-1);

            var caughtException = Assert.Throws<ArgumentException>(action);

            // Assert.True(caughtException.Message.Contains("Invalid employee type; employee type can not be negative."));
            Assert.Contains("Invalid employee type; employee type can not be negative.", caughtException.Message);
        }
    }
}