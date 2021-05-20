using System;
using PipefittersSupply.Domain.Common;
using PipefittersSupply.Domain.HumanResources.Employee;
using PipefittersSupply.Domain.Lookup;
using PipefittersSupply.Tests;
using Xunit;

namespace PipefittersSupply.Tests
{
    public class EmployeeTest
    {
        private readonly IStateProvinceLookup _stateProvinceLookup = new MockStateProvinceCodeLookup();

        [Fact]
        public void ShouldReturnValid_Employee_UsingCtor()
        {
            var hireDate = new DateTime(2018, 6, 17);
            var eeID = new EmployeeId(1);
            var eeTypeID = EmployeeTypeIdentifier.FromInterger(3);
            var lname = EmployeeLastName.FromString("Pimp");
            var fname = EmployeeFirstName.FromString("Big");
            var mi = EmployeeMiddleInitial.FromString("P");
            var ssn = EmployeeSSN.FromString("587887964");
            var line1 = AddressLine1.FromString("123 Main Street");
            var line2 = AddressLine2.FromString("Apt 2");
            var city = City.FromString("Somewhere");
            var stateCode = StateProvinceCode.FromString("TX", _stateProvinceLookup);
            var zipcode = Zipcode.FromString("75654");
            var phone = Telephone.FromString("555-555-5555");
            var maritalStatus = MaritalStatus.FromString("M");
            var exempt = TaxExemption.FromInterger(5);
            var payRate = EmployeePayRate.FromDecimal(25.00M);
            var startDate = EmployeeStartDate.FromDateTime(hireDate);
            var isActive = IsActive.FromBoolean(true);

            var employee = new Employee
            (
                eeID, eeTypeID, lname, fname, mi, ssn, line1, line2, city, stateCode,
                zipcode, phone, maritalStatus, exempt, payRate, startDate, isActive
            );

            Assert.IsType<Employee>(employee);
            Assert.Equal(1, employee.Id);
            Assert.Equal(3, employee.EmployeeTypeId);
            Assert.Equal("Pimp", employee.LastName);
            Assert.Equal("Big", employee.FirstName);
            Assert.Equal("P", employee.MiddleInitial);
            Assert.Equal("587887964", employee.SSN);
            Assert.Equal("123 Main Street", employee.AddressLine1);
            Assert.Equal("Apt 2", employee.AddressLine2);
            Assert.Equal("Somewhere", employee.City);
            Assert.Equal("TX", employee.State);
            Assert.Equal("75654", employee.Zipcode);
            Assert.Equal("555-555-5555", employee.Telephone);
            Assert.Equal("M", employee.MaritalStatus);
            Assert.Equal(5, employee.Exemptions);
            Assert.Equal(25.00M, employee.PayRate);
            Assert.Equal(hireDate, employee.StartDate);
            Assert.True(employee.IsActive);
            Assert.NotNull(employee.CreatedDate);
            Assert.NotEqual(default(DateTime), employee.CreatedDate);
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
            var stateCode = StateProvinceCode.FromString("Tx", _stateProvinceLookup);

            Assert.IsType<StateProvinceCode>(stateCode);
        }

        [Fact]
        public void ShouldRaiseError_StateProvinceCode_InvalidCharacter()
        {
            Action action = () => StateProvinceCode.FromString("T7", _stateProvinceLookup);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("The 2-digit state (province) code is invalid.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_StateProvinceCode_EmptyString()
        {
            Action action = () => StateProvinceCode.FromString("", _stateProvinceLookup);

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The 2-digit state (province) code is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_StateProvinceCode_TooLong()
        {
            Action action = () => StateProvinceCode.FromString("ABC", _stateProvinceLookup);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("The 2-digit state (province) code is invalid.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_StateProvinceCode_TooShort()
        {
            Action action = () => StateProvinceCode.FromString("A", _stateProvinceLookup);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("The 2-digit state (province) code is invalid.", caughtException.Message);
        }

        [Fact]
        public void ShouldReturnValid_Telephone_10Digits_Dashes()
        {
            var telephone = Telephone.FromString("555-555-5555");

            Assert.IsType<Telephone>(telephone);
        }

        [Fact]
        public void ShouldReturnValid_Telephone_10Digits_FullyFormatted()
        {
            var telephone = Telephone.FromString("(555)-555-5555");

            Assert.IsType<Telephone>(telephone);
        }

        [Fact]
        public void ShouldReturnValid_Telephone_10Digits_NoDashes()
        {
            var telephone = Telephone.FromString("5555555555");

            Assert.IsType<Telephone>(telephone);
        }

        [Fact]
        public void ShouldRaiseError_Telephone_EmptyString()
        {
            Action action = () => Telephone.FromString("");

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The telephone number is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldReturnValid_MaritalStatus_UCase()
        {
            var status = MaritalStatus.FromString("M");

            Assert.IsType<MaritalStatus>(status);
        }

        [Fact]
        public void ShouldReturnValid_MaritalStatus_LCase()
        {
            var status = MaritalStatus.FromString("s");

            Assert.IsType<MaritalStatus>(status);
        }

        [Fact]
        public void ShouldRaiseError_MaritalStatus_EmptyString()
        {
            Action action = () => MaritalStatus.FromString("");

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The marital status is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_MaritalStatus_InvalidCharacter()
        {
            Action action = () => MaritalStatus.FromString("T");

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Invalid marital status, valid statues are 'S' and 'M'.", caughtException.Message);
        }

        [Fact]
        public void ShouldReturnValid_EmployeePayRate()
        {
            var payRate = EmployeePayRate.FromDecimal(7.50M);

            Assert.IsType<EmployeePayRate>(payRate);
        }

        [Fact]
        public void ShouldRaiseError_PayRate_TooLow()
        {
            Action action = () => EmployeePayRate.FromDecimal(7.49M);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Invalid pay rate, must be between $7.50 and $40.00 (per hour) inclusive!", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_PayRate_TooHigh()
        {
            Action action = () => EmployeePayRate.FromDecimal(40.01M);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Invalid pay rate, must be between $7.50 and $40.00 (per hour) inclusive!", caughtException.Message);
        }

        [Fact]
        public void ShouldReturnValid_EmployeeStartDate()
        {
            var startDate = EmployeeStartDate.FromDateTime(new DateTime(2021, 1, 1));

            Assert.IsType<EmployeeStartDate>(startDate);
        }

        [Fact]
        public void ShouldRaiseError_EmployeeStartDate_DefaultDate()
        {
            Action action = () => EmployeeStartDate.FromDateTime(new DateTime());

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The employee start date is required.", caughtException.Message);
        }
    }
}