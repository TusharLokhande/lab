using ExpenseTracker.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.RateLimiting;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Extensions
{
    public static class RateLimitExtension
    {
        public static IServiceCollection AddRateLimiter(
            this IServiceCollection services
        )
        {

            // RATE LIMITTER
            services.AddRateLimiter(options =>
            {
                //  IP-based global limit
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                {
                    var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                    return RateLimitPartition.GetFixedWindowLimiter(ip, _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 5000,               // 100 requests
                        Window = TimeSpan.FromMinutes(1), // per minute per IP
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0
                    });
                });

                // Controller-based limit (named policy)
                options.AddFixedWindowLimiter("RATE_LIMIT", o =>
                {
                    o.PermitLimit = 1000;               // 20 requests
                    o.Window = TimeSpan.FromSeconds(30); // per 30 sec per client
                    o.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    o.QueueLimit = 0;
                });
            });


            return services;
        }
    }
}
