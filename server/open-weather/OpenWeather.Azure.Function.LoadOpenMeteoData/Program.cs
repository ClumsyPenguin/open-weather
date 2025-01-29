using FluentValidation;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenWeather.Azure.Function.LoadOpenMeteoData;
using OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.Services;

var builder = FunctionsApplication.CreateBuilder(args);
builder.Services.AddTransient<DefaultStatusCodeHandler>();

builder.Services.AddHttpClient<IOpenMeteoService, OpenMeteoHttpClient>()
    .AddHttpMessageHandler<DefaultStatusCodeHandler>()
    .ConfigurePrimaryHttpMessageHandler(
        () => new SocketsHttpHandler
        { 
            PooledConnectionLifetime = TimeSpan.FromMinutes(15)
        })
    .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

builder.Services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);

builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();