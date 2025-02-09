using Autofac;
using OpenWeather.Azure.Function.LoadOpenMeteoData.Temperature.Config;

namespace OpenWeather.Azure.Function.LoadOpenMeteoData.Config;

public static class DiConfig
{
    public static void Configure(ContainerBuilder builder)
    {
        builder.RegisterModule(new TemperatureModule());
    }
}