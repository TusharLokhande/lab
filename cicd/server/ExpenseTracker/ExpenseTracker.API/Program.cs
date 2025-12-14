using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using ExpenseTracker.API.Extensions;
using ExpenseTracker.API.Middlewares;
using ExpenseTracker.Application;
using ExpenseTracker.Infrastructure;
using ExpenseTracker.Infrastructure.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCorsPolicy(builder.Configuration)
    .AddAuthorizationPolicies()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddApiVersioningConfiguration();


builder.Logging.AddApplicationInsightsLogging(builder.Configuration);

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters
        .Add(new JsonStringEnumConverter());
});

builder.Services.AddSwaggerDocumentation(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseGlobalExceptionMiddleware();

app.UseRouting();


app.UseCors("DefaultCorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();


// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant()
            );
        }
        options.RoutePrefix = "swagger";
    });
// }


app.MapControllers();
app.Run();

