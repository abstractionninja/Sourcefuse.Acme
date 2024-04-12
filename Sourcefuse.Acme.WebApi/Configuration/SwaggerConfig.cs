using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Sourcefuse.Acme.WebApi.Configuration
{
    /// <summary>
    /// Helper Class for configuring Swagger / Swagger UI
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        /// Configure app for Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="config"></param>
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration config)
        {
            _ = app.UseSwagger();

            var options = config.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>() ?? new SwaggerSettings();

            _ = app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(options.Path, $"{options.Title} [{env.EnvironmentName}]");

                if (!string.IsNullOrEmpty(options.CssFile))
                    c.InjectStylesheet(options.CssFile);

                if (options.AllowDeepLinking)
                    c.EnableDeepLinking();

                c.DisplayRequestDuration();
            });
        }

        /// <summary>
        /// Configure services for Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            var options = config.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>() ?? new SwaggerSettings();

            _ = services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(options.Version, new OpenApiInfo
                {
                    Title = options.Title,
                    Version = options.Version,
                    Description = "Acme Api Swagger UI"
                });

                c.OperationFilter<ApiKeyOperationFilter>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
