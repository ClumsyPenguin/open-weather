using Microsoft.Extensions.Caching.Hybrid;

namespace OpenWeather.Adapters.REST.Configuration;

public static class Caching
{
#pragma warning disable EXTEXP0018
    public static IServiceCollection ConfigureCaching(this IServiceCollection services, ConfigurationManager configuration)
    {
        
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = 
                configuration.GetConnectionString("RedisConnectionString");
        });
        
        //HybridCache will automatically use Redis as L2
        services.AddHybridCache(options =>
        {
            options.MaximumPayloadBytes = 1024 * 1024;
            options.MaximumKeyLength = 1024;
            options.DefaultEntryOptions = new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(15),
                LocalCacheExpiration = TimeSpan.FromMinutes(15)
            };
        });
        
        return services;
    }
#pragma warning restore EXTEXP0018
}   