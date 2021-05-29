using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Raven.Client;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

using PipefittersSupply.Controllers;
using PipefittersSupply.AppServices;
using PipefittersSupply.Infrastructure.Repositories;
using PipefittersSupply.Domain.Lookup;
using PipefittersSupply.Domain.Repository;
using PipefittersSupply.Domain.HumanResources.Employees;
using PipefittersSupply.Domain.HumanResources.TimeCards;

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
            services.AddControllers();
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
                      FindIdentityProperty = m => m.Name == "_databaseId"
                  }
            };

            // store.Conventions.RegisterAsyncIdConvention<Employee>(
            //     (dbname, employee) =>
            //         Task.FromResult(string.Format("employees/{0}/{1}", employee.LastName, employee.FirstName)));

            // store.Conventions.RegisterAsyncIdConvention<TimeCard>(
            //     (dbname, timecard) =>
            //         Task.FromResult(string.Format("managers/{0}/{1}", "Hello", "World")));


            // store.Conventions.RegisterAsyncIdConvention<Employee>(
            //     (dbName, entity) => Task.FromResult("Employee/" + entity.Id.ToString()));
            // store.Conventions.RegisterAsyncIdConvention<TimeCard>(
            //     (dbName, entity) => Task.FromResult("TimeCard/" + entity.Id.ToString()));

            store.Initialize();

            services.AddTransient(c => store.OpenAsyncSession());
            services.AddSingleton<IStateProvinceLookup, StateProvinceCodeLookup>();
            services.AddSingleton<IEmployeeLookup, EmployeeLookup>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ITimeCardRepository, TimeCardRepository>();
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
