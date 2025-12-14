using Asp.Versioning;

namespace ExpenseTracker.API.Extensions
{
    public static class ApiVersioningExtensions
    {
        public static IServiceCollection AddApiVersioningConfiguration(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                            options.DefaultApiVersion = new ApiVersion(1, 0);
                            options.AssumeDefaultVersionWhenUnspecified = true;
                            options.ReportApiVersions = true;
                        })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV"; // v1.0
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}
