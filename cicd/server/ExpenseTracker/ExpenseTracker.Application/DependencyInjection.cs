using ExpenseTracker.Application.Features.Auth.Register;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Interfaces.Services;
using ExpenseTracker.Application.Mappings;
using ExpenseTracker.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace ExpenseTracker.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
          this IServiceCollection services)
        {

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DefaultMapper>();
                cfg.AddProfile<RegisterMapping>();
            });

            services.AddValidatorsFromAssembly(typeof(Validator).Assembly);


            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
