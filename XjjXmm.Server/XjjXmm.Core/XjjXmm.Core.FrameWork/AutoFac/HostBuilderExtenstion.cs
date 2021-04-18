using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DoCare.Extension.AutoFac
{
    public static class HostBuilderExtenstion
    {
        public static IHostBuilder AddAutoFaceBuilder(this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory()); ;


        }
    }
}
