using System;
using Xunit;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.UnitTests.Core.HumanResources.EmployeeAggregate
{
    public class EmployeeTests
    {
        [Fact]
        public void ShouldReturn_ValidExternalAgent()
        {
            var result = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Assert.IsType<ExternalAgent>(result);
        }

        [Fact]
        public void ShouldRaiseError_InvalidAgentTypeId()
        {
            Action action = () => new ExternalAgent(Guid.NewGuid(), 0);

            var caughtException = Assert.Throws<ArgumentException>(action);

            Assert.Contains("Undefined agent type.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_UninitializedGuidForAgentId()
        {
            Action action = () => new ExternalAgent(new Guid(), AgentType.Employee);

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The agent id is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldReturn_NewEmployee()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Employee employee = new Employee
            (
                employeeAgent,
                SupervisorId.Create(employeeAgent.Id),
                PersonName.Create("Ken", "Sanchez", "J"),
                SSN.Create("123789999"),
                PhoneNumber.Create("817-987-1234"),
                MaritalStatus.Create("M"),
                TaxExemption.Create(5),
                PayRate.Create(40.00M),
                StartDate.Create(new DateTime(1998, 12, 2)),
                IsActive.Create(true)
            );

            Assert.IsType<Employee>(employee);
        }

        [Fact]
        public void ShouldRaiseError_SupervisorIdEqualDefaultGuid()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Action action = () => new Employee
           (
                employeeAgent,
                SupervisorId.Create(new Guid()),
                PersonName.Create("Ken", "Sanchez", "J"),
                SSN.Create("123789999"),
                PhoneNumber.Create("817-987-1234"),
                MaritalStatus.Create("M"),
                TaxExemption.Create(5),
                PayRate.Create(40.00M),
                StartDate.Create(new DateTime(1998, 12, 2)),
                IsActive.Create(true)
           );

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The supervisor id is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_NewEmployee_WithNullLastName()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Action action = () => new Employee
           (
                employeeAgent,
                SupervisorId.Create(Guid.NewGuid()),
                PersonName.Create("Ken", null, "J"),
                SSN.Create("123789999"),
                PhoneNumber.Create("817-987-1234"),
                MaritalStatus.Create("M"),
                TaxExemption.Create(5),
                PayRate.Create(40.00M),
                StartDate.Create(new DateTime(1998, 12, 2)),
                IsActive.Create(true)
           );

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("A last name is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_NewEmployee_WithNullFirstName()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Action action = () => new Employee
           (
                employeeAgent,
                SupervisorId.Create(Guid.NewGuid()),
                PersonName.Create(null, "Sanchez", "J"),
                SSN.Create("123789999"),
                PhoneNumber.Create("817-987-1234"),
                MaritalStatus.Create("M"),
                TaxExemption.Create(5),
                PayRate.Create(40.00M),
                StartDate.Create(new DateTime(1998, 12, 2)),
                IsActive.Create(true)
           );

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("A first name is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldAdd_AddressToEmployee()
        {
            var employee = GetEmployee();

            employee.AddAddress(1, AddressVO.Create("123 Main", "#4", "Somewhere", "TX", "78885"));

            var count = employee.Addresses().Count;

            Assert.Equal(1, count);
        }

        [Fact]
        public void ShouldRaiseError_AddingDuplicateAddressToEmployee()
        {
            var employee = GetEmployee();

            employee.AddAddress(1, AddressVO.Create("11123 Main", null, "Somewhere", "TX", "78885"));
            Action action = () => employee.AddAddress(2, AddressVO.Create("11123 Main", null, "Somewhere", "TX", "78885"));

            var caughtException = Assert.Throws<InvalidOperationException>(action);

            Assert.Contains("We already have this address.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_CallingAddAddressWithNull()
        {
            var employee = GetEmployee();

            Action action = () => employee.AddAddress(0, null);

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("Can not add null to list of agent addresses.", caughtException.Message);
        }

        [Fact]
        public void ShouldAdd_ContactPersonToEmployee()
        {
            var employee = GetEmployee();
            var name = PersonName.Create("Fidel", "Castro", null);
            var phone = PhoneNumber.Create("555-555-5555");
            var notes = "You are being tested.";

            employee.AddContactPerson(1, name, phone, notes);

            var count = employee.ContactPersons().Count;

            Assert.Equal(1, count);
        }

        [Fact]
        public void ShouldRaiseError_AddingDuplicateContactPersonToEmployee()
        {
            var employee = GetEmployee();
            var name = PersonName.Create("Fidel", "Castro", null);
            var phone = PhoneNumber.Create("555-555-5555");
            var notes = "You are being tested.";

            employee.AddContactPerson(1, name, phone, notes);

            Action action = () => employee.AddContactPerson(2, name, phone, notes);

            var caughtException = Assert.Throws<InvalidOperationException>(action);

            Assert.Contains("We already have this contact person.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_AddingContactPersonWithNullNameToEmployee()
        {
            var employee = GetEmployee();
            var phone = PhoneNumber.Create("555-555-5555");
            var notes = "You are being tested.";

            Action action = () => employee.AddContactPerson(1, null, phone, notes);

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The contact person name is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldRaiseError_AddingContactPersonWithNullTelephoneToEmployee()
        {
            var employee = GetEmployee();
            var name = PersonName.Create("Fidel", "Castro", null);
            var notes = "You are being tested.";

            Action action = () => employee.AddContactPerson(1, name, null, notes);

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("The contact person telephone number is required.", caughtException.Message);
        }

        [Fact]
        public void ShouldUpdate_Employee_TaxExemptions()
        {
            var employee = GetEmployee();

            Assert.Equal(5, employee.TaxExemption);

            employee.UpdateTaxExemptions(TaxExemption.Create(6));

            Assert.Equal(6, employee.TaxExemption);
        }

        [Fact]
        public void ShoulRaiseError_UpdateEmployee_TaxExemptionWithNull()
        {
            var employee = GetEmployee();

            Action action = () => employee.UpdateTaxExemptions(null);

            var caughtException = Assert.Throws<ArgumentNullException>(action);

            Assert.Contains("Employee tax exemptions can not be updated with null.", caughtException.Message);
        }

        [Fact]
        public void ShouldUpdate_EmployeeAddress()
        {
            var employee = GetEmployeeWithAddresses();
            var address = employee.Addresses()[0];

            Assert.Equal("123 Main Terrace", address.AddressDetails.AddressLine1);
            Assert.Equal("Somewhere", address.AddressDetails.City);

            employee.UpdateAddress(address.Id, AddressVO.Create("123 Main Blvd", "#4", "Anywhere", "TX", "78885"));

            address = employee.Addresses()[0];

            Assert.Equal("123 Main Blvd", address.AddressDetails.AddressLine1);
            Assert.Equal("Anywhere", address.AddressDetails.City);
        }

        [Fact]
        public void ShouldUpdate_EmployeeContactPerson()
        {
            var employee = GetEmployeeWithContactPeople();
            var contact = employee.ContactPersons()[0];

            Assert.Equal("Fidel", contact.ContactName.FirstName);
            Assert.Equal("555-555-1234", contact.Telephone);
            Assert.Equal("You are being tested.", contact.Notes);

            employee.UpdateContactPerson
            (
                contact.Id,
                PersonName.Create("Bubba", "Smith", "C"),
                PhoneNumber.Create("987-965-1234"),
                "You have been updated"
            );

            Assert.Equal("Bubba", contact.ContactName.FirstName);
            Assert.Equal("987-965-1234", contact.Telephone);
            Assert.Equal("You have been updated", contact.Notes);
        }

        [Fact]
        public void ShouldDelete_EmployeeAddress()
        {
            var employee = GetEmployeeWithAddresses();
            var addressCount = employee.Addresses().Count;

            Assert.Equal(2, addressCount);

            var address = employee.Addresses()[0];
            Assert.Equal(1, address.Id);

            employee.DeleteAddress(address.Id);
            addressCount = employee.Addresses().Count;
            address = employee.Addresses()[0];

            Assert.Equal(1, addressCount);
            Assert.Equal(2, address.Id);
        }

        [Fact]
        public void ShouldDelete_EmployeeContactPerson()
        {
            var employee = GetEmployeeWithContactPeople();
            var count = employee.ContactPersons().Count;
            Assert.Equal(2, count);

            var contact = employee.ContactPersons()[0];
            Assert.Equal(1, contact.Id);

            employee.DeleteContactPerson(contact.Id);
            count = employee.ContactPersons().Count;
            contact = employee.ContactPersons()[0];

            Assert.Equal(1, count);
            Assert.Equal(2, contact.Id);
        }


        /**     Helper methods     **/
        private Employee GetEmployee()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);
            return new Employee
           (
                employeeAgent,
                SupervisorId.Create(Guid.NewGuid()),
                PersonName.Create("George", "Orwell", "Z"),
                SSN.Create("123789999"),
                PhoneNumber.Create("817-987-1234"),
                MaritalStatus.Create("M"),
                TaxExemption.Create(5),
                PayRate.Create(40.00M),
                StartDate.Create(new DateTime(1998, 12, 2)),
                IsActive.Create(true)
           );
        }

        private Employee GetEmployeeWithAddresses()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);
            var employee = new Employee
           (
                employeeAgent,
                SupervisorId.Create(Guid.NewGuid()),
                PersonName.Create("George", "Orwell", "Z"),
                SSN.Create("123789999"),
                PhoneNumber.Create("817-987-1234"),
                MaritalStatus.Create("M"),
                TaxExemption.Create(5),
                PayRate.Create(40.00M),
                StartDate.Create(new DateTime(1998, 12, 2)),
                IsActive.Create(true)
           );

            employee.AddAddress(1, AddressVO.Create("123 Main Terrace", "#4", "Somewhere", "TX", "78885"));
            employee.AddAddress(2, AddressVO.Create("123 Main Plaza", "Apt 13", "nowhere", "TX", "78981"));

            return employee;
        }

        private Employee GetEmployeeWithContactPeople()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);
            var employee = new Employee
           (
                employeeAgent,
                SupervisorId.Create(Guid.NewGuid()),
                PersonName.Create("George", "Orwell", "Z"),
                SSN.Create("123789999"),
                PhoneNumber.Create("817-987-1234"),
                MaritalStatus.Create("M"),
                TaxExemption.Create(5),
                PayRate.Create(40.00M),
                StartDate.Create(new DateTime(1998, 12, 2)),
                IsActive.Create(true)
           );

            employee.AddContactPerson(1, PersonName.Create("Fidel", "Castro", "C"), PhoneNumber.Create("555-555-1234"), "You are being tested.");
            employee.AddContactPerson(2, PersonName.Create("Fidel", "Raul", "Z"), PhoneNumber.Create("555-555-5678"), "You are not being tested.");

            return employee;
        }
    }
}