using System;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Moq;
using TestSupport.Helpers;
using PipefittersSupplyCompany.Infrastructure.Persistence;

namespace PipefittersSupplyCompany.IntegrationTests.Base
{
    public abstract class IntegrationTestBase : IDisposable
    {
        private const string _defaultConnectionString = "DefaultConnection";
        private readonly string _connectionString;
        protected readonly AppDbContext _dbContext;

        public IntegrationTestBase()
        {
            var config = AppSettings.GetConfiguration();
            _connectionString = config.GetConnectionString(_defaultConnectionString);

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer(
                _connectionString,
                mySqlOptions => mySqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
            )
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();

            var mockMediator = new Mock<IMediator>();
            _dbContext = new AppDbContext(optionsBuilder.Options, mockMediator.Object);
            TestDataInitialization.InitializeData(_dbContext);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}