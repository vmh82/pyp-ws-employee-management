using EmployeeManagement.Infraestructure.Exceptions;
using EmployeeManagement.Infraestructure.Services;
using System;
using System.Net;
using System.Text.Json;

namespace EmployeeManagement.API.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger )
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
                context.Response.Headers.Add("Content-Type", "application/json");
                switch (exception)
                {
                    case EmployeeAlreadyExistsException:
                        _logger.LogWarning(exception, exception.Message);
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case EmployeeNotFoundException:
                        _logger.LogWarning(exception, exception.Message);
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        _logger.LogError(exception, exception.Message);
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                string message = context.Response.StatusCode != 500 ? exception.Message : "Unexpected error while processing the request.";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    code = context.Response.StatusCode,
                    message = message
                }));
                await context.Response.CompleteAsync();
            }
        }
    }
}
