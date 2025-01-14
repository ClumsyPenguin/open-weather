using OpenWeather.Domain.Temperature.Models;

namespace OpenWeather.Domain.Temperature.Services.Ports;

public interface ITemperatureService
{
    public Task<double> GetCurrentTemperatureAsync(GetCurrentTemperatureRequest request, CancellationToken cancellationToken);
}