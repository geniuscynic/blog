using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using AspectCore.DynamicProxy.Parameters;
using log4net.Core;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using XjjXmm.FrameWork.Cache;
using XjjXmm.FrameWork.LogExtension;
using XjjXmm.FrameWork.ToolKit;
using XjjXmm.FrameWork.ToolKit.DataEncryption.Extensions;

namespace XjjXmm.FrameWork.Aop
{
    public class LogInterceptor : AbstractInterceptor
    {
        //通过注入的方式，把缓存操作接口通过构造函数注入
        // private readonly ICache _cache;
        //public CustomInterceptorAttribute(ICache cache)
        //{
        //    _cache = cache;
        //}

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            if(context.ImplementationMethod.GetCustomAttribute<ProcessLogAttribute>()  == null &&
               context.ImplementationMethod?.DeclaringType?.GetCustomAttribute<ProcessLogAttribute>() == null &&
               context.ServiceMethod.GetCustomAttribute<ProcessLogAttribute>() == null &&
               context.ServiceMethod?.DeclaringType?.GetCustomAttribute<ProcessLogAttribute>() == null )
            //if (context.ImplementationMethod.DeclaringType.FullName.Contains("Swashbuckle"))
            {
                await next(context);
                return;
            }

            var _logger = App.GetLog<LogInterceptor>();
           
            _logger.Debug($"执行类型名:{context.ImplementationMethod.DeclaringType.FullName}");
            _logger.Debug($"执行方法名:{context.ImplementationMethod.Name}");
            _logger.Debug("参数:");
            foreach (var parameter in context.GetParameters())
            {
                _logger.Debug($"{parameter.Name}:{parameter.Value.ToValue()}");
            }

            await next(context);

            //先判断方法是否有返回值，无就不进行缓存判断
            var returnParams = context.GetReturnParameter();
            if (returnParams.Type != typeof(void))
            {
                _logger.Debug($"返回值:{context.GetReturnParameter()?.Value?.ToValue()}");
            }
        }


        protected string GetArgumentValue(object arg)
        {
            if (arg is DateTime || arg is DateTime?)
                return ((DateTime)arg).ToString("yyyyMMddHHmmss");

            if (arg is string || arg is ValueType || arg is Nullable)
                return arg.ToString();

            if (arg != null)
            {
                if (arg.GetType().IsClass)
                {
                    return JsonConvert.SerializeObject(arg);
                }
                else
                {
                    return arg.ToString();
                }
            }
            return string.Empty;
        }

    }
}
