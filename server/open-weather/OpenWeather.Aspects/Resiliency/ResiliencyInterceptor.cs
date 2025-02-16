using Castle.DynamicProxy;
using Polly.Registry;

namespace OpenWeather.Aspects.Resiliency
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class ResilientAttribute : Attribute
    {
        public ResilientAttribute()
        {
        }
    }

    public interface IResiliencyInterceptor : IAsyncInterceptor
    {
    }

    public class ResiliencyInterceptor : IResiliencyInterceptor
    {
        private readonly ResiliencePipelineProvider<string> _pipeLineprovider;

        public ResiliencyInterceptor(ResiliencePipelineProvider<string> pipeLineprovider)
        {
            _pipeLineprovider = pipeLineprovider;
        }

        public void InterceptAsynchronous(IInvocation invocation)
        {
            if (IsResilient(invocation))
            {
                invocation.ReturnValue = ExecuteAsResilient(invocation);
            }
            else
            {
                invocation.Proceed();
            }            
        }
        
        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            if (IsResilient(invocation))
            {
                invocation.ReturnValue = ExecuteAsResilient<TResult>(invocation);
            }
            else
            {
                invocation.Proceed();
            }
        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            throw new InvalidOperationException("No resiliency is possible on synchronous methods");
        }

        private ValueTask<TResult> ExecuteAsResilient<TResult>(IInvocation invocation)
        {
            var pipeline = _pipeLineprovider.GetPipeline("Client-pipeline");

            return pipeline.ExecuteAsync<TResult>(async token =>
            {
                invocation.Proceed();

                var task = (ValueTask<TResult>)invocation.ReturnValue;
                var result = await task;

                return result;
            });
        }

        private ValueTask ExecuteAsResilient(IInvocation invocation)
        {
            var pipeline = _pipeLineprovider.GetPipeline("Client-pipeline");

            return pipeline.ExecuteAsync(token =>
            {
                invocation.Proceed();

                return (ValueTask)invocation.ReturnValue;
            });
        }

        private static bool IsResilient(IInvocation invocation)
        {
            if (Attribute.IsDefined(invocation.MethodInvocationTarget, typeof(ResilientAttribute)))
            {
                //var cacheAttribute = Attribute.GetCustomAttribute(invocation.MethodInvocationTarget, typeof(ResiliencyAttribute)) as ResiliencyAttribute;

                return true;
            }

            return false;
        }
    }
}
