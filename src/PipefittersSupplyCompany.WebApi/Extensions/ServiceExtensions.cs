using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using PipefittersSupplyCompany.Infrastructure;
using PipefittersSupplyCompany.Infrastructure.Interfaces;

using PipefittersSupplyCompany.Infrastructure.Persistence;
using PipefittersSupplyCompany.Infrastructure.Persistence.Repositories.HumanResources;
using PipefittersSupplyCompany.WebApi.Interfaces;
using PipefittersSupplyCompany.WebApi.Controllers.Financing.Financiers;
using PipefittersSupplyCompany.Infrastructure.Application.Services.HumanResources;
using PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources;
using PipefittersSupplyCompany.Infrastructure.Persistence.Repositories.Financing;
using PipefittersSupplyCompany.Infrastructure.Application.Commands.Financing;
using PipefittersSupplyCompany.Infrastructure.Application.Services.Financing;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;

namespace Microsoft.Extensions.DependencyInjection //PipefittersSupplyCompany.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

        public static IServiceCollection ConfigureLoggingService(this IServiceCollection services) =>
            services.AddScoped<ILoggerManager, LoggerManager>();

        public static IServiceCollection AddCustomMediaTypes(this IServiceCollection services)
        {
            return services.Configure<MvcOptions>(config =>
            {
                var newtonsoftJsonOutputFormatter = config.OutputFormatters
                      .OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();

                if (newtonsoftJsonOutputFormatter != null)
                {
                    newtonsoftJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.btechnical-consulting.hateoas+json");
                }

            });
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            return services
                    .AddScoped<IUnitOfWork, AppUnitOfWork>()
                    .AddScoped<IEmployeeAggregateRepository, EmployeeAggregateRepository>()
                    .AddScoped<EmployeeAggregateCommandHandler>()
                    .AddScoped<IEmployeeQueryService, EmployeeQueryService>()
                    .AddScoped<IFinancierAggregateRepository, FinancierAggregateRepository>()
                    .AddScoped<FinancierAggregateCommandHandler>()
                    .AddScoped<IFinancierQueryService, FinancierQueryService>()
                    .AddScoped<IFinancierQueryRequestHandler, FinancierQueryRequestHander>();



            // Singleton and scoped example of injecting generic class. DO NOT DELETE!!!
            // .AddSingleton(typeof(IQueryRequestHandler<>), typeof(FinancierAggregateQueryRequestHander<>));
            // .AddScoped(typeof(IQueryRequestHandler<>), typeof(FinancierQueryRequestHander<>))
        }

    }
}