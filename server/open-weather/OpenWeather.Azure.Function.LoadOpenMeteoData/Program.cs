using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenWeather.Aspects.Resiliency;
using OpenWeather.Azure.Function.LoadOpenMeteoData;
using OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.Services;
using OpenWeather.Core.Extensions;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureContainer(new AutofacServiceProviderFactory(ConfigureDependencies));

/*
 <IOpenMeteoClient, OpenMeteoClient>()
    .AddHttpMessageHandler<DefaultStatusCodeHandler>()
    .ConfigurePrimaryHttpMessageHandler(
        () => new SocketsHttpHandler
        { 
            PooledConnectionLifetime = TimeSpan.FromMinutes(15)
        })
    .SetHandlerLifetime(Timeout.InfiniteTimeSpan)
 */



builder.Services.AddHttpClient();
builder.Services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);
builder.Services.ConfigureResiliency();


builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

var host = builder.Build();
host.Run();

static void ConfigureDependencies(ContainerBuilder builder)
{
    OpenWeather.Aspects.Config.DiConfig.Configure(builder);

    builder.RegisterTypeWithInterception<OpenMeteoClient, IOpenMeteoClient>(typeof(IResiliencyInterceptor));
    builder.RegisterType<OpenMeteoService>().As<IOpenMeteoService>();
    builder.RegisterType<DefaultStatusCodeHandler>().AsSelf();
}