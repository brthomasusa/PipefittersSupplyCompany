using System;
using Xunit;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.UnitTests.Core.HumanResources.EmployeeAggregate
{
    public class EmployeeTests
    {
        [Fact]
        public void ShouldReturnValid_NewEmployee()
        {
            EmployeeID eeID = EmployeeID.Create(Guid.NewGuid());
            EmployeeType eeType = new EmployeeType(1, "Administrator");
            EmployeeID supvID = eeID;
            PersonName name = PersonName.Create("Joe", "Blow", "B");
            Address address = Address.Create("123 Main Street", "Apt 2", "Somewhere", "TX", "75654");
            SSN ssn = SSN.Create("587887964");
            Telephone phone = Telephone.Create("555-555-5555");
            MaritalStatus maritalStatus = MaritalStatus.Create("M");
            TaxExemption exemption = TaxExemption.Create(5);
            PayRate payRate = PayRate.Create(25.00M);
            StartDate startDate = StartDate.Create(new DateTime(2018, 6, 17));
            IsActive status = IsActive.Create(true);

            Employee employee = new Employee
            (
                eeID, eeType, supvID, name, address, ssn, phone, maritalStatus, exemption, payRate, startDate, status
            );

            Assert.IsType<Employee>(employee);
        }

        private Employee GetEmployee()
        {
            EmployeeID eeID = EmployeeID.Create(Guid.NewGuid());
            EmployeeType eeType = new EmployeeType(1, "Administrator");
            EmployeeID supvID = eeID;
            PersonName name = PersonName.Create("Joe", "Blow", "B");
            Address address = Address.Create("123 Main Street", "Apt 2", "Somewhere", "TX", "75654");
            SSN ssn = SSN.Create("587887964");
            Telephone phone = Telephone.Create("555-555-5555");
            MaritalStatus maritalStatus = MaritalStatus.Create("M");
            TaxExemption exemption = TaxExemption.Create(5);
            PayRate payRate = PayRate.Create(25.00M);
            StartDate startDate = StartDate.Create(new DateTime(2018, 6, 17));
            IsActive status = IsActive.Create(true);

            return new Employee
            (
                eeID, eeType, supvID, name, address, ssn, phone, maritalStatus, exemption, payRate, startDate, status
            );
        }
    }
}