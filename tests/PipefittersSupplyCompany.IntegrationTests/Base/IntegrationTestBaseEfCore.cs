using System;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Moq;
using TestSupport.Helpers;
using PipefittersSupplyCompany.Infrastructure.Persistence;

namespace PipefittersSupplyCompany.IntegrationTests.Base
{
    public abstract class IntegrationTestBaseEfCore : IDisposable
    {
        private const string _defaultConnectionString = "DefaultConnection";
        private readonly string _connectionString;
        protected readonly AppDbContext _dbContext;
        protected readonly string serviceAddress = "https://localhost:5001/";

        public IntegrationTestBaseEfCore()
        {
            var config = AppSettings.GetConfiguration();
            _connectionString = config.GetConnectionString(_defaultConnectionString);

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseSqlServer(
                _connectionString,
                mySqlOptions => mySqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
            )
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
            .UseLazyLoadingProxies();

            _dbContext = new AppDbContext(optionsBuilder.Options);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}