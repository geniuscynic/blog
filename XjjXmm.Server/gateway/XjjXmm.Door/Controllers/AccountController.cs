using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Internal;
using DoCare.Zkzx.Core.FrameWork.Tool.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XjjXmm.Core.FrameWork.Cache;
using XjjXmm.Core.SetUp.Configuration;
using XjjXmm.Core.SetUp.Jwt;
using XjjXmm.Door.model;

namespace XjjXmm.Door.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ICache _cache;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHubContext<NotificationHub> _hubContext;

        public AccountController(ICache cache, IHttpClientFactory httpClientFactory, IHubContext<NotificationHub> hubContext)
        {
            _cache = cache;
            _httpClientFactory = httpClientFactory;
            _hubContext = hubContext;
        }

        [HttpPost("login")]
        public async Task<string> Login(LoginModel model)
        {
            var setting = ConfigurationManager.GetSection<ClientSetting>($"{model.Client}:authorize");
            if (setting == null)
            {
                throw BussinessException.CreateException(ExceptionCode.KeyNotExist, "未授权的客户端");
            }


            var response = await _httpClientFactory.CreateClient()
                .PostAsync(setting.url, new StringContent($"loginName={model.Account}&password={model.Password}", 
                    Encoding.UTF8,
                    "application/x-www-form-urlencoded"));


            var res = "";

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var sr = new StreamReader(response.Content.ReadAsStreamAsync().Result, Encoding.GetEncoding("GB2312")))
            {
                res = await sr.ReadToEndAsync();
            }

            //{
            //    "IsSuccess": true,
            //    "Title": "提示",
            //    "Tip": "info",
            //    "Data": {
            //        "RedirectUrl": "/Home/index",
            //        "LoginName": "superadmin"
            //    },
            //    "Message": "登录成功",
            //    "Status": "0000"
            //}

            var responseObject = (JObject) JsonConvert.DeserializeObject(res);
            if (responseObject == null)
            {
                throw BussinessException.CreateException(ExceptionCode.CustomException, "登入失败，请联系管理员");
            }

            if (responseObject["IsSuccess"]?.ToString().ToLower() != "true")
            {
                throw BussinessException.CreateException(ExceptionCode.CustomException, responseObject["Message"]?.ToString()??"");
            }


            var option = new TokenModelOptions()
            {
                Id = responseObject["Data"]?["Id"]?.ToString()??"",
                ClientId = model.Client
            };

            //_cache.Set($"user_{option.Id}", responseObject["Data"], TimeSpan.FromDays(1), true);

            //做登入认证
            var jwtTokenSetting = ConfigurationManager.GetSection<JwtTokenSetting>($"{option.ClientId}:JWT");
            //访问url
            var jwt = JwtHelper.IssueToken(jwtTokenSetting, option);

            //_cache.Set(jwt, jwt, jwtTokenSetting.GetExpires());
            _cache.Set($"auth_{jwt}", jwt, jwtTokenSetting.GetExpires());
            //_cache.Remove($"accessToken_{option.AppId}_{option.Id}");

            return jwt;
        }

        [HttpPost("GetAccessToken")]
        public string GetAccessToken(AccessTokenModel tokenModel)
        {
            //var option1 = JwtHelper.DecryptToken(authorizationCode);
            //if (option1 == null)
            //{
            //    throw BussinessException.CreateException(ExceptionCode.CustomException, "非法的token");
            //}

            //var accessSecret = ConfigurationManager.GetSection<string>($"{option1.AppId}:secret:{secret}");
            //if (accessSecret != secret)
            //{
            //    throw BussinessException.CreateException(ExceptionCode.KeyNotExist, "未授权的客户端");
            //}

            ////做登入认证
            //var jwtTokenSetting = ConfigurationManager.GetSection<JwtTokenSetting>($"{option1.AppId}:JWT");
            //if (jwtTokenSetting == null)
            //{
            //    throw BussinessException.CreateException(ExceptionCode.KeyNotExist, "未授权的客户端");
            //}

            //var setting = ConfigurationManager.GetSection<ClientSetting>($"{option1.AppId}:authorize");
            //if (setting == null)
            //{
            //    throw BussinessException.CreateException(ExceptionCode.KeyNotExist, "未授权的客户端");
            //}


            //var option = JwtHelper.DecryptToken(jwtTokenSetting, authorizationCode);
            //if (option == null)
            //{
            //    throw BussinessException.CreateException(ExceptionCode.CustomException, "非法的token");
            //}
            //

            var authorizationCode = tokenModel.authorizationCode;
            var secret = tokenModel.secret;

            var code = _cache.Get<string>("auth_" + authorizationCode);

            if (code != authorizationCode)
            {
                throw BussinessException.CreateException(ExceptionCode.CustomException, "token不存在，请重新登入");
            }

            _cache.Remove("auth_" + authorizationCode);

            var option= JwtTool.DecryptAndValidationToken(authorizationCode);
            if (option == null)
            {
                throw BussinessException.CreateException(ExceptionCode.CustomException, "未授权的客户端");
            }

            var app = ConfigurationManager.GetSection<string>($"{option.ClientId}:secret:{secret}");
            if (app.IsNullOrEmpty())
            {
                throw BussinessException.CreateException(ExceptionCode.KeyNotExist, "未授权的客户端");
            }

            option.AppId = app;

          


            var jwtTokenSetting = ConfigurationManager.GetSection<JwtTokenSetting>($"{option.ClientId}:JWT");
            if (jwtTokenSetting == null)
            {
                throw BussinessException.CreateException(ExceptionCode.KeyNotExist, "未授权的客户端");
            }

            var accessToken = JwtHelper.IssueToken(jwtTokenSetting, option);
            _cache.Set($"at_{accessToken}", accessToken, jwtTokenSetting.GetExpires());

            //_cache.Set($"accessToken_{option.Id}", accessToken, jwtTokenSetting.GetExpires());

            return accessToken;
        }

        [HttpPost("RefreshToken")]
        public string RefreshToken(RefreshTokenModel refreshToken)
        {
            var token = refreshToken.code;
            if (!_cache.Contain($"at_{token}"))
            {
                throw BussinessException.CreateException(ExceptionCode.KeyNotExist, "未授权的客户端");
                //return Task.FromResult((Uri)null);
            }

            var option = JwtTool.DecryptAndValidationToken(token);
            if (option == null)
            {
                throw BussinessException.CreateException(ExceptionCode.KeyNotExist, "未授权的客户端");
            }

            var jwtTokenSetting = ConfigurationManager.GetSection<JwtTokenSetting>($"{option.ClientId}:JWT");
            if (jwtTokenSetting == null)
            {
                throw BussinessException.CreateException(ExceptionCode.KeyNotExist, "未授权的客户端");
            }

            //Thread.Sleep(500);

            var accessToken = JwtHelper.IssueToken(jwtTokenSetting, option);
            _cache.Set($"at_{accessToken}", accessToken, jwtTokenSetting.GetExpires());
            _cache.Remove($"at_{token}");

            return accessToken;

        }


        [HttpGet("SendNotification")]
        public async Task<string> SendNotification()
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", $"hello world");

            return "yes";
        }


    }
}
