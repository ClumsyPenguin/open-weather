using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Azure.Functions.Worker.OpenTelemetry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenWeather.Aspects.Resiliency;
using OpenWeather.Azure.Function.LoadOpenMeteoData;
using OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.Services;
using Sentry.Azure.Functions.Worker;
using Sentry.OpenTelemetry;
using OpenWeather.Core.Extensions;

var builder = FunctionsApplication.CreateBuilder(args);

builder.Services.AddTransient<DefaultStatusCodeHandler>();

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


builder.Services
    .AddOpenTelemetry()
    .UseFunctionsWorkerDefaults();

builder
    .ConfigureFunctionsWebApplication()
    .UseSentry(options =>
    {
        options.Dsn = "http://8e445484ee02564310f90ba8bf3300bb@100.107.209.63:9000/3";
        options.Debug = true;
        options.TracesSampleRate = 1.0;
        options.UseOpenTelemetry();
    });

var host = builder.Build();
host.Run();

static void ConfigureDependencies(ContainerBuilder builder)
{
    OpenWeather.Aspects.Config.DiConfig.Configure(builder);

    builder.RegisterTypeWithInterception<OpenMeteoClient, IOpenMeteoClient>(typeof(IResiliencyInterceptor));
    builder.RegisterType<OpenMeteoService>().As<IOpenMeteoService>();
    builder.RegisterType<DefaultStatusCodeHandler>().AsSelf();
}