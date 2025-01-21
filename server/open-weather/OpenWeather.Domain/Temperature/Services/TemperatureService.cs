using OpenWeather.Domain.Temperature.Models;
using OpenWeather.Domain.Temperature.Services.Ports;

namespace OpenWeather.Domain.Temperature.Services;

internal class TemperatureService() : ITemperatureService
{//KMI updates weather data at intervals of 1 hour
    public Task<double> GetCurrentTemperatureAsync(GetCurrentTemperatureRequest request, CancellationToken cancellationToken)
    {

        return Task.FromResult(21d);
    }
}