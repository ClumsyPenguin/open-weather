using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeather.Adapters.Postgres.Config
{
    public static class DbContextConfig
    {
        public static void Configure(IServiceCollection serviceCollection, IConfiguration config)
        {
            serviceCollection.AddNpgsql<OpenWeatherDbContext>(config.GetConnectionString("OpenWeatherDB"));
        }
    }
}
