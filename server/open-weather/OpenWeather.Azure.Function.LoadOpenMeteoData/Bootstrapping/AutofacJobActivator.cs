using Autofac;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace OpenWeather.Core.DependencyInjection
{
    internal class AutofacJobActivator : IJobActivatorEx
    {
        public T CreateInstance<T>()
        {
            // In practice, this method will not get called. We cannot safely resolve T here
            // because we don't have access to an ILifetimeScope, so it's better to just
            // throw.
            throw new NotSupportedException();
        }

        public T CreateInstance<T>(IFunctionInstanceEx functionInstance)
            where T : notnull
        {
            var lifetimeScope = functionInstance.InstanceServices
                .GetRequiredService<LifetimeScopeWrapper>()
                .Scope;

            // This is necessary because some dependencies of ILoggerFactory are registered
            // after FunctionsStartup.
            var loggerFactory = functionInstance.InstanceServices.GetRequiredService<ILoggerFactory>();
            lifetimeScope.Resolve<ILoggerFactory>(
                new NamedParameter(LoggerModule.LoggerFactoryParam, loggerFactory)
            );
            lifetimeScope.Resolve<ILogger>(
                new NamedParameter(LoggerModule.FunctionNameParam, functionInstance.FunctionDescriptor.LogName)
            );

            return lifetimeScope.Resolve<T>();
        }
    }
}

internal sealed class LifetimeScopeWrapper : IDisposable
{
    public ILifetimeScope Scope { get; }

    public LifetimeScopeWrapper(IContainer container)
    {
        Scope = container.BeginLifetimeScope();
    }

    public void Dispose()
    {
        Scope.Dispose();
    }
}

internal class LoggerModule : Module
{
    public const string LoggerFactoryParam = "loggerFactory";
    public const string FunctionNameParam = "functionName";

    protected override void Load(ContainerBuilder builder)
    {
        builder.Register((ctx, p) => p.Named<ILoggerFactory>(LoggerFactoryParam))
            .SingleInstance();

        builder.Register((ctx, p) =>
        {
            var factory = ctx.Resolve<ILoggerFactory>();
            var functionName = p.Named<string>(FunctionNameParam);

            return factory.CreateLogger(Microsoft.Azure.WebJobs.Logging.LogCategories.CreateFunctionUserCategory(functionName));
        })
            .InstancePerLifetimeScope();
    }
}
