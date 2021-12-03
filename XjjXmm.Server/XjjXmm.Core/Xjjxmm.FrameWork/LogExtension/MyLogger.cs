using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Log4Net.AspNetCore.Extensions;

namespace XjjXmm.FrameWork.LogExtension
{
    public class LoggerHelper<T> : ILog<T>
    {
        private log4net.ILog _logger;

        private LoggerHelper()
        {
            var type = typeof(T);
            _logger = LogManager.GetLogger(string.Format("{0}.{1}", type.Namespace, type.Name));
        }


        public static LoggerHelper<T> GetLogger()
        {
            return new LoggerHelper<T>();
        }

        public void Debug(string msg)
        {
            _logger.Debug(msg);
        }

        public void Info(string msg)
        {
            _logger.Info(msg);
        }

        public void Trace(string msg, Exception ex)
        {
            _logger.Trace(msg, ex);
        }

        public void Error(string msg, Exception ex)
        {
            _logger.Error(msg, ex);
        }

        public void Critical(string msg, Exception ex)
        {
            _logger.Critical(msg, ex);
        }
    }
    public class MyLogger<T> : ILog<T>
    {
        private readonly ILogger<T> _logger;

        public MyLogger(ILogger<T> logger)
        {
            _logger = logger;
        }
        public void Debug(string msg)
        {
            _logger.LogDebug(msg);
        }

        public void Info(string msg)
        {
            _logger.LogInformation(msg);
        }

        public void Trace(string msg, Exception ex)
        {
            _logger.LogTrace(ex, msg);
        }

        public void Error(string msg, Exception ex)
        {
            _logger.LogError(ex, msg);
        }

        public void Critical(string msg, Exception ex)
        {
            _logger.LogCritical(ex, msg);
        }
    }


    public class DefaultLogger : MyLogger<DefaultLogger>
    {
        public DefaultLogger(ILogger<DefaultLogger> logger) : base(logger)
        {
        }
    }
}
