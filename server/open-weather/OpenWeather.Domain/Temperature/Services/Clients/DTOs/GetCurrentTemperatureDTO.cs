using System.Text.Json.Serialization;

namespace OpenWeather.Domain.Temperature.Services.Clients.DTOs;

// ReSharper disable once InconsistentNaming
internal class GetCurrentTemperatureDTO
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

internal class Current
{
    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("interval")]
    public int Interval { get; set; }

    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }
}

internal class CurrentUnits
{
    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("interval")]
    public string Interval { get; set; }

    [JsonPropertyName("temperature")]
    public string Temperature { get; set; }
}
