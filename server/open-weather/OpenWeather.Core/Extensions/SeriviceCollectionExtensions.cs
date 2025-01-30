using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenWeather.Core.Exceptions;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using System.Net;

namespace OpenWeather.Core.Extensions
{
    public static class SeriviceCollectionExtensions
    {
        #region Caching
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
        #endregion

        #region CORS
        internal const string AllowAllPolicy = "AllowAll";

        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AllowAllPolicy,
                    builder => builder
                        .WithOrigins("*")
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            return services;
        }
        #endregion 

        #region Resiliency
        public static IServiceCollection ConfigureResiliency(this IServiceCollection services)
        {
            services.AddResiliencePipeline<string>("Client-pipeline", builder =>
            {
                builder
                    .AddRetry(new RetryStrategyOptions
                    {
                        BackoffType = DelayBackoffType.Constant,
                        Delay = TimeSpan.FromSeconds(2),
                        MaxRetryAttempts = 3,
                        ShouldHandle = new PredicateBuilder().Handle<ClientException>(e => IsTransient(e))
                    })
                    .AddCircuitBreaker(new CircuitBreakerStrategyOptions
                    {
                        BreakDuration = TimeSpan.FromMinutes(5),
                        FailureRatio = 0.75,
                        ShouldHandle = new PredicateBuilder().Handle<ClientException>(e => !IsTransient(e))
                    })
                    .AddTimeout(TimeSpan.FromSeconds(10));
            });


            return services;
        }

        private static bool IsTransient(ClientException e)
        {
            return e.StatusCode switch
            {
                HttpStatusCode.GatewayTimeout => true,
                HttpStatusCode.TooManyRequests => true,
                _ => false
            };
        }

        #endregion
    }
}
