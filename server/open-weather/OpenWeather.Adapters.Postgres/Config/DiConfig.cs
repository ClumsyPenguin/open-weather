using Autofac;
using OpenWeather.Adapters.Postgres.Repositories;
using OpenWeather.Domain.Temperature.Ports;

namespace OpenWeather.Adapters.Postgres.Config;

public class DiConfig
{
    public static void Configure(ContainerBuilder builder)
    {
        builder.RegisterType<TemperatureRepository>().As<ITemperatureRepository>();
    }
}