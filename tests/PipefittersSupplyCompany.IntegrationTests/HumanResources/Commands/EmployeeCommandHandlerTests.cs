using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
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
    public class EmployeeCommandHandlerTests : IntegrationTestBase
    {
        private readonly ICommandHandler _employeeCmdHdlr;

        public EmployeeCommandHandlerTests()
        {
            IUnitOfWork unitOfWork = new AppUnitOfWork(_dbContext);
            IEmployeeAggregateRepository employeeRepo = new EmployeeAggregateRepository(_dbContext);

            _employeeCmdHdlr = new EmployeeAggregateCommandHandler(employeeRepo, unitOfWork);
        }

        [Fact]
        public async Task ShouldInsert_Employee_UsingCreateEmployeeCommand()
        {
            Guid id = Guid.NewGuid();
            var command = new V1.CreateEmployee
            {
                Id = id,
                SupervisorId = id,
                LastName = "Hello",
                FirstName = "World",
                MiddleInitial = "Z",
                SSN = "523789999",
                AddressLine1 = "555 Fifth Street",
                AddressLine2 = "Apt 555",
                City = "Richardson",
                StateCode = "TX",
                Zipcode = "75213",
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
    }
}