namespace OpenWeather.Domain.Temperature.Models;

public class CurrentTemperature
{
    public double TemperatureC { get; init; }
    public double Longitude { get; init; }
    public double Latitude { get; init; }

    public DateTime TimeStamp { get; init; }
}