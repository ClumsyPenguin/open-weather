namespace OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.DTOs;

public record GetCurrentTemperatureRequest
{
    public GetCurrentTemperatureRequest(double Longitude, double Latitude)
    {
        this.Longitude = Math.Round(Longitude, 2);
        this.Latitude = Math.Round(Latitude, 2);
    }

    public double Longitude { get; }
    public double Latitude { get; }
}