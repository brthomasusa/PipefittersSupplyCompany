using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Extensions.AssertExtensions;
using PipefittersSupply.Tests.TestData;
using PipefittersSupply.Infrastructure;

namespace PipefittersSupply.Tests.IntegrationTests
{
    public class CreateTestDatabase : IntegrationTestBase
    {
        public CreateTestDatabase() : base() { }

        [Fact]
        public void ShouldCreateNewDbForIntegrationTesting()
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

                var numberOfEmployeeTypes = context.EmployeeTypes.ToList().Count;
                var numberOfEmployees = context.Employees.ToList().Count;
                numberOfEmployeeTypes.ShouldEqual(6);
                numberOfEmployees.ShouldEqual(4);
            }
        }
    }
}