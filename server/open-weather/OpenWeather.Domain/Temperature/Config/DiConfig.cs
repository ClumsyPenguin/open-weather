using Microsoft.Extensions.DependencyInjection;
using OpenWeather.Domain.Temperature.Services.Ports;
using OpenWeather.Domain.Temperature.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeather.Domain.Temperature.Config
{
    internal static class DiConfig
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITemperatureService, TemperatureService>();
        }
    }
}
