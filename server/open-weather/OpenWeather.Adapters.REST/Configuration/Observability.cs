using OpenTelemetry.Trace;
using Sentry.OpenTelemetry;

namespace OpenWeather.Adapters.REST.Configuration;

public static class Observability
{
    public static IServiceCollection ConfigureOpenTelemetry(this IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .WithTracing(tracingBuilder =>
            {
                tracingBuilder
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRedisInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddSentry();
            })
            .WithLogging(loggingBuilder =>
            {
                
            })
            .WithMetrics(metricsBuilder =>
            {
                
            });
        
        return services;
    }

    public static void ConfigureSentry(this ConfigureWebHostBuilder webHostBuilder)
    {
        webHostBuilder.UseSentry(options =>
        {
            options.TracesSampleRate = 1.0;
            options.UseOpenTelemetry();
        });
    }
}