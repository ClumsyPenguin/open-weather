using Autofac;
using Autofac.Extensions.DependencyInjection;
using OpenWeather.Adapters.Postgres.Config;
using OpenWeather.Adapters.REST.Configuration;
using OpenWeather.Adapters.REST.Temperature;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

ConfigureCrossCuttingInfra(builder);
ConfigureDatabases(builder);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(ConfigureDependencies));

var app = BuildApp(builder);
app.Run();
return;

static void ConfigureDependencies(ContainerBuilder builder)
{
    OpenWeather.Domain.Config.DiConfig.Configure(builder);
    OpenWeather.Aspects.Config.DiConfig.Configure(builder);
    OpenWeather.Adapters.Postgres.Config.DiConfig.Configure(builder);
}

static void ConfigureDatabases(WebApplicationBuilder builder)
{
    DbContextConfig.Configure(builder.Services, builder.Configuration);
}

static void ConfigureCrossCuttingInfra(WebApplicationBuilder builder)
{
    builder.Services.AddOpenApi();
    builder.Services.ConfigureCors();
    builder.Services.ConfigureCaching(builder.Configuration);
    builder.Services.ConfigureHttpClients();
}

static WebApplication BuildApp(WebApplicationBuilder builder)
{
    var app = builder.Build();

    app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("OpenWeather API")
            .WithDarkModeToggle(true)
            .WithDefaultHttpClient(ScalarTarget.Http, ScalarClient.HttpClient)
            .WithTheme(ScalarTheme.Moon);
    });
}

    app.UseHttpsRedirection();
    app.MapTemperatureEndpoints();
    app.UseCors(Cors.AllowAllPolicy);
    app.CreateDbIfNotExists();
    
    return app;
}