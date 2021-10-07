using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace XjjXmm.Core.SetUp.AutoFac
{
    public static class HostBuilderExtenstion
    {
        public static IHostBuilder AddAutoFaceBuilder(this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory()); ;


        }
    }
}
