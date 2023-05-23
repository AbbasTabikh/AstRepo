using Demo.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Demo.Api.ExceptionMiddleware
{
    public class GlobalExceptionMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public GlobalExceptionMiddleware(RequestDelegate next , ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }



        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError($"An error occured : {exception.Message}");
                await HandleExceptionAsync(context, exception);
            }
        }


        private static async Task HandleExceptionAsync(HttpContext httpContext , Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;


            var message = exception switch
            {
                DbUpdateException => "An Error Occured While Accessing the DataBase",
                Exception => "Internal Server Error"
            };

            await httpContext.Response.WriteAsJsonAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            });


        }

    }
}
