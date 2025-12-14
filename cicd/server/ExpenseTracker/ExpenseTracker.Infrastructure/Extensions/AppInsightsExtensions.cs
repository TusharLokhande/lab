using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Extensions
{
    public static class AppInsightsExtensions
    {
        public static IServiceCollection AddAppInsights(this IServiceCollection services, IConfiguration config)
        {


            services.AddSingleton(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration["ApplicationInsights:ConnectionString"]
                    ?? throw new InvalidOperationException("Application Insights connection string not found.");

                var telemetryConfig = new TelemetryConfiguration
                {
                    ConnectionString = connectionString
                };

                return new TelemetryClient(telemetryConfig);
            });

            return services;
        }


        public static ILoggingBuilder AddApplicationInsightsLogging(
           this ILoggingBuilder logging,
           IConfiguration configuration
        )
        {
            logging.AddApplicationInsights(
                configureTelemetryConfiguration: (config) =>
                    config.ConnectionString = configuration["ApplicationInsights:ConnectionString"],
                configureApplicationInsightsLoggerOptions: options =>
                {
                    options.IncludeScopes = true;
                });

            return logging;
        }
    }
}
