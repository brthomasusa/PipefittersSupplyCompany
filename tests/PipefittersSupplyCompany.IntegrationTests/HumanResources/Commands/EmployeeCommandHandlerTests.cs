using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Core.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Persistence;
using PipefittersSupplyCompany.Infrastructure.Persistence.Repositories.HumanResources;
using PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;
using PipefittersSupplyCompany.IntegrationTests.Base;
using static PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources.EmployeeAggregateCommand;

namespace PipefittersSupplyCompany.IntegrationTests.HumanResources.Commands
{
    public class EmployeeCommandHandlerTests : IntegrationTestBaseEfCore
    {
        private readonly ICommandHandler _employeeCmdHdlr;

        public EmployeeCommandHandlerTests()
        {
            TestDataInitialization.InitializeData(_dbContext);
            IUnitOfWork unitOfWork = new AppUnitOfWork(_dbContext);
            IEmployeeAggregateRepository employeeRepo = new EmployeeAggregateRepository(_dbContext);

            _employeeCmdHdlr = new EmployeeAggregateCommandHandler(employeeRepo, unitOfWork);
        }

        [Fact]
        public async Task ShouldInsert_Employee_UsingCreateEmployeeInfoCommand()
        {
            Guid id = Guid.NewGuid();
            var command = new V1.CreateEmployeeInfo
            {
                Id = id,
                SupervisorId = id,
                LastName = "Hello",
                FirstName = "World",
                MiddleInitial = "Z",
                SSN = "523789999",
                Telephone = "214-654-9874",
                MaritalStatus = "S",
                Exemptions = 2,
                PayRate = 25.00M,
                StartDate = new DateTime(2021, 8, 29),
                IsActive = true
            };

            await _employeeCmdHdlr.Handle(command);

            Employee result = await _dbContext.Employees.FindAsync(id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldUpdate_Employee_UsingEditEmployeeInfoCommand()
        {
            var command = new V1.EditEmployeeInfo
            {
                Id = new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"),
                SupervisorId = new Guid("4b900a74-e2d9-4837-b9a4-9e828752716e"),
                LastName = "Hello",
                FirstName = "World",
                MiddleInitial = "Z",
                SSN = "523019999",
                Telephone = "214-654-9874",
                MaritalStatus = "S",
                Exemptions = 2,
                PayRate = 25.00M,
                StartDate = new DateTime(2021, 8, 29),
                IsActive = true
            };

            await _employeeCmdHdlr.Handle(command);

            Employee result = await _dbContext.Employees.FindAsync(command.Id);
            Assert.Equal(command.LastName, result.EmployeeName.LastName);
            Assert.Equal(command.FirstName, result.EmployeeName.FirstName);
            Assert.Equal(command.SSN, result.SSN);
        }

        [Fact]
        public async Task ShouldDelete_Employee_UsingDeleteEmployeeInfoCommand()
        {
            Employee employee = await _dbContext.Employees.FindAsync(new Guid("e6b86ea3-6479-48a2-b8d4-54bd6cbbdbc5"));
            Assert.NotNull(employee);

            var command = new V1.DeleteEmployeeInfo
            {
                Id = employee.Id,
            };

            await _employeeCmdHdlr.Handle(command);

            Employee result = await _dbContext.Employees.FindAsync(command.Id);
            Assert.Null(result);
        }
    }
}