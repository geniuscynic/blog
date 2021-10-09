// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.


using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XjjXmm.FrameWork.Filter;
using XjjXmm.FrameWork.Startup;
using XjjXmm.FrameWork.Swagger;

[assembly: HostingStartup(typeof(XjjXmm.FrameWork.Startup.HostingStartup))]
namespace XjjXmm.FrameWork.Startup
{
    /// <summary>
    /// 配置程序启动时自动注入
    /// </summary>

    public sealed class HostingStartup : IHostingStartup
    {
        /// <summary>
        /// 配置应用启动
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(IWebHostBuilder builder)
        {
            // 应用初始化服务
            builder.ConfigureServices((hostContext, services) =>
            {
                services.AddSwaggerSetup();

                // 存储配置对象
                App.Configuration = hostContext.Configuration;

                // 注册 HttpContextAccessor 服务
                services.AddHttpContextAccessor();


                services.Configure<MvcOptions>(option =>
                {
                    option.Filters.Add(typeof(MvcActionFilter));
                    option.Filters.Add(typeof(GlobalExceptionsFilter));
                });
            });

            //builder.Configure((context, applicationBuilder) =>
            //{
            //    applicationBuilder.UseSwaggerMiddlewares();
            //});

        }
    }
}