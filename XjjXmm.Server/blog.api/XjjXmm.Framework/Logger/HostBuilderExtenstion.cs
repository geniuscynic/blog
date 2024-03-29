﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace XjjXmm.Framework.Logger
{
    public static class HostBuilderExtenstion
    {
        public static IHostBuilder AddLogNetBuilder(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureLogging((hostingContext, builder) =>
            {
                //过滤掉系统默认的一些日志
                //builder.AddFilter("System", LogLevel.Error);
                //builder.AddFilter("Microsoft", LogLevel.Error);
                //builder.AddFilter("Blog.Core.AuthHelper.ApiResponseHandler", LogLevel.Error);

                //可配置文件
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Log4net.config");


                builder.AddLog4Net(path);
            });


        }
    }
}
