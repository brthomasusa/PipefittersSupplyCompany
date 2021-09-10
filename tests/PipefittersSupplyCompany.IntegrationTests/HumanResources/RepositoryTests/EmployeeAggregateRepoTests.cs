using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Persistence;
using PipefittersSupplyCompany.Infrastructure.Persistence.Repositories.HumanResources;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;
using PipefittersSupplyCompany.IntegrationTests.Base;

namespace PipefittersSupplyCompany.IntegrationTests.HumanResources.RepositoryTests
{
    public class EmployeeAggregateRepoTests : IntegrationTestBaseEfCore
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeAggregateRepository _employeeRepo;

        public EmployeeAggregateRepoTests()
        {
            TestDataInitialization.InitializeData(_dbContext);
            _unitOfWork = new AppUnitOfWork(_dbContext);
            _employeeRepo = new EmployeeAggregateRepository(_dbContext);
        }

        [Fact]
        public async Task ShouldInsert_ExternalAgentAndEmployee()
        {
            Guid id = Guid.NewGuid();

            Employee employee = new Employee
            (
                new ExternalAgent(id, AgentType.Employee),
                SupervisorId.Create(id),
                PersonName.Create("George", "Orwell", "J"),
                SSN.Create("623789999"),
                PhoneNumber.Create("817-987-1234"),
                MaritalStatus.Create("M"),
                TaxExemption.Create(5),
                PayRate.Create(40.00M),
                StartDate.Create(new DateTime(1998, 12, 2)),
                IsActive.Create(true)
            );

            await _employeeRepo.AddAsync(employee);
            await _unitOfWork.Commit();

            var employeeResult = await _employeeRepo.Exists(employee.Id);

            Assert.True(employeeResult);
        }

        [Fact]
        public async Task ShouldRetrieve_Employee()
        {
            var employee = await _employeeRepo.GetByIdAsync(new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"));

            Assert.Equal("Ken", employee.EmployeeName.FirstName);
            Assert.Equal("Sanchez", employee.EmployeeName.LastName);
        }

        [Fact]
        public async Task ShouldUpdate_Employee_UsingRepository()
        {
            var employee = await _employeeRepo.GetByIdAsync(new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"));

            Assert.Equal("Ken", employee.EmployeeName.FirstName);
            Assert.Equal("Sanchez", employee.EmployeeName.LastName);

            employee.UpdateEmployeeName(PersonName.Create("Lil", "Wayne", null));

            _employeeRepo.Update(employee);
            await _unitOfWork.Commit();

            var updatedEmployee = await _employeeRepo.GetByIdAsync(new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"));

            Assert.Equal("Lil", updatedEmployee.EmployeeName.FirstName);
            Assert.Equal("Wayne", updatedEmployee.EmployeeName.LastName);
        }

        [Fact]
        public async Task ShouldDelete_Employee_UsingRepository()
        {
            var employee = await _employeeRepo.GetByIdAsync(new Guid("e6b86ea3-6479-48a2-b8d4-54bd6cbbdbc5"));

            Assert.NotNull(employee);

            _employeeRepo.Delete(employee);
            await _unitOfWork.Commit();

            var result = await _employeeRepo.GetByIdAsync(new Guid("e6b86ea3-6479-48a2-b8d4-54bd6cbbdbc5"));
            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldAdd_AddressToEmployee_UsingRepo()
        {
            var employee = await _employeeRepo.GetByIdAsync(new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"));
            var originalAddressCount = employee.Addresses().Count;

            employee.AddAddress(0, AddressVO.Create("123 Main", "#4", "Somewhere", "TX", "78885"));
            _employeeRepo.Update(employee);
            await _unitOfWork.Commit();

            var addressCount = employee.Addresses().Count;

            Assert.Equal(originalAddressCount + 1, addressCount);
        }

        [Fact]
        public async Task ShouldUpdate_EmployeeAddress_UsingRepo()
        {
            var employee = await _employeeRepo.GetByIdAsync(new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"));
            var address = (from item in employee.Addresses()
                           where item.Id.Equals(1)
                           select item).SingleOrDefault();

            Assert.Equal("321 Tarrant Pl", address.AddressDetails.AddressLine1);

            employee.UpdateAddress(address.Id, AddressVO.Create("123 Main Blvd", "#4", "Anywhere", "TX", "78885"));

            _employeeRepo.Update(employee);
            await _unitOfWork.Commit();

            var result = (from item in employee.Addresses()
                          where item.Id.Equals(1)
                          select item).SingleOrDefault();

            Assert.Equal("123 Main Blvd", result.AddressDetails.AddressLine1);
            Assert.Equal("Anywhere", result.AddressDetails.City);
        }

        [Fact]
        public async Task ShouldDelete_EmployeeAddress_UsingRepo()
        {
            var employee = await _employeeRepo.GetByIdAsync(new Guid("9f7b902d-566c-4db6-b07b-716dd4e04340"));
            var address = (from item in employee.Addresses()
                           where item.Id.Equals(5)
                           select item).SingleOrDefault();

            employee.DeleteAddress(address.Id);
            _employeeRepo.Update(employee);
            await _unitOfWork.Commit();

            var result = (from item in employee.Addresses()
                          where item.Id.Equals(5)
                          select item).SingleOrDefault();

            Assert.Null(result);
        }

        [Fact]
        public async Task ShouldAdd_ContactPersonToEmployee_UsingRepo()
        {
            var employee = await _employeeRepo.GetByIdAsync(new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"));
            var originalContactCount = employee.ContactPersons().Count;

            var name = PersonName.Create("Fidel", "Castro", null);
            var phone = PhoneNumber.Create("555-555-5555");
            var notes = "You are being tested.";

            employee.AddContactPerson(0, name, phone, notes);
            _employeeRepo.Update(employee);
            await _unitOfWork.Commit();

            var count = employee.ContactPersons().Count;

            Assert.Equal(originalContactCount + 1, count);
        }

        [Fact]
        public async Task ShouldUpdate_EmployeeContactPerson_UsingRepo()
        {
            var employee = await _employeeRepo.GetByIdAsync(new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"));
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
            _employeeRepo.Update(employee);
            await _unitOfWork.Commit();

            var result = (from item in employee.ContactPersons()
                          where item.Id.Equals(2)
                          select item).SingleOrDefault();

            Assert.Equal("Bubba", result.ContactName.FirstName);
            Assert.Equal("987-965-1234", result.Telephone);
            Assert.Equal("You have been updated", result.Notes);
        }

        [Fact]
        public async Task ShouldDelete_EmployeeContactPerson_UsingRepo()
        {
            var employee = await _employeeRepo.GetByIdAsync(new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"));
            var contact = (from item in employee.ContactPersons()
                           where item.Id.Equals(2)
                           select item).SingleOrDefault();

            employee.DeleteContactPerson(contact.Id);
            _employeeRepo.Update(employee);
            await _unitOfWork.Commit();

            var result = (from item in employee.ContactPersons()
                          where item.Id.Equals(2)
                          select item).SingleOrDefault();

            Assert.Null(result);
        }
    }
}