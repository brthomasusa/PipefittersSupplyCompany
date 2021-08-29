using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.IntegrationTests.Base;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.IntegrationTests.HumanResources.Commands
{
    public class EmployeeCommandsTest : IntegrationTestBase
    {
        [Fact]
        public void ShouldInsert_ExternalAgentAndEmployee()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Employee employee = new Employee
            (
                employeeAgent,
                SupervisorId.Create(employeeAgent.Id),
                PersonName.Create("George", "Orwell", "J"),
                SSN.Create("323789999"),
                PhoneNumber.Create("817-987-1234"),
                MaritalStatus.Create("M"),
                TaxExemption.Create(5),
                PayRate.Create(40.00M),
                StartDate.Create(new DateTime(1998, 12, 2)),
                IsActive.Create(true)
            );

            _dbContext.ExternalAgents.Add(employeeAgent);
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();

            var employeeResult = _dbContext.Employees.Find(employeeAgent.Id);
            var agentResult = _dbContext.ExternalAgents.Find(employeeAgent.Id);

            Assert.IsType<ExternalAgent>(agentResult);
            Assert.IsType<Employee>(employeeResult);

            // Assert.Null(agentResult.Employee); It is doing eager loading!
        }

        [Fact]
        public void ShouldUpdate_EmployeeUsingDbContext()
        {
            var employee = _dbContext.Employees.Find(new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"));

            Assert.Equal("Ken", employee.EmployeeName.FirstName);
            Assert.Equal("Sanchez", employee.EmployeeName.LastName);

            employee.UpdateEmployeeName(PersonName.Create("Lil", "Wayne", null));

            _dbContext.Employees.Update(employee);
            var updatedEmployee = _dbContext.Employees.Find(new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"));

            Assert.Equal("Lil", updatedEmployee.EmployeeName.FirstName);
            Assert.Equal("Wayne", updatedEmployee.EmployeeName.LastName);
        }

        [Fact]
        public void ShouldRetrieve_ExternalAgentAndIncludeEmployee()
        {
            var employeeAgent = new ExternalAgent(Guid.NewGuid(), AgentType.Employee);

            Employee employee = new Employee
            (
                employeeAgent,
                SupervisorId.Create(employeeAgent.Id),
                PersonName.Create("George", "Orwell", "J"),
                SSN.Create("523789999"),
                PhoneNumber.Create("817-987-1234"),
                MaritalStatus.Create("M"),
                TaxExemption.Create(5),
                PayRate.Create(40.00M),
                StartDate.Create(new DateTime(1998, 12, 2)),
                IsActive.Create(true)
            );

            _dbContext.ExternalAgents.Add(employeeAgent);
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();

            var result = _dbContext.ExternalAgents
                        .Where(a => a.Id.Equals(employeeAgent.Id))
                        .Include(e => e.Employee)
                        .FirstOrDefault();

            Assert.NotNull(result);
            Assert.Equal("Orwell", result.Employee.EmployeeName.LastName);
        }

        [Fact]
        public void ShouldAdd_AddressToEmployee()
        {
            var employee = _dbContext.Employees.Find(new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"));
            var originalAddressCount = employee.Addresses().Count;

            employee.AddAddress(0, AddressVO.Create("123 Main", "#4", "Somewhere", "TX", "78885"));
            _dbContext.SaveChanges();

            var addressCount = employee.Addresses().Count;

            Assert.True(addressCount > originalAddressCount);
        }

        [Fact]
        public void ShouldUpdate_EmployeeAddress()
        {
            var employee = _dbContext.Employees.Find(new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"));
            var address = (from item in employee.Addresses()
                           where item.Id.Equals(1)
                           select item).SingleOrDefault();

            Assert.Equal("321 Tarrant Pl", address.AddressDetails.AddressLine1);

            employee.UpdateAddress(address.Id, AddressVO.Create("123 Main Blvd", "#4", "Anywhere", "TX", "78885"));
            _dbContext.SaveChanges();

            var result = (from item in employee.Addresses()
                          where item.Id.Equals(1)
                          select item).SingleOrDefault();

            Assert.Equal("123 Main Blvd", result.AddressDetails.AddressLine1);
            Assert.Equal("Anywhere", result.AddressDetails.City);
        }

        [Fact]
        public void ShouldDelete_EmployeeAddress()
        {
            var employee = _dbContext.Employees.Find(new Guid("9f7b902d-566c-4db6-b07b-716dd4e04340"));

            var address = (from item in employee.Addresses()
                           where item.Id.Equals(5)
                           select item).SingleOrDefault();

            employee.DeleteAddress(address.Id);
            _dbContext.SaveChanges();

            var result = (from item in employee.Addresses()
                          where item.Id.Equals(5)
                          select item).SingleOrDefault();

            Assert.Null(result);
        }

        [Fact]
        public void ShouldAdd_ContactPersonToEmployee()
        {
            var employee = _dbContext.Employees.Find(new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"));
            var originalContactCount = employee.ContactPersons().Count;

            var name = PersonName.Create("Fidel", "Castro", null);
            var phone = PhoneNumber.Create("555-555-5555");
            var notes = "You are being tested.";

            employee.AddContactPerson(0, name, phone, notes);
            _dbContext.SaveChanges();

            var count = employee.ContactPersons().Count;

            Assert.Equal(originalContactCount + 1, count);
        }

        [Fact]
        public void ShouldUpdate_EmployeeContactPerson()
        {
            var employee = _dbContext.Employees.Find(new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"));
            var contact = (from item in employee.ContactPersons()
                           where item.Id.Equals(2)
                           select item).SingleOrDefault();

            Assert.Equal("Steve", contact.ContactName.FirstName);
            Assert.Equal("Harvey", contact.ContactName.LastName);
            Assert.Equal("972-854-5688", contact.Telephone);

            employee.UpdateContactPerson
            (
                contact.Id,
                PersonName.Create("Bubba", "Smith", "C"),
                PhoneNumber.Create("987-965-1234"),
                "You have been updated"
            );
            _dbContext.SaveChanges();

            var result = (from item in employee.ContactPersons()
                          where item.Id.Equals(2)
                          select item).SingleOrDefault();

            Assert.Equal("Bubba", result.ContactName.FirstName);
            Assert.Equal("987-965-1234", result.Telephone);
            Assert.Equal("You have been updated", result.Notes);
        }

        [Fact]
        public void ShouldDelete_EmployeeContactPerson()
        {
            var employee = _dbContext.Employees.Find(new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"));
            var contact = (from item in employee.ContactPersons()
                           where item.Id.Equals(2)
                           select item).SingleOrDefault();

            employee.DeleteContactPerson(contact.Id);
            _dbContext.SaveChanges();

            var result = (from item in employee.ContactPersons()
                          where item.Id.Equals(2)
                          select item).SingleOrDefault();

            Assert.Null(result);
        }
    }
}