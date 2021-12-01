using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using XjjXmm.FrameWork.Common;
using XjjXmm.FrameWork.LogExtension;
using XjjXmm.FrameWork.ToolKit;

namespace XjjXmm.FrameWork.Filter
{
    /// <summary>
    /// 全局异常错误日志
    /// </summary>
    public class GlobalExceptionsFilter : IExceptionFilter, IAsyncExceptionFilter
    {
      //  private readonly IWebHostEnvironment _env;
      private readonly IWebHostEnvironment _env;
      private readonly ILog<GlobalExceptionsFilter> _loggerHelper;

        public GlobalExceptionsFilter(IWebHostEnvironment env, ILog<GlobalExceptionsFilter> loggerHelper)
        {
            //_env = env;
            _env = env;
            _loggerHelper = loggerHelper;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BussinessException bussinessException)
            {
                context.Result = new JsonResult(new ResponseModel<string>("")
                {
                    Success = false,
                    Code = bussinessException.ExceptionModel.Code,
                    Message = bussinessException.ExceptionModel.Message
                });

                _loggerHelper.Error("GlobalExceptionsFilter", bussinessException);
                //context.Exception = null;
            }
            else
            {
                var message = context.Exception.Message;
                if (_env.IsProduction())
                {
                    message = StatusCodes.Status500InternalServerError.ToDescription();
                }

                context.Result = new InternalServerErrorObjectResult(new ResponseModel<string>(StatusCodes.Status500InternalServerError.ToDescription())
                {
                    Success = false,
                    Code = StatusCodes.Status500InternalServerError.ToInt(),
                    Message = message
                });

                _loggerHelper.Error("GlobalExceptionsFilter", context.Exception);
            }



            //  var json = new JsonErrorResponse();

            //  json.Message = context.Exception.Message;//错误信息
            //  var errorAudit = "Unable to resolve service for";
            //  if (!string.IsNullOrEmpty(json.Message) && json.Message.Contains(errorAudit))
            //  {
            //      json.Message = json.Message.Replace(errorAudit, $"（若新添加服务，需要重新编译项目）{errorAudit}");
            //  }

            // // if (_env.IsDevelopment())
            // // {
            //      json.DevelopmentMessage = context.Exception.StackTrace;//堆栈信息
            ////  }
            //  context.Result = new InternalServerErrorObjectResult(json);



            //  //采用log4net 进行错误日志记录
            //  _loggerHelper.LogError(json.Message + WriteLog(json.Message, context.Exception));

        }

        /// <summary>
        /// 自定义返回格式
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public string WriteLog(string throwMsg, Exception ex)
        {
            return string.Format("\r\n【自定义错误】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}", new object[] { throwMsg,
                ex.GetType().Name, ex.Message, ex.StackTrace });
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }
    }
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError.ToInt();
        }
    }
    //返回错误信息
    public class JsonErrorResponse
    {
        /// <summary>
        /// 生产环境的消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 开发环境的消息
        /// </summary>
        public string DevelopmentMessage { get; set; }
    }

}
