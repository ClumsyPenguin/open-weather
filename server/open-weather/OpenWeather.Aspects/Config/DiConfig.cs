using Autofac;
using OpenWeather.Aspects.Caching;

namespace OpenWeather.Aspects.Config
{
    public static class DiConfig
    {
        public static void Configure(ContainerBuilder builder)
        {
            builder.RegisterType<TieredCacheInterceptor>().As<ICacheInterceptor>();
        }
    }
}
