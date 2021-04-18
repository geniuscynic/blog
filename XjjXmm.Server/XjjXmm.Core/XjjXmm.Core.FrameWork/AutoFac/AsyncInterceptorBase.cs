using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace XjjXmm.Framework.AutoFac
{
    public abstract class AsyncInterceptorBase : IInterceptor
    {
        public AsyncInterceptorBase()
        {
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                BeforeProceed(invocation);
                invocation.Proceed();
                if (IsAsyncMethod(invocation.MethodInvocationTarget))
                {
                    invocation.ReturnValue = InterceptAsync((dynamic) invocation.ReturnValue, invocation);
                }
                else
                {
                    AfterProceedSync(invocation);
                }
            }
            catch (Exception ex)
            {
                ProceedException(invocation, ex);
                throw;
            }
        }

        //didn't support ValueTask yet
        protected virtual bool IsAsyncMethod(MethodInfo method)
        {
            var attr = method.GetCustomAttributes<AsyncStateMachineAttribute>(false);
            bool isAsync = (attr != null) && typeof(Task).IsAssignableFrom(method.ReturnType);
            return isAsync;
        }

        private async Task InterceptAsync(Task task, IInvocation invocation)
        {
            await task.ConfigureAwait(false);
            await AfterProceedAsync(invocation, false);
        }

        protected object ProceedAsynResult { get; set; }

        private async Task<TResult> InterceptAsync<TResult>(Task<TResult> task, IInvocation invocation)
        {
            TResult result = await task.ConfigureAwait(false);
            ProceedAsynResult = result;
            await AfterProceedAsync(invocation, true);
            return (TResult)ProceedAsynResult;
        }

        protected virtual void BeforeProceed(IInvocation invocation) { }

        protected virtual void AfterProceedSync(IInvocation invocation) { }

        protected virtual void ProceedException(IInvocation invocation, Exception ex) { }
        
        protected virtual Task AfterProceedAsync(IInvocation invocation, bool hasAsynResult)
        {
            return Task.CompletedTask;
        }
    }
}
