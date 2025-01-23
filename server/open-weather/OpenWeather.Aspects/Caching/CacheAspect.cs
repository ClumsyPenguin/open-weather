using Castle.DynamicProxy;
using Microsoft.Extensions.Caching.Hybrid;
using System.Threading;

namespace OpenWeather.Aspects.Caching
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class CacheAttribute : Attribute
    {
        public CacheAttribute(int itemLifeSpanInMinutes)
        {
            ItemLifeSpan = TimeSpan.FromMinutes(itemLifeSpanInMinutes);
        }

        public CacheAttribute(double itemLifeSpanInMinutes)
        {
            ItemLifeSpan = TimeSpan.FromMinutes(itemLifeSpanInMinutes);
        }

        public TimeSpan ItemLifeSpan { get; }
    }

    public interface ICacheInterceptor : IAsyncInterceptor
    {
    }

    public class TieredCacheInterceptor : ICacheInterceptor
    {
        private readonly HybridCache _multiTieredCache;

        public TieredCacheInterceptor(HybridCache multiTieredCache)
        {
            _multiTieredCache = multiTieredCache;
        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            throw new InvalidOperationException("No caching allowed for synchronous methods");
        }


        public void InterceptAsynchronous(IInvocation invocation)
        {
            //This method is one which doesn't return a value, hence no caching desired
            invocation.Proceed();
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            if (IsCacheable(invocation, out var itemLifeSpan))
            {
                invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation, itemLifeSpan);
            }
            else
            {
                invocation.Proceed();
            }
        }

        private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation, TimeSpan itemLifeSpan)
        {
            var proceed = invocation.CaptureProceedInfo();
            var cacheKey = CacheKeyGenerator.Generate(invocation.Method, invocation.Arguments);
            var cancellationToken = (CancellationToken) invocation.Arguments.Single(a => a is CancellationToken);

            return (await _multiTieredCache.GetOrCreateAsync(cacheKey,
                                                          _ => GetNonCachedValueAsync<TResult>(proceed, invocation)!,
                                                          options: SetCacheEntryOptions(itemLifeSpan),
                                                          cancellationToken: cancellationToken))!;
        }

        private static async ValueTask<TResult> GetNonCachedValueAsync<TResult>(IInvocationProceedInfo proceed, IInvocation invocation)
        {
            proceed.Invoke();

            var task = (Task<TResult>)invocation.ReturnValue;
            var result = await task;

            return result;
        }
        
        private static bool IsCacheable(IInvocation invocation, out TimeSpan itemLifeSpan)
        {
            if (Attribute.IsDefined(invocation.MethodInvocationTarget, typeof(CacheAttribute)))
            {
                var cacheAttribute = Attribute.GetCustomAttribute(invocation.MethodInvocationTarget, typeof(CacheAttribute)) as CacheAttribute;
                itemLifeSpan = cacheAttribute!.ItemLifeSpan;

                return true;
            }

            itemLifeSpan = TimeSpan.Zero;
            return false;
        }

        private HybridCacheEntryOptions SetCacheEntryOptions(TimeSpan itemLifeSpan)
        {
            var entryOptions = new HybridCacheEntryOptions
            {
                Expiration = itemLifeSpan,
                LocalCacheExpiration = itemLifeSpan
            };
            
            return entryOptions;
        }
    }
}
