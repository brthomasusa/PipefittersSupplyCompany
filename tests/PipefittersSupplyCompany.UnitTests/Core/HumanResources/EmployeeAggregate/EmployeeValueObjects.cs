using System;
using Xunit;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.UnitTests.Core.HumanResources.EmployeeAggregate
{
    public class EmployeeValueObjectTests
    {
        // SupervisorId

        [Fact]
        public void ShouldReturn_Valid_SupervisorId()
        {
            Guid eeID = Guid.NewGuid();

            var result = SupervisorId.Create(eeID);

            Assert.IsType<SupervisorId>(result);
            Assert.Equal(eeID, result);
        }

        [Fact]
        public void ShouldRaiseError_SupervisorId_IsDefaultGuid()
        {
            Guid eeID = new Guid();

            Action action = () => SupervisorId.Create(eeID);

            var caughtException = Assert.Throws<ArgumentOutOfRangeException>(action);

            Assert.Equal("The installment number must be greater than or equal to one.", caughtException.Message);
        }


        // PersonName

        [Fact]
        public void ShouldReturn_Valid_PersonName()
        {
            string first = "Joe";
            string last = "Blow";
            string mi = "Z";

            var result = PersonName.Create(first, last, mi);

            Assert.IsType<PersonName>(result);
            Assert.Equal(first, result.FirstName);
            Assert.Equal(last, result.LastName);
            Assert.Equal(mi, result.MiddleInitial);
        }

        [Fact]
        public void ShouldReturn_Valid_PersonName_WithNullMiddleInitial()
        {
            string first = "Joe";
            string last = "Blow";
            string mi = null;

            var result = PersonName.Create(first, last, mi);

            Assert.IsType<PersonName>(result);
            Assert.Equal(first, result.FirstName);
            Assert.Equal(last, result.LastName);
            Assert.Equal(mi, result.MiddleInitial);
        }

        [Fact]
        public void ShouldRaiseError_NullFirstName()
        {
            string first = null;
            string last = "Blow";
            var mi = "Z";

            Action action = () => PersonName.Create(first, last, mi);

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Equal("A first name is required.", caughtException.ParamName);
        }

        [Fact]
        public void ShouldRaiseError_NullLastName()
        {
            var first = "Joe";
            string last = null;
            var mi = "Z";

            Action action = () => PersonName.Create(first, last, mi);

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Equal("A last name is required.", caughtException.ParamName);
        }

        [Fact]
        public void ShouldRaiseError_FirstNameTooLong()
        {
            var first = "Joe123456789saaadaadddaaaaaaaaaaaaaeeeeeeessssss";
            string last = "Blow";
            var mi = "Z";

            Action action = () => PersonName.Create(first, last, mi);

            var caughtException = Assert.Throws<ArgumentOutOfRangeException>(action);

            Assert.Contains("Maximum length of the first name is 25 characters.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_LastNameTooLong()
        {
            var first = "Joe";
            string last = "Blow123456789saaadaadddaaaaaaaaaaaaaeeeeeeessssss";
            var mi = "Z";

            Action action = () => PersonName.Create(first, last, mi);

            var caughtException = Assert.Throws<ArgumentOutOfRangeException>(action);

            Assert.Contains("Maximum length of the last name is 25 characters.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_MiddleInitialTooLong()
        {
            var first = "Joe";
            string last = "Blow";
            var mi = "ZZ";

            Action action = () => PersonName.Create(first, last, mi);

            var caughtException = Assert.Throws<ArgumentOutOfRangeException>(action);

            Assert.Contains("Maximum length of middle initial is 1 character.", caughtException.Message);
        }

        // Address

        [Fact]
        public void ShouldReturn_Valid_Address_WithSecondAddressLine()
        {
            string line1 = "123 Main Street";
            string line2 = "Suite 1A";
            string city = "Anywhereville";
            string stateCode = "TX";
            string zipcode = "75216";

            var result = AddressVO.Create(line1, line2, city, stateCode, zipcode);

            Assert.IsType<AddressVO>(result);
            Assert.Equal(line1, result.AddressLine1);
            Assert.Equal(line2, result.AddressLine2);
            Assert.Equal(city, result.City);
            Assert.Equal(stateCode, result.StateCode);
            Assert.Equal(zipcode, result.Zipcode);
        }

        [Fact]
        public void ShouldReturn_Valid_Address_WithoutSecondAddressLine()
        {
            string line1 = "123 Main Street";
            string line2 = null;
            string city = "Anywhereville";
            string stateCode = "TX";
            string zipcode = "75216";

            var result = AddressVO.Create(line1, line2, city, stateCode, zipcode);

            Assert.IsType<AddressVO>(result);
            Assert.Equal(line1, result.AddressLine1);
            Assert.Null(result.AddressLine2);
            Assert.Equal(city, result.City);
            Assert.Equal(stateCode, result.StateCode);
            Assert.Equal(zipcode, result.Zipcode);
        }

        [Fact]
        public void ShouldRaiseError_Invalid_StateCodeDoesNotExist()
        {
            string line1 = "123 Main Street";
            string line2 = null;
            string city = "Anywhereville";
            string stateCode = "IX";
            string zipcode = "75216";

            Action action = () => AddressVO.Create(line1, line2, city, stateCode, zipcode);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Invalid state code!", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_Invalid_StateCodeIsNull()
        {
            string line1 = "123 Main Street";
            string line2 = null;
            string city = "Anywhereville";
            string stateCode = null;
            string zipcode = "75216";

            Action action = () => AddressVO.Create(line1, line2, city, stateCode, zipcode);

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("A 2-digit state code is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_Invalid_ZipcodeIsNull()
        {
            string line1 = "123 Main Street";
            string line2 = null;
            string city = "Anywhereville";
            string stateCode = "TX";
            string zipcode = null;

            Action action = () => AddressVO.Create(line1, line2, city, stateCode, zipcode);

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("A zip code is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_Invalid_Zipcode()
        {
            string line1 = "123 Main Street";
            string line2 = null;
            string city = "Anywhereville";
            string stateCode = "TX";
            string zipcode = "752136";

            Action action = () => AddressVO.Create(line1, line2, city, stateCode, zipcode);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Invalid zip code!", caughtException.Message);
        }

        [Fact]
        public void ShouldReturnValid_SSN()
        {
            string ssn = "587887964";

            var result = SSN.Create(ssn);

            Assert.IsType<SSN>(result);
            Assert.Equal(ssn, result);
        }

        [Fact]
        public void ShouldReturn_Valid_TelephoneNumber()
        {
            string phone = "972-555-5555";

            var result = PhoneNumber.Create(phone);

            Assert.IsType<PhoneNumber>(result);
            Assert.Equal(phone, result.Value);
            Assert.Equal(phone, result);
        }

        [Fact]
        public void ShouldRaiseError_Invalid_TelephoneNumber()
        {
            string phone = "0972-555-5555";

            Action action = () => PhoneNumber.Create(phone);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Invalid PhoneNumber number!", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_Invalid_TelephoneNumberIsNull()
        {
            string phone = null;

            Action action = () => PhoneNumber.Create(phone);

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The PhoneNumber number is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldReturn_Valid_MaritalStatus()
        {
            string status = "M";

            var result = MaritalStatus.Create(status);

            Assert.IsType<MaritalStatus>(result);
            Assert.Equal(status, result.Value);
        }

        [Fact]
        public void ShouldRaiseError_Invalid_MaritalStatusNotMorS()
        {
            string status = "D";

            Action action = () => MaritalStatus.Create(status);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Invalid marital status, valid statues are 'S' and 'M'.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_Invalid_MaritalStatusIsNull()
        {
            string status = null;

            Action action = () => MaritalStatus.Create(status);

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The marital status is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldReturn_Valid_IsActiveStatus()
        {
            bool status = true;

            var result = IsActive.Create(status);

            Assert.IsType<IsActive>(result);
            Assert.Equal(status, result.Value);
        }

        [Fact]
        public void ShouldReturn_Valid_EmployeePayRate()
        {
            decimal rate = 7.51M;

            var result = PayRate.Create(rate);

            Assert.IsType<PayRate>(result);
            Assert.Equal(rate, result.Value);
        }

        [Fact]
        public void ShouldRaiseError_Invalid_PayRateZero()
        {
            decimal rate = 0;

            Action action = () => PayRate.Create(rate);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Invalid pay rate; pay rate can not be zero!", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_Invalid_PayRateLessThanMinWage()
        {
            decimal rate = 7.49M;

            Action action = () => PayRate.Create(rate);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Invalid pay rate, must be between $7.50 and $40.00 (per hour) inclusive!", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_Invalid_PayRateTooLarge()
        {
            decimal rate = 40.01M;

            Action action = () => PayRate.Create(rate);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Invalid pay rate, must be between $7.50 and $40.00 (per hour) inclusive!", caughtException.Message);
        }

        [Fact]
        public void ShouldReturn_Valid_TaxExemption()
        {
            int exemption = 0;

            var result = TaxExemption.Create(exemption);

            Assert.IsType<TaxExemption>(result);
            Assert.Equal(exemption, result);
        }

        [Fact]
        public void ShouldRaiseError_Invalid_NegativeTaxExemption()
        {
            int exemption = -1;

            Action action = () => TaxExemption.Create(exemption);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Number of exemptions must be greater than or equal to zero.", caughtException.Message);
        }

        [Fact]
        public void ShouldReturn_Valid_StartDate()
        {
            DateTime start = new DateTime(2021, 6, 1);

            var result = StartDate.Create(start);

            Assert.IsType<StartDate>(result);
            Assert.Equal(start, result);
        }

        [Fact]
        public void ShouldRaiseError_Invalid_StartDateIsDefaultDate()
        {
            DateTime start = new DateTime();

            Action action = () => StartDate.Create(start);

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The employee start date is required.", caughtException.Message);
        }
    }
}