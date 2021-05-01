using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace XjjXmm.Core.FrameWork
{
    public class Logger
    {
        private readonly ILogger _log;

        public Logger(ILogger log)
        {
            _log = log;
        }

        /// <summary>
        /// 输出Debug日志
        /// </summary>
        /// <param name="message">消息对象</param>
        public void Debug(string message)
        {
            _log.Debug(message);

        }

        /// <summary>
        /// 输出Debug日志
        /// </summary>
        /// <param name="message">消息对象</param>
        /// <param name="e">异常信息</param>
        public void Debug(string message, Exception e)
        {

            _log.Debug(message, e);

        }

        /// <summary>
        /// 输出Debug日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="args">参数</param>
        public void DebugFormat(string message, params object[] args)
        {

            _log.Debug(message, args);

        }

        /// <summary>
        /// 输出Info日志
        /// </summary>
        /// <param name="message">消息对象</param>
        public void Info(string message)
        {

            _log.Information(message);

        }

        /// <summary>
        /// 输出Info日志
        /// </summary>
        /// <param name="message">消息对象</param>
        /// <param name="e">异常信息</param>
        public void Info(string message, Exception e)
        {

            _log.Information(e, message);

        }

        /// <summary>
        /// 输出Info日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="args">参数</param>
        public void InfoFormat(string message, params object[] args)
        {

            _log.Information(message, args);

        }

        /// <summary>
        /// 输出Warn日志
        /// </summary>
        /// <param name="message">消息对象</param>
        public void Warn(string message)
        {
            _log.Warning(message);

        }

        /// <summary>
        /// 输出Warn日志
        /// </summary>
        /// <param name="message">消息对象</param>
        /// <param name="e">异常信息</param>
        public void Warn(string message, Exception e)
        {

            _log.Warning(message, e);

        }

        /// <summary>
        /// 输出Info日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="args">参数</param>
        public void WarnFormat(string message, params object[] args)
        {

            _log.Warning(message, args);

        }

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="message">消息对象</param>
        public void Error(string message)
        {

            _log.Error(message);

        }

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="message">消息对象</param>
        /// <param name="e">异常信息</param>
        public void Error(string message, Exception e)
        {

            _log.Error(message, e);

        }

        /// <summary>
        /// 输出Error日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="args">参数</param>
        public void ErrorFormat(string message, params object[] args)
        {

            _log.Error(message, args);

        }

        /// <summary>
        /// 输出Fatal日志
        /// </summary>
        /// <param name="message">消息对象</param>
        public void Fatal(string message)
        {

            _log.Fatal(message);

        }

        /// <summary>
        /// 输出Fatal日志
        /// </summary>
        /// <param name="message">消息对象</param>
        /// <param name="e">异常信息</param>
        public void Fatal(string message, Exception e)
        {

            _log.Fatal(message, e);

        }

        /// <summary>
        /// 输出Fatal日志
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="args">参数</param>
        public void FatalFormat(string message, params object[] args)
        {

            _log.Fatal(message, args);

        }
    }
}
