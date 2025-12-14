namespace ExpenseTracker.API.Extensions
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy("AdminOnly", policy =>
                {
                    policy.RequireRole("Admin");
                });

            return services;
        }
    }
}
