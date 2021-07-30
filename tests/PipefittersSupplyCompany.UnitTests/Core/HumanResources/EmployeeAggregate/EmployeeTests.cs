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
            Guid eeID = Guid.NewGuid();
            EmployeeType eeType = new EmployeeType(1, "Administrator");
            Guid supvID = eeID;
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

        [Fact]
        public void ShouldRaiseError_NewEmployee_EmployeeIdEqualDefaultGuid()
        {
            Guid eeID = new Guid();
            EmployeeType eeType = new EmployeeType(1, "Administrator");
            Guid supvID = Guid.NewGuid(); ;
            PersonName name = PersonName.Create("Joe", "Blow", "B");
            Address address = Address.Create("123 Main Street", "Apt 2", "Somewhere", "TX", "75654");
            SSN ssn = SSN.Create("587887964");
            Telephone phone = Telephone.Create("555-555-5555");
            MaritalStatus maritalStatus = MaritalStatus.Create("M");
            TaxExemption exemption = TaxExemption.Create(5);
            PayRate payRate = PayRate.Create(25.00M);
            StartDate startDate = StartDate.Create(new DateTime(2018, 6, 17));
            IsActive status = IsActive.Create(true);

            Action action = () => new Employee
            (
                eeID, eeType, supvID, name, address, ssn, phone, maritalStatus, exemption, payRate, startDate, status
            );

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The employee id is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_NewEmployee_SupervisorIdEqualDefaultGuid()
        {
            Guid eeID = Guid.NewGuid();
            EmployeeType eeType = new EmployeeType(1, "Administrator");
            Guid supvID = new Guid();
            PersonName name = PersonName.Create("Joe", "Blow", "B");
            Address address = Address.Create("123 Main Street", "Apt 2", "Somewhere", "TX", "75654");
            SSN ssn = SSN.Create("587887964");
            Telephone phone = Telephone.Create("555-555-5555");
            MaritalStatus maritalStatus = MaritalStatus.Create("M");
            TaxExemption exemption = TaxExemption.Create(5);
            PayRate payRate = PayRate.Create(25.00M);
            StartDate startDate = StartDate.Create(new DateTime(2018, 6, 17));
            IsActive status = IsActive.Create(true);

            Action action = () => new Employee
            (
                eeID, eeType, supvID, name, address, ssn, phone, maritalStatus, exemption, payRate, startDate, status
            );

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The supervisor id is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_NewEmployee_WithNullName()
        {
            Guid eeID = Guid.NewGuid();
            EmployeeType eeType = new EmployeeType(1, "Administrator");
            Guid supvID = eeID;
            PersonName name = PersonName.Create("Joe", "Blow", "B");
            Address address = Address.Create("123 Main Street", "Apt 2", "Somewhere", "TX", "75654");
            SSN ssn = SSN.Create("587887964");
            Telephone phone = Telephone.Create("555-555-5555");
            MaritalStatus maritalStatus = MaritalStatus.Create("M");
            TaxExemption exemption = TaxExemption.Create(5);
            PayRate payRate = PayRate.Create(25.00M);
            StartDate startDate = StartDate.Create(new DateTime(2018, 6, 17));
            IsActive status = IsActive.Create(true);

            Action action = () => new Employee
            (
                eeID, eeType, supvID, null, address, ssn, phone, maritalStatus, exemption, payRate, startDate, status
            );

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("Value cannot be null.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_NewEmployee_WithNullEmployeeType()
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

            Action action = () => new Employee
            (
                eeID, null, supvID, name, address, ssn, phone, maritalStatus, exemption, payRate, startDate, status
            );

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("Value cannot be null.", caughtException.Message);
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