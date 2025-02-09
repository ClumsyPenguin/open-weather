using OpenWeather.Domain.Temperature.Ports;

namespace OpenWeather.Adapters.REST.Configuration;

public static class HttpClients
{
    public static IServiceCollection ConfigureHttpClients(this IServiceCollection services)
    {
        return services;
    }
}