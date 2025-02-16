using Autofac;
using Castle.DynamicProxy;
using OpenWeather.Aspects.Caching;
using OpenWeather.Aspects.Resiliency;

namespace OpenWeather.Aspects.Config
{
    public static class DiConfig
    {
        public static void Configure(ContainerBuilder builder)
        {
            builder.RegisterType<ProxyGenerator>().AsSelf();
            builder.RegisterType<TieredCacheInterceptor>().As<ICacheInterceptor>();
            builder.RegisterType<ResiliencyInterceptor>().As<IResiliencyInterceptor>();
        }
    }
}
