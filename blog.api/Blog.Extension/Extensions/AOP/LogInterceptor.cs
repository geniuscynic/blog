using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.Logging;
using StackExchange.Profiling;

namespace Blog.Extension.Extensions.AOP
{
    /// <summary>
    /// AOP log 记录 并掏出异常
    /// </summary>
    public class LogInterceptorAttribute : AbstractInterceptorAttribute
    {

        [AspectCore.DependencyInjection.FromServiceContext]
        public ILogger<LogInterceptorAttribute> logger { get; set; }

        //private readonly ILogger logger;

        //public LogInterceptorAttribute(ILogger logger)
        //{
        //    this.logger = logger;
        //}
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            try
            {

                string key = "Methods:" + context.Implementation.ToString() + "." + context.ServiceMethod.Name;

                MiniProfiler.Current.Step($"{key}");

                //Console.WriteLine("Before service call");
                await next(context);
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Service threw an exception!");
                //采用log4net 进行错误日志记录
                                       
                logger.LogError(WriteLog("AOP:" + context.ServiceMethod.Name, ex));

                throw;
            }
            finally
            {
                //Console.WriteLine("After service call");
            }
        }

        private string WriteLog(string throwMsg, Exception ex)
        {
            return string.Format("\r\n【自定义错误】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}", new object[] { throwMsg,
                ex.GetType().Name, ex.Message, ex.StackTrace });
        }
    }
   
}
