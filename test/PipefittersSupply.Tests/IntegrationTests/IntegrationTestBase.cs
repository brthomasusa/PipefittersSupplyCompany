using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using TestSupport.Helpers;
using TestSupport.EfHelpers;
using PipefittersSupply.Infrastructure.Persistence;

namespace PipefittersSupply.Tests.IntegrationTests
{
    public abstract class IntegrationTestBase
    {
        private const string _defaultConnectionString = "DefaultConnection";
        protected readonly string connectionString;

        public IntegrationTestBase()
        {
            var config = AppSettings.GetConfiguration();
            connectionString = config.GetConnectionString(_defaultConnectionString);

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
    }
}