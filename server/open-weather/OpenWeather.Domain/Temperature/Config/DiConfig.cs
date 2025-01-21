using Microsoft.Extensions.DependencyInjection;
using OpenWeather.Domain.Temperature.Services.Ports;
using OpenWeather.Domain.Temperature.Services;

namespace OpenWeather.Domain.Temperature.Config
{
    internal static class DiConfig
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITemperatureService, TemperatureService>();
        }
    }
}
