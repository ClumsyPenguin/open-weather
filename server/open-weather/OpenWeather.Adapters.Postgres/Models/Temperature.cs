namespace OpenWeather.Adapters.Postgres.Models;

public class Temperature
{
    public int TemperatureId { get; init; }
    public double TemperatureC { get; init; }
    public double Longitude { get; init; }
    public double Latitude { get; init; }
    
    public DateTime TimeStamp { get; init; }
}