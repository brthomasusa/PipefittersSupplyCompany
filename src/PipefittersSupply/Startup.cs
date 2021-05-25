using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Raven.Client;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Swashbuckle.AspNetCore.Swagger;

using PipefittersSupply.Api;
using PipefittersSupply.AppServices;
using PipefittersSupply.Repositories;
using PipefittersSupply.Domain.Lookup;
using PipefittersSupply.Domain.Repository;
using PipefittersSupply.Domain.HumanResources.Employees;
using PipefittersSupply.Domain.HumanResources.TimeCards;

namespace PipefittersSupply
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var store = new DocumentStore
            {
                Urls = new[] { "http://localhost:8080" },
                Database = "PipefittersSupply",
                Conventions =
                  {
                      FindIdentityProperty = m => m.Name == "_databaseId"
                  }
            };
            store.Conventions.RegisterAsyncIdConvention<Employee>(
                (dbName, entity) => Task.FromResult("Employee/" + entity.Id.ToString()));
            store.Initialize();

            services.AddTransient(c => store.OpenAsyncSession());
            services.AddSingleton<IStateProvinceLookup, StateProvinceCodeLookup>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ITimeCardRepository, TimeCardRepository>();
            services.AddSingleton<EmployeeAppicationService>();
            // services.AddSingleton<TimeCardAppicationService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"{0}/PipefittersSupply.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PipefittersSupply",
                });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PipefittersSupply v1"));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}