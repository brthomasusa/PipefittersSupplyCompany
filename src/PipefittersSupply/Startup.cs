using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Raven.Client.Documents;
using PipefittersSupply.AppServices;
using PipefittersSupply.Domain.Interfaces;
using PipefittersSupply.Domain.Lookup;
using PipefittersSupply.Domain.Repository;
using PipefittersSupply.Infrastructure;
using PipefittersSupply.Infrastructure.Persistence.Repositories;

namespace PipefittersSupply
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PipefittersSupplyDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    msSqlOptions => msSqlOptions.MigrationsAssembly(typeof(PipefittersSupplyDbContext).Assembly.FullName)
                )
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
            );

            services.AddControllers();
            // services.AddPersistence(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PipefittersSupply", Version = "v1" });
            });

            var store = new DocumentStore
            {
                Urls = new[] { "http://localhost:8080" },
                Database = "PipefittersSupply",
                Conventions =
                  {
                      FindIdentityProperty = m => m.Name == "DbId"
                  }
            };

            store.Initialize();

            services.AddTransient(c => store.OpenAsyncSession());
            services.AddSingleton<IStateProvinceLookup, StateProvinceCodeLookup>();
            services.AddSingleton<IEmployeeLookup, EmployeeLookup>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ITimeCardRepository, TimeCardRepository>();
            services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
            services.AddScoped<EmployeeAppicationService>();
            services.AddScoped<TimeCardApplicationService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PipefittersSupply v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
