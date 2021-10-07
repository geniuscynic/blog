using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace XjjXmm.Core.SetUp.AutoFac
{
    public class CacheTestInterceptorBase : IInterceptor
    {
       

        public void Intercept(IInvocation invocation)
        {
            try
            {
                 Console.WriteLine("aa");
                invocation.Proceed();
                Console.WriteLine("bb");
            }
            catch (Exception ex)
            {
                Console.WriteLine("cc");
                throw;
            }
        }

       
    }
}
