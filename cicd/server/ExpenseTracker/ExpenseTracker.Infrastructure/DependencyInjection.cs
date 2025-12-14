using ExpenseTracker.Application.Interfaces.External;
using ExpenseTracker.Application.Interfaces.Persistence;
using ExpenseTracker.Application.Interfaces.Repository;
using ExpenseTracker.Infrastructure.Email.Providers;
using ExpenseTracker.Infrastructure.Email.Templates;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Infrastructure.Persistence;
using ExpenseTracker.Infrastructure.Persistence.Context;
using ExpenseTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
           this IServiceCollection services, IConfiguration configuration)
        {

            services
                .AddJwtAuth(configuration) // JWT AUTH
                .AddRateLimiter() // RATE LIMITER
                .AddAppInsights(configuration); // APPLICATION INSIGHTS

            services.AddDbContext<AppDbContext>(options =>
              options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));



            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleMappingRepository, UserRoleMappingRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthProviderRepository, AuthProviderRepository>();

            services.AddScoped<IEmailService, SmtpEmailService>();
            services.AddScoped<IEmailTemplateProvider, EmailTemplateProvider>();
            

            return services;
        }
    }
}
