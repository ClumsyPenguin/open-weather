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

        private async Task<TResult> ExecuteAsResilient<TResult>(IInvocation invocation)
        {
            var pipeline = _pipeLineprovider.GetPipeline("Client-pipeline");

            return await pipeline.ExecuteAsync(async token =>
            {
                var task = (Task<TResult>)invocation.MethodInvocationTarget.Invoke(invocation.InvocationTarget, invocation.Arguments);

                return await task;
            });
        }

        private async Task ExecuteAsResilient(IInvocation invocation)
        {
            var pipeline = _pipeLineprovider.GetPipeline("Client-pipeline");

            await pipeline.ExecuteAsync(async token =>
            {
                var task = (Task)invocation.MethodInvocationTarget.Invoke(invocation.InvocationTarget, invocation.Arguments);

                await task;
            });
        }

        private static bool IsResilient(IInvocation invocation)
        {
            if (Attribute.IsDefined(invocation.MethodInvocationTarget, typeof(ResilientAttribute)))
            {
                return true;
            }

            return false;
        }
    }
}
