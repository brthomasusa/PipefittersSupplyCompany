using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using PipefittersSupply.Tests.TestData;
using PipefittersSupply.Infrastructure;
using PipefittersSupply.Infrastructure.Repositories;
using PipefittersSupply.Domain.Common;
using PipefittersSupply.Domain.HumanResources.Employees;
using PipefittersSupply.Domain.Lookup;

namespace PipefittersSupply.Tests.IntegrationTests.HumanResources
{
    public class EmployeeRepositoryTest : IntegrationTestBase
    {
        private readonly IStateProvinceLookup _stateProvinceLookup = new MockStateProvinceCodeLookup();

        public EmployeeRepositoryTest() : base() { }

        [Fact]
        public async void ShouldReturn_True_Valid_EmployeeID()
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
                context.EmployeeTypes.AddRange(EmployeeTypes.GetEmployeeTypes());
                context.Employees.AddRange(Employees.GetEmployees());
                context.SaveChanges();

                EmployeeRepository repo = new EmployeeRepository(context);
                var result = await repo.Exists(new EmployeeId(1));

                Assert.True(result);
            }
        }

        [Fact]
        public async void ShouldReturn_False_Invalid_EmployeeID()
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
                context.EmployeeTypes.AddRange(EmployeeTypes.GetEmployeeTypes());
                context.Employees.AddRange(Employees.GetEmployees());
                context.SaveChanges();

                EmployeeRepository repo = new EmployeeRepository(context);
                var result = await repo.Exists(new EmployeeId(-1));

                Assert.False(result);
            }
        }

        [Fact]
        public async void ShouldReturn_Employee_Valid_EmployeeID()
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
                context.EmployeeTypes.AddRange(EmployeeTypes.GetEmployeeTypes());
                context.Employees.AddRange(Employees.GetEmployees());
                context.SaveChanges();

                EmployeeRepository repo = new EmployeeRepository(context);
                var result = await repo.Load(new EmployeeId(1));

                Assert.IsType<Employee>(result);

                Assert.Equal(EntityState.Unchanged, context.Entry(result).State);
            }
        }

        [Fact]
        public async void ShouldReturn_Null_Invalid_EmployeeID()
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
                context.EmployeeTypes.AddRange(EmployeeTypes.GetEmployeeTypes());
                context.Employees.AddRange(Employees.GetEmployees());
                context.SaveChanges();

                EmployeeRepository repo = new EmployeeRepository(context);
                var result = await repo.Load(new EmployeeId(-1));

                Assert.Null(result);
            }
        }

        [Fact]
        public async void ShouldShow_EmployeeEntityState_Added()
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
                context.EmployeeTypes.AddRange(EmployeeTypes.GetEmployeeTypes());
                context.Employees.AddRange(Employees.GetEmployees());
                context.SaveChanges();

                EmployeeRepository repo = new EmployeeRepository(context);
                var employee = GetEmployee();
                await repo.Add(employee);

                Assert.Equal(EntityState.Added, context.Entry(employee).State);
            }
        }

        [Fact]
        public async void ShouldAdd_Employee_ToEmployeeTable()
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
                context.EmployeeTypes.AddRange(EmployeeTypes.GetEmployeeTypes());
                context.Employees.AddRange(Employees.GetEmployees());
                context.SaveChanges();

                EmployeeRepository repo = new EmployeeRepository(context);
                EfCoreUnitOfWork unitOfWork = new EfCoreUnitOfWork(context);
                var employee = GetEmployee();

                await repo.Add(employee);

                Assert.Equal(EntityState.Added, context.Entry(employee).State);

                await unitOfWork.Commit();

                var result = await repo.Load(employee.Id);

                Assert.IsType<Employee>(employee);
            }
        }

        private Employee GetEmployee()
        {
            var eeID = new EmployeeId(16);
            var eeTypeID = EmployeeTypeIdentifier.FromInterger(3);
            var mgrID = new EmployeeId(1);
            var lname = EmployeeLastName.FromString("Pimp");
            var fname = EmployeeFirstName.FromString("Big");
            var mi = EmployeeMiddleInitial.FromString("P");
            var ssn = EmployeeSSN.FromString("587887964");
            var line1 = AddressLine1.FromString("123 Main Street");
            var line2 = AddressLine2.FromString("Apt 2");
            var city = City.FromString("Somewhere");
            var stateCode = StateProvinceCode.FromString("TX", _stateProvinceLookup);
            var zipcode = Zipcode.FromString("75654");
            var phone = Telephone.FromString("555-555-5555");
            var maritalStatus = MaritalStatus.FromString("M");
            var exempt = TaxExemption.FromInterger(5);
            var payRate = EmployeePayRate.FromDecimal(25.00M);
            var startDate = EmployeeStartDate.FromDateTime(new DateTime(2018, 6, 17));
            var isActive = IsActive.FromBoolean(true);

            return new Employee
                (
                    eeID, eeTypeID, mgrID, lname, fname, mi, ssn, line1, line2, city, stateCode,
                    zipcode, phone, maritalStatus, exempt, payRate, startDate, isActive
                );
        }
    }
}