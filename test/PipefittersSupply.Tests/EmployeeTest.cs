using System;
using PipefittersSupply.Domain.HumanResources;
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
    }
}