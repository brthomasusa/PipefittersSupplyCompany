using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using PipefittersSupplyCompany.WebApi.Models;

namespace PipefittersSupplyCompany.WebApi.Exceptions.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {

                await _next(httpContext);

            }
            catch (SqlException sqlEx)
            {
                _logger.LogError($"An ADO exception has been thrown: {sqlEx}");
                await HandleExceptionAsync(httpContext, sqlEx);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception has been thrown: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var message = exception switch
            {
                SqlException => "An exception in the Dapper ORM has occured.",
                _ => "Internal server error (from custom middleware)."
            };

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}