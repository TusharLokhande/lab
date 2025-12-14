using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Extensions
{
    public static class JwtAuthExtensions
    {
        public static IServiceCollection AddJwtAuth(
          this IServiceCollection services, IConfiguration configuration)
        {
            // JWT Authentication Configuration can be added here in the future

            var azureAd = configuration.GetSection("AzureAd");
            var tenantId = azureAd["TenantId"];
            var clientId = azureAd["ClientId"];
            var instance = azureAd["Instance"];


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"{instance}{tenantId}/v2.0";
                    options.Audience = $"api://{clientId}";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudiences = new[]
                        {
                            $"api://{clientId}",  // Recommended audience format
                            clientId              // Raw clientId fallback
                        },
                        ValidateIssuer = true
                    };
                });

            return services;
        }
    }
}
