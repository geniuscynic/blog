﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using XjjXmm.Framework.AutoFac;

namespace DoCare.Extension.Logger
{
    /// <summary>
    /// AOP log 记录 并掏出异常
    /// </summary>
    public class LogInterceptor : AsyncInterceptorBase
    {
        public ILogger<LogInterceptor> logger { get; set; }

        public StringBuilder LogMessage = new StringBuilder();
        protected override void BeforeProceed(IInvocation invocation)
        {
            LogMessage.Append(
                                $"【当前执行方法】：{ invocation.Method.Name} \r\n" +
                                $"【携带的参数有】： {string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())} \r\n");

        }

        protected override Task AfterProceedAsync(IInvocation invocation, bool hasAsynResult)
        {
            
            if (hasAsynResult)
            {
                LogMessage.Append($"【执行完成结果】：{invocation.ReturnValue}");
            }

            return Task.CompletedTask;
        }

        protected override void AfterProceedSync(IInvocation invocation)
        {
            LogMessage.Append($"【执行完成结果】：{invocation.ReturnValue}");
        }

        protected override void ProceedException(IInvocation invocation, Exception ex)
        {
            logger.LogError(WriteLog("AOP:" + invocation.Method.Name, ex));
        }

        private string WriteLog(string throwMsg, Exception ex)
        {
            return string.Format("\r\n【自定义错误】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}", new object[] { throwMsg,
                ex.GetType().Name, ex.Message, ex.StackTrace });
        }

        
    }
   
}
