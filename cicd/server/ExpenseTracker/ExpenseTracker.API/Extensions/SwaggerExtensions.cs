using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore;

namespace ExpenseTracker.API.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(option =>
            {
                option.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                var provider = services.BuildServiceProvider()
                                       .GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var desc in provider.ApiVersionDescriptions)
                {
                    option.SwaggerDoc(desc.GroupName, new OpenApiInfo
                    {
                        Title = $"ExpenseTracker API {desc.ApiVersion}",
                        Version = desc.GroupName
                    });
                }
            });

            return services;
        }
    }
}
