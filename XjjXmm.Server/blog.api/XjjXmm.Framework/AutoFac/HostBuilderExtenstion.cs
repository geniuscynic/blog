using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace XjjXmm.Framework.AutoFac
{
    public static class HostBuilderExtenstion
    {
        public static IHostBuilder AddAutoFaceBuilder(this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory()); ;


        }
    }
}
