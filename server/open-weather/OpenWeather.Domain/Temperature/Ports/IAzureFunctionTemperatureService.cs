using OpenWeather.Shared.Temperature.Models;

namespace OpenWeather.Domain.Temperature.Ports;

public interface IAzureFunctionTemperatureService
{
    public Task<CurrentTemperature> GetCurrentTemperatureAsync(double latitude, double longitude);
}