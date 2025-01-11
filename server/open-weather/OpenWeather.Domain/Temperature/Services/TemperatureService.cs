using OpenWeather.Domain.Temperature.Models;
using OpenWeather.Domain.Temperature.Services.Ports;

namespace OpenWeather.Domain.Temperature.Services;

public class TemperatureService : ITemperatureService
{
    public Task<double> GetCurrentTemperature(GetCurrentTemperatureRequest request)
    {
        return Task.FromResult(1d);
    }
}