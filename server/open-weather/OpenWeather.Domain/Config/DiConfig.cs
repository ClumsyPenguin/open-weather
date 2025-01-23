using Autofac;
using OpenWeather.Domain.Temperature.Config;
using OpenWeather.Domain.Temperature.Validators.Config;

namespace OpenWeather.Domain.Config
{
    public static class DiConfig
    {
        public static void Configure(ContainerBuilder builder)
        {
            builder.RegisterModule(new TemperatureModule());
            builder.RegisterModule(new ValidatorModule());            
        }
    }
}
