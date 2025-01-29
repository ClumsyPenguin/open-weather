using OpenWeather.Domain.Temperature.Services.Clients;

namespace OpenWeather.Adapters.REST.Configuration;

public static class HttpClients
{
    public static IServiceCollection ConfigureHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient<IAzureFunctionService, AzureFunctionHttpClient>();
        
        return services;
    }
}