using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Serilog;


namespace XjjXmm.Core.FrameWork.Interceptor
{
    /// <summary>
    /// AOP log 记录 并掏出异常
    /// </summary>
    public class LogInterceptor : AsyncInterceptorBase
    {
        private readonly ILogger logger;

        public LogInterceptor(ILogger logger)
        {
            this.logger = logger;
        }
       // public ILogger logger { get; set; }

        public StringBuilder LogMessage = new StringBuilder();
        protected override void BeforeProceed(IInvocation invocation)
        {
            LogMessage.Append(
                                $"【当前执行方法】： {invocation.TargetType.Name}.{ invocation.Method.Name} \r\n" +
                                $"【携带的参数有】： {string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())} \r\n");

        }

        protected override Task AfterProceedAsync(IInvocation invocation, bool hasAsynResult)
        {
            
            if (hasAsynResult)
            {
                LogMessage.Append($"【执行完成结果】：{invocation.ReturnValue}");

                logger.Information(LogMessage.ToString());
            }

            return Task.CompletedTask;
        }

        protected override void AfterProceedSync(IInvocation invocation)
        {
            LogMessage.Append($"【执行完成结果】：{invocation.ReturnValue}");

            logger.Information(LogMessage.ToString());
        }

        protected override void ProceedException(IInvocation invocation, Exception ex)
        {
            logger.Error(WriteLog("AOP:" + invocation.Method.Name, ex));
        }

        private string WriteLog(string throwMsg, Exception ex)
        {
            return string.Format("\r\n【自定义错误1】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}", new object[] { throwMsg,
                ex.GetType().Name, ex.Message, ex.StackTrace });
        }

        
    }
   
}
