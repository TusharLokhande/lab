using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace ExpenseTracker.API.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly TelemetryClient _telemetryClient;

        public GlobalExceptionMiddleware(
            RequestDelegate next, 
            ILogger<GlobalExceptionMiddleware> logger,
            TelemetryClient telemetryClient
        )
        {
            _next = next;
            _logger = logger;
            _telemetryClient = telemetryClient;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex) 
            {

                _logger.LogError(ex, "Unhandled exception occurred.");

                _telemetryClient.TrackException(ex, new Dictionary<string, string>
                {
                    { "HttpMethod", httpContext.Request.Method },
                    { "Path", httpContext.Request.Path },
                    { "UserAgent", httpContext.Request.Headers["User-Agent"].ToString() }
                });  

                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var  errorResponse = new { message = ex.Message, stackTrace = ex.StackTrace };
                await httpContext.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
