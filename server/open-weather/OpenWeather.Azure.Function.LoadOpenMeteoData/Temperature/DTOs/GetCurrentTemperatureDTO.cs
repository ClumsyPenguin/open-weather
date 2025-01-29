using System.Text.Json.Serialization;

namespace OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.DTOs;

// ReSharper disable once InconsistentNaming
public class GetCurrentTemperatureDTO
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("generationtime_ms")]
    public double GenerationtimeMs { get; set; }

    [JsonPropertyName("utc_offset_seconds")]
    public int UtcOffsetSeconds { get; set; }

    [JsonPropertyName("timezone")]
    public string Timezone { get; set; }

    [JsonPropertyName("timezone_abbreviation")]
    public string TimezoneAbbreviation { get; set; }

    [JsonPropertyName("elevation")]
    public double Elevation { get; set; }

    [JsonPropertyName("current_units")]
    public CurrentUnits CurrentUnits { get; set; }

    [JsonPropertyName("current")]
    public Current Current { get; set; }
}

public class Current
{
    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("interval")]
    public int Interval { get; set; }

    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }
}

public class CurrentUnits
{
    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("interval")]
    public string Interval { get; set; }

    [JsonPropertyName("temperature")]
    public string Temperature { get; set; }
}
