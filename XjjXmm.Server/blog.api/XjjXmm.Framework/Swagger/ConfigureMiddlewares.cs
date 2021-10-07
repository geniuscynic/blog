using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace XjjXmm.Framework.Swagger
{
    public static class ConfigureMiddlewares
    {
        public static IApplicationBuilder UseSwaggerMiddlewares(this IApplicationBuilder app, SwaggerConfig swaggerConfig = null)
        {
            swaggerConfig ??= new SwaggerConfig();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{swaggerConfig.Version}/swagger.json", swaggerConfig.Title);

                c.RoutePrefix = "";

                var types = typeof(ConfigureMiddlewares);

                var streamHtml = types.Assembly.GetManifestResourceStream($"{types.Namespace}.index.html");

                c.IndexStream = () => streamHtml;
            });

            return app;
        }
    }
}
