using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PipefittersSupply.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PipefittersSupplyDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    msSqlOptions => msSqlOptions.MigrationsAssembly(typeof(PipefittersSupplyDbContext).Assembly.FullName)
                )
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
            );

            services.AddScoped<PipefittersSupplyDbContext>(provider => provider.GetService<PipefittersSupplyDbContext>());
        }
    }
}