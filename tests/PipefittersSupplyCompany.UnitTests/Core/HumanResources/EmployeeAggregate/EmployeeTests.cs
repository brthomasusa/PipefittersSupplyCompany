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
        public void ShouldAttach_NewEmployeeToExternalAgent()
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

            // employeeAgent.SetEmployee(employee);
            Assert.IsType<Employee>(employeeAgent.Employee);
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

            employee.AddAddress(AddressVO.Create("123 Main", "#4", "Somewhere", "TX", "78885"));

            var count = employee.Addresses().Count;

            Assert.Equal(1, count);
        }

        [Fact]
        public void ShouldAdd_ContactPersonToEmployee()
        {
            var employee = GetEmployee();
            var name = PersonName.Create("Fidel", "Castro", null);
            var phone = PhoneNumber.Create("555-555-5555");
            var notes = "You are being tested.";

            employee.AddContactPerson(name, phone, notes);

            var count = employee.ContactPersons().Count;

            Assert.Equal(1, count);
        }

        // Helper methods
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
    }
}