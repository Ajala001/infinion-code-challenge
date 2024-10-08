namespace App.infrastructure.Middlewares
{
    using App.Domain.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System;
    using System.Net;
    using System.Threading.Tasks;

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Determine status code and response message based on exception type
            var statusCode = HttpStatusCode.InternalServerError; // Default to 500
            string message = "An unexpected error occurred.";

            switch (exception)
            {
                case ArgumentNullException _:
                    statusCode = HttpStatusCode.BadRequest; // 400
                    message = "One or more arguments are null.";
                    break;

                case UnauthorizedAccessException _:
                    statusCode = HttpStatusCode.Unauthorized; // 401
                    message = "Unauthorized access.";
                    break;

                case EmailSmtpException smtpEx:
                    statusCode = HttpStatusCode.ServiceUnavailable; // 503
                    message = smtpEx.Message; // SMTP error message
                    break;

                    // Add more exceptions as needed
            }

            context.Response.StatusCode = (int)statusCode;

            var errorDetails = new
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
                Detailed = exception.Message // You can expose this for detailed error info
            };

            var result = JsonConvert.SerializeObject(errorDetails); // Convert error details to JSON
            return context.Response.WriteAsync(result); // Write response
        }
    }


}
