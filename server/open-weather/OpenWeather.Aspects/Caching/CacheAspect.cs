using Metalama.Extensions.DependencyInjection;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Microsoft.Extensions.Caching.Hybrid;
using System.Threading;

namespace OpenWeather.Aspects.Caching
{
    public class CacheAttribute : OverrideMethodAspect
    {
        [IntroduceDependency]
        private readonly HybridCache _hybridCache;
        private readonly int _durationInHours;

        public CacheAttribute(int durationInHours)
        {
            _durationInHours = durationInHours;
        }

        public override dynamic? OverrideMethod()
        {
          
            var entryOptions = new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromHours(_durationInHours), 
                LocalCacheExpiration = TimeSpan.FromHours(_durationInHours),
            };

            CancellationToken cancellationToken = meta.Target.Method.Parameters["cancellationToken"].Value;

            return _hybridCache.GetOrCreateAsync<CacheAttribute, object>(CreateKey(meta.Target.Method),
                                                (state, cancel) => state.ExecuteMethod(cancellationToken),
                                                entryOptions,
                                                cancellationToken: cancellationToken);
            
        }

        [RunTime]
        private async ValueTask<object> ExecuteMethod(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        private string CreateKey(IMethod method)
        {
            var parameterValues = string.Join('-',method.Parameters.Select(p => p));

            return $"{method.Name}-{parameterValues}";
        }
    }
}
