// Copyright (c) 2020-2021 百小僧, Baiqian Co.,Ltd.
// Furion is licensed under Mulan PSL v2.
// You can use this software according to the terms and conditions of the Mulan PSL v2.
// You may obtain a copy of Mulan PSL v2 at:
//             https://gitee.com/dotnetchina/Furion/blob/master/LICENSE
// THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO NON-INFRINGEMENT, MERCHANTABILITY OR FIT FOR A PARTICULAR PURPOSE.
// See the Mulan PSL v2 for more details.


using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using XjjXmm.FrameWork.ToolKit;

namespace XjjXmm.FrameWork.Startup
{
    /// <summary>
    /// 主机构建器拓展类
    /// </summary>
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Web 主机注入
        /// </summary>
        /// <param name="hostBuilder">Web主机构建器</param>
        /// <param name="assemblyName">外部程序集名称</param>
        /// <returns>IWebHostBuilder</returns>
        public static IWebHostBuilder Inject(this IWebHostBuilder hostBuilder, string assemblyName = default)
        {
            var frameworkPackageName = assemblyName ?? ReflectKit.GetAssemblyName(typeof(HostBuilderExtensions));
            hostBuilder.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, frameworkPackageName);

            
            return hostBuilder;
        }

       
    }
}