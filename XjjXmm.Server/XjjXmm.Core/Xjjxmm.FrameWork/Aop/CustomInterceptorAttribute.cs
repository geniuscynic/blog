using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using AspectCore.DynamicProxy.Parameters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using XjjXmm.FrameWork.Cache;
using XjjXmm.FrameWork.ToolKit.DataEncryption.Extensions;

namespace XjjXmm.FrameWork.Aop
{
    public class CustomInterceptor : AbstractInterceptor
    {
        //通过注入的方式，把缓存操作接口通过构造函数注入
       // private readonly ICache _cache;
        //public CustomInterceptorAttribute(ICache cache)
        //{
        //    _cache = cache;
        //}

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            //先判断方法是否有返回值，无就不进行缓存判断
            var returnParams = context.GetReturnParameter();
            if (returnParams.Type == typeof(void))
            {
                await next(context);
                return;
            }

            var method = context.ProxyMethod;
            //对当前方法的特性验证
            //如果需要验证
            if (method.GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(CachingAttribute)) is CachingAttribute qCachingAttribute)
            {
                var _cache = context.ServiceProvider.GetService<ICache>();
                //获取自定义缓存键
                var cacheKey = CustomCacheKey(context);
                //根据key获取相应的缓存值
                var cacheValue = _cache.Get<object>(cacheKey);
                if (cacheValue != null)
                {
                    //将当前获取到的缓存值，赋值给当前执行方法
                    //invocation.ReturnValue = cacheValue;
                    context.ReturnValue = cacheValue;
                    return;
                }
                //去执行当前的方法
                await next(context);

                //存入缓存
                if (!string.IsNullOrWhiteSpace(cacheKey))
                {
                    _cache.Set(cacheKey, context.ReturnValue, new TimeSpan(0,qCachingAttribute.AbsoluteExpiration,0));
                }
            }
            else
            {
                await next(context);//直接执行被拦截方法
            }


        }

        protected string CustomCacheKey(AspectContext context)
        {
            //var key = "Methods:" + context.ImplementationMethod.DeclaringType.FullName + "." + context.ImplementationMethod.Name;

            var typeName = context.ImplementationMethod.DeclaringType.FullName;// context.TargetType.Name;
            var methodName = context.ImplementationMethod.Name;
            var methodArguments = context.Parameters.Select(GetArgumentValue).Take(3).ToList();//获取参数列表，最多三个

            var key = $"{typeName}:{methodName}:";
            foreach (var param in methodArguments)
            {
                key = $"{key}{param}:";
            }

            return key.TrimEnd(':');
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
                    return JsonConvert.SerializeObject(arg).ToMD5Encrypt();
                }
                else
                {
                    var res = arg.GetHashCode() + arg.ToString();
                    return res.ToMD5Encrypt();
                }
            }
            return string.Empty;
        }

    }
}
