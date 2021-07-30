using System;
using Xunit;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.UnitTests.Core.HumanResources.EmployeeAggregate
{
    public class EmployeeTests
    {
        [Fact]
        public void ShouldReturnValid_Employee_UsingCtor()
        {
            DateTime hireDate = new DateTime(2018, 6, 17);
            Guid eeID = Guid.NewGuid();
            EmployeeType eeType = new EmployeeType(1, "Administrator");
            Guid mgrID = eeID;
            PersonName name = PersonName.Create("Joe", "Blow", "B");
            Address address = Address.Create("123 Main Street", "Apt 2", "Somewhere", "TX", "75654");
            SSN ssn = SSN.Create("587887964");

            // var phone = "555-555-5555";
            // var maritalStatus = "M";
            // var exempt = 5;
            // var payRate = 25.00M;
            // var startDate = hireDate;
            // var isActive = IsActive.Create(true);

            //     var employee = new Employee
            //     (
            //         eeID, eeTypeID, mgrID, lname, fname, mi, ssn, line1, line2, city, stateCode,
            //         zipcode, phone, maritalStatus, exempt, payRate, startDate, isActive
            //     );

            //     Assert.IsType<Employee>(employee);
            //     Assert.Equal(1, employee.Id);
            //     Assert.Equal(3, employee.EmployeeTypeId);
            //     Assert.Equal(1, employee.SupervisorId);
            //     Assert.Equal("Pimp", employee.LastName);
            //     Assert.Equal("Big", employee.FirstName);
            //     Assert.Equal("P", employee.MiddleInitial);
            //     Assert.Equal("587887964", employee.SSN);
            //     Assert.Equal("123 Main Street", employee.AddressLine1);
            //     Assert.Equal("Apt 2", employee.AddressLine2);
            //     Assert.Equal("Somewhere", employee.City);
            //     Assert.Equal("TX", employee.State);
            //     Assert.Equal("75654", employee.Zipcode);
            //     Assert.Equal("555-555-5555", employee.Telephone);
            //     Assert.Equal("M", employee.MaritalStatus);
            //     Assert.Equal(5, employee.Exemptions);
            //     Assert.Equal(25.00M, employee.PayRate);
            //     Assert.Equal(hireDate, employee.StartDate);
            //     Assert.True(employee.IsActive);
            //     Assert.NotNull(employee.CreatedDate);
            //     Assert.NotEqual(default(DateTime), employee.CreatedDate);
        }
    }
}