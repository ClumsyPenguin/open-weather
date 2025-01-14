namespace OpenWeather.Domain.Temperature.Models;

public record GetCurrentTemperatureRequest
{
    public GetCurrentTemperatureRequest(double Longitude, double Latitude)
    {
        this.Longitude = Math.Round(Longitude, 2);
        this.Latitude = Math.Round(Latitude, 2);
    }

    public double Longitude { get; init; }
    public double Latitude { get; init; }
}