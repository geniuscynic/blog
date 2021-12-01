using Admin.Core.Common.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System.IO;
using XjjXmm.FrameWork;

namespace Admin.Core.Extensions
{
    public static class UploadConfigApplicationBuilderExtensions
    {
        private static void UseFileUploadConfig(IApplicationBuilder app, FileUploadConfig config)
        {
            if (!Directory.Exists(config.UploadPath))
            {
                Directory.CreateDirectory(config.UploadPath);
            }

            app.UseStaticFiles(new StaticFileOptions()
            {
                RequestPath = config.RequestPath,
                FileProvider = new PhysicalFileProvider(config.UploadPath)
            });
        }

        public static IApplicationBuilder UseUploadConfig(this IApplicationBuilder app)
        {
            var uploadConfig = App.Configuration.GetSection<UploadConfig>("upload");  //app.ApplicationServices.GetRequiredService<IOptions<UploadConfig>>();
            UseFileUploadConfig(app, uploadConfig.Avatar);
            UseFileUploadConfig(app, uploadConfig.Document);

            return app;
        }
    }
}