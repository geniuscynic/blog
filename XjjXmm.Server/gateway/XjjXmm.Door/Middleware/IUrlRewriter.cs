using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoCare.Zkzx.Core.FrameWork.Tool.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using XjjXmm.Core.FrameWork.Cache;
using XjjXmm.Core.SetUp.Configuration;
using XjjXmm.Door.model;

namespace XjjXmm.Door.Middleware
{
    public interface IUrlRewriter
    {
        Task<Uri> RewriteUri(HttpContext context);

        public string Id { get; }
    }

    public class PrefixRewriter : IUrlRewriter
    {
        private readonly PathString _prefix; //前缀值
        private readonly string _newHost; //转发的地址

        public PrefixRewriter(PathString prefix, string newHost)
        {
            _prefix = prefix;
            _newHost = newHost;
        }

        public Task<Uri> RewriteUri(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments(_prefix))//判断访问是否含有前缀
            {
                var newUri = context.Request.Path.Value.Remove(0, _prefix.Value.Length) + context.Request.QueryString;
                var targetUri = new Uri(_newHost + newUri);
                return Task.FromResult(targetUri);
            }

            return Task.FromResult((Uri)null);
        }

        public string Id { get; } = "";
    }

    public class TokenRewriter : IUrlRewriter
    {
        private readonly ICache _cache;
        private readonly ILogger<TokenRewriter> _logger;

        public TokenRewriter(ICache cache, ILogger<TokenRewriter> logger)
        {
            _cache = cache;
            _logger = logger;
        }
        //private readonly PathString _prefix; //前缀值
        // private readonly string _newHost; //转发的地址

        //public TokenRewriter(PathString prefix, string newHost)
        //{
        //   / _prefix = prefix;
        //    _newHost = newHost;
        //}


        public string Id { get; private set; }
        public Task<Uri> RewriteUri(HttpContext context)
        {
            _logger.LogInformation("访问的URL:"+ context.Request.Path.Value);
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var code = context.Request.Headers["Authorization"];

                if (code.ToString().Length <= 7 || !code.ToString().StartsWith("Bearer"))
                {
                    return Task.FromResult((Uri) null);
                }


                code = code.ToString().Substring(7);

                if (!_cache.Contain($"at_{code}"))
                {
                    throw BussinessException.CreateException(ExceptionCode.KeyNotExist, "未授权的客户端");
                    //return Task.FromResult((Uri)null);
                }



                var option = JwtTool.DecryptAndValidationToken(code);
                if (option == null)
                {
                    throw BussinessException.CreateException(ExceptionCode.KeyNotExist, "未授权的客户端");
                }

                Id = option.Id;

                var appId = option.AppId;

                var module = context.Request.Headers["module"];

                if (context.Request.Headers.ContainsKey("module"))  {
                    if (module.ToString()  == "SIE")
                    {
                        appId = module.ToString();
                    }
                }

           
                //_cache.Set($"user_{option.Id}", responseObject["Data"], TimeSpan.FromDays(1), true);

                var url = ConfigurationManager.GetSection<string>($"{option.ClientId}:{appId}:url");

                var newUri = context.Request.Path.Value + context.Request.QueryString;


                var targetUri = new Uri(new Uri(url), newUri);

                return Task.FromResult(targetUri);

            }
            //if (context.Request.Path.StartsWithSegments(_prefix))//判断访问是否含有前缀
            //{
            //    var newUri = context.Request.Path.Value.Remove(0, _prefix.Value.Length) + context.Request.QueryString;
            //    var targetUri = new Uri(_newHost + newUri);
            //    return Task.FromResult(targetUri);
            //}

            return Task.FromResult((Uri)null);
        }
    }
}
