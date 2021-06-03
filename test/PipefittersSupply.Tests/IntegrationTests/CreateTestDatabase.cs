using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Extensions.AssertExtensions;
using TestSupport.Helpers;
using TestSupport.EfHelpers;
using PipefittersSupply.Tests.TestData;
using PipefittersSupply.Infrastructure;

namespace PipefittersSupply.Tests.IntegrationTests
{
    public class CreateTestDatabase
    {
        private const string _connectionString = "DefaultConnection";

        public CreateTestDatabase()
        {
            var config = AppSettings.GetConfiguration();
            var connectionString = config.GetConnectionString(_connectionString);

            var optionsBuilder = new DbContextOptionsBuilder<PipefittersSupplyDbContext>();

            optionsBuilder.UseSqlServer(
                connectionString,
                mySqlOptions => mySqlOptions.MigrationsAssembly(typeof(PipefittersSupplyDbContext).Assembly.FullName)
            )
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();

            using (var context = new PipefittersSupplyDbContext(optionsBuilder.Options))
            {
                context.Database.EnsureClean();
            }
        }

        [Fact]
        public void ShouldCreateNewDbForIntegrationTesting()
        {
            var config = AppSettings.GetConfiguration();
            var connectionString = config.GetConnectionString(_connectionString);

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

                var numberOfEmployeeTypes = context.EmployeeTypes.ToList().Count;
                var numberOfEmployees = context.Employees.ToList().Count;
                numberOfEmployeeTypes.ShouldEqual(6);
                numberOfEmployees.ShouldEqual(4);
            }
        }
    }
}