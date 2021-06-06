using System;
using Microsoft.EntityFrameworkCore;
using Xunit;
using PipefittersSupply.Infrastructure.Application.Services;
using PipefittersSupply.Tests.TestData;
using PipefittersSupply.Infrastructure.Persistence;
using PipefittersSupply.Infrastructure.Persistence.Repositories;
using static PipefittersSupply.Infrastructure.Application.Commands.HumanResources.EmployeeCommand;

namespace PipefittersSupply.Tests.IntegrationTests.HumanResources
{
    public class EmployeeAppSvcTest : IntegrationTestBase
    {
        public EmployeeAppSvcTest() : base() { }

        [Fact]
        public async void ShouldCreate_Employee_From_CreateEmployeeCommand()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PipefittersSupplyDbContext>();

            optionsBuilder.UseSqlServer(
                connectionString,
                mySqlOptions => mySqlOptions.MigrationsAssembly(typeof(PipefittersSupplyDbContext).Assembly.FullName)
            )
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();

            using (var context = new PipefittersSupplyDbContext(optionsBuilder.Options))
            {
                var createEmployeeCmd = new V1.CreateEmployee
                {
                    Id = 25,
                    EmployeeTypeId = 2,
                    SupervisorId = 1,
                    LastName = "Cardin",
                    FirstName = "Bobby",
                    MiddleInitial = "J",
                    SSN = "358881236",
                    AddressLine1 = "361 HelloWorld Blvd",
                    AddressLine2 = "Apt 2B4",
                    City = "Houston",
                    StateProvinceCode = "TX",
                    Zipcode = "77777",
                    Telephone = "713-317-5421",
                    MaritalStatus = "S",
                    Exemptions = 1,
                    PayRate = 15.00M,
                    StartDate = new DateTime(2020, 6, 4),
                    IsActive = true
                };

                EmployeeAppicationService appSvc =
                    new EmployeeAppicationService
                    (
                        new StateProvinceCodeLookup(),
                        new EmployeeRepository(context),
                        new EfCoreUnitOfWork(context)
                    );

                await appSvc.Handle(createEmployeeCmd);

                var result = await context.Employees.FindAsync(createEmployeeCmd.Id) != null;

                Assert.True(result);
            }

        }

        [Fact]
        public async void ShouldUpdate_Employee_From_UpdateEmployeeCommand()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PipefittersSupplyDbContext>();

            optionsBuilder.UseSqlServer(
                connectionString,
                mySqlOptions => mySqlOptions.MigrationsAssembly(typeof(PipefittersSupplyDbContext).Assembly.FullName)
            )
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();

            using (var context = new PipefittersSupplyDbContext(optionsBuilder.Options))
            {
                var before = await context.Employees.FindAsync(4);

                Assert.Equal(2, before.EmployeeType);
                Assert.Equal("Goldberg", before.LastName);
                Assert.Equal("Jozef", before.FirstName);
                Assert.Equal("L", before.MiddleInitial);
                Assert.Equal("036889999", before.SSN);
                Assert.Equal("6667 Melody Lane", before.AddressLine1);
                Assert.Equal("A2", before.AddressLine2);
                Assert.Equal("Dallas", before.City);
                Assert.Equal("TX", before.State);
                Assert.Equal("75231", before.Zipcode);
                Assert.Equal("469-545-5874", before.Telephone);
                Assert.Equal("S", before.MaritalStatus);
                Assert.Equal(1, before.Exemptions);
                Assert.Equal(29.00M, before.PayRate);
                Assert.Equal(new DateTime(2013, 2, 28), before.StartDate);
                Assert.True(before.IsActive);

                var updateEmployeeCmd = new V1.UpdateEmployee
                {
                    Id = 4,
                    EmployeeTypeId = 3,
                    SupervisorId = 2,
                    LastName = "Brown",
                    FirstName = "Jamie",
                    MiddleInitial = "J",
                    SSN = "358881236",
                    AddressLine1 = "361 HelloWorld Blvd",
                    AddressLine2 = "Apt 2B4",
                    City = "Houston",
                    StateProvinceCode = "TX",
                    Zipcode = "77777",
                    Telephone = "713-317-5421",
                    MaritalStatus = "M",
                    Exemptions = 3,
                    PayRate = 15.25M,
                    StartDate = new DateTime(2019, 6, 4),
                    IsActive = false
                };

                EmployeeAppicationService appSvc =
                    new EmployeeAppicationService
                    (
                        new StateProvinceCodeLookup(),
                        new EmployeeRepository(context),
                        new EfCoreUnitOfWork(context)
                    );

                await appSvc.Handle(updateEmployeeCmd);

                var after = await context.Employees.FindAsync(4);

                Assert.Equal(3, after.EmployeeType);
                Assert.Equal("Brown", after.LastName);
                Assert.Equal("Jamie", after.FirstName);
                Assert.Equal("J", after.MiddleInitial);
                Assert.Equal("358881236", after.SSN);
                Assert.Equal("361 HelloWorld Blvd", after.AddressLine1);
                Assert.Equal("Apt 2B4", after.AddressLine2);
                Assert.Equal("Houston", after.City);
                Assert.Equal("77777", after.Zipcode);
                Assert.Equal("713-317-5421", after.Telephone);
                Assert.Equal("M", after.MaritalStatus);
                Assert.Equal(3, after.Exemptions);
                Assert.Equal(15.25M, after.PayRate);
                Assert.Equal(new DateTime(2019, 6, 4), after.StartDate);
                Assert.False(after.IsActive);
            }
        }
    }
}