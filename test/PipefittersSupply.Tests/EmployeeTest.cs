using System;
using PipefittersSupply.Domain.Common;
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
        public void ShouldReturnTrue_LastNamesValueObjects_AreEqual()
        {
            var lastName1 = EmployeeFirstName.FromString("Testing");
            var lastName2 = EmployeeFirstName.FromString("Testing");

            Assert.Equal(lastName1, lastName2);
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

        [Fact]
        public void ShouldRaiseError_MiddleInitial_TooSmall()
        {
            Action action = () => EmployeeMiddleInitial.FromString("");

            var caughtException = Assert.Throws<ArgumentOutOfRangeException>(action);

            Assert.Contains("Middle initial is limited to 1 character.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_MiddleInitial_TooLarge()
        {
            Action action = () => EmployeeMiddleInitial.FromString("aa");

            var caughtException = Assert.Throws<ArgumentOutOfRangeException>(action);

            Assert.Contains("Middle initial is limited to 1 character.", caughtException.Message);
        }

        [Fact]
        public void ShouldReturnValid_SSN()
        {
            var ssn = EmployeeSSN.FromString("123456789");

            Assert.IsType<EmployeeSSN>(ssn);
        }

        [Fact]
        public void ShouldRaiseError_SSN_BadCharacters()
        {
            Action action = () => EmployeeSSN.FromString("q23457890");

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Invalid social security number!", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_SSN_BeginWith9()
        {
            Action action = () => EmployeeSSN.FromString("923457890");

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Invalid social security number!", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_SSN_EmptyString()
        {
            Action action = () => EmployeeSSN.FromString("");

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Invalid social security number!", caughtException.Message);
        }

        [Fact]
        public void ShouldAccept_Valid_US_Zipcode_5digits()
        {
            var zipcode = Zipcode.FromString("12345");

            Assert.IsType<Zipcode>(zipcode);
        }

        [Fact]
        public void ShouldAccept_Valid_US_Zipcode_9digits()
        {
            var zipcode = Zipcode.FromString("12345-1234");

            Assert.IsType<Zipcode>(zipcode);
        }

        [Fact]
        public void ShouldAccept_Valid_Canadian_Zipcode_Space()
        {
            var zipcode = Zipcode.FromString("K1A 0B1");

            Assert.IsType<Zipcode>(zipcode);
        }

        [Fact]
        public void ShouldAccept_Valid_Canadian_Zipcode_NoSpace()
        {
            var zipcode = Zipcode.FromString("K1A0B1");

            Assert.IsType<Zipcode>(zipcode);
        }

        [Fact]
        public void ShouldRaiseError_Zipcode_InvalidUsCanada()
        {
            Action action = () => Zipcode.FromString("1222");

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Invalid zip code!", caughtException.Message);
        }

        [Fact]
        public void ShouldReturnValid_StateProvinceCode()
        {
            var stateCode = StateProvinceCode.FromString("Tx");

            Assert.IsType<StateProvinceCode>(stateCode);
        }

        [Fact]
        public void ShouldRaiseError_StateProvinceCode_InvalidCharacter()
        {
            Action action = () => StateProvinceCode.FromString("T7");

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("The 2-digit state (province) code contains only letters.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_StateProvinceCode_EmptyString()
        {
            Action action = () => StateProvinceCode.FromString("");

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The 2-digit state (province) code is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_StateProvinceCode_TooLong()
        {
            Action action = () => StateProvinceCode.FromString("ABC");

            var caughtException = Assert.Throws<ArgumentOutOfRangeException>(action);

            Assert.Contains("The state (province) code must be 2 characters.", caughtException.Message);
        }
    }
}