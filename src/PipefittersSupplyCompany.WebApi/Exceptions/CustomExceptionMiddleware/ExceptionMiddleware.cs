using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using PipefittersSupplyCompany.WebApi.Models;
using PipefittersSupplyCompany.Infrastructure.Interfaces;

namespace PipefittersSupplyCompany.WebApi.Exceptions.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ILoggerManager logger)
        {
            try
            {

                await _next(httpContext);

            }
            catch (SqlException sqlEx)
            {
                logger.LogError($"An ADO exception has been thrown: {sqlEx}");
                await HandleExceptionAsync(httpContext, sqlEx);
            }
            catch (Exception ex)
            {
                logger.LogError($"An exception has been thrown: {ex}");
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
                Exception => exception.Message,
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