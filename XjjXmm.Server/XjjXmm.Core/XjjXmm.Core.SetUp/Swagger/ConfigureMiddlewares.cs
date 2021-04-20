using Microsoft.AspNetCore.Builder;

namespace XjjXmm.Core.SetUp.Swagger
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
