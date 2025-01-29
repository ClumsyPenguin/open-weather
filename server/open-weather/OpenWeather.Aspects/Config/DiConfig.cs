using Autofac;
using Castle.DynamicProxy;
using OpenWeather.Aspects.Caching;

namespace OpenWeather.Aspects.Config
{
    public static class DiConfig
    {
        public static void Configure(ContainerBuilder builder)
        {
            builder.RegisterType<ProxyGenerator>().AsSelf();
            builder.RegisterType<TieredCacheInterceptor>().As<ICacheInterceptor>();
        }
    }
}
