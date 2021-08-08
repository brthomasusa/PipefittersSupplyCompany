using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Moq;
using TestSupport.Helpers;
using PipefittersSupplyCompany.Infrastructure.Data;

namespace PipefittersSupplyCompany.IntegrationTests.Base
{
    public abstract class IntegrationTestBase
    {
        private const string _defaultConnectionString = "DefaultConnection";
        protected readonly string connectionString;

        public IntegrationTestBase()
        {
            var config = AppSettings.GetConfiguration();
            connectionString = config.GetConnectionString(_defaultConnectionString);

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer(
                connectionString,
                mySqlOptions => mySqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
            )
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();

            var mockMediator = new Mock<IMediator>();

            using (var context = new AppDbContext(optionsBuilder.Options, mockMediator.Object))
            {
                TestDataInitialization.InitializeData(context);
            }
        }
    }
}