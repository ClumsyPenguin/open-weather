namespace OpenWeather.Domain.Temperature.Models;

public class GetCurrentTemperatureRequest
{
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}