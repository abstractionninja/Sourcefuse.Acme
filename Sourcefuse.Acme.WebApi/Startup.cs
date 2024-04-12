using Sourcefuse.Acme.Common.Interfaces;
using Sourcefuse.Acme.Services;
using Sourcefuse.Acme.WebApi.Configuration;
using Sourcefuse.Acme.WebApi.Middleware;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sourcefuse.Acme.WebApi
{
    /// <summary>
    /// Startup class
    /// </summary>
    public partial class Startup(IConfiguration configuration)
    {

        private readonly IConfiguration Configuration = configuration;

        /// <summary>
        /// Configure Service Collection
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            _ = services.AddOptions()
                        .Configure<ApiKeySettings>(Configuration.GetSection(nameof(ApiKeySettings)))
                        .AddHttpContextAccessor()
                        .AddControllers()
                        .AddJsonOptions(options =>
                        {
                            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                            options.JsonSerializerOptions.WriteIndented = true;
                        });

            _ = services.AddSingleton<IAsyncRepoService, InMemoryRepoService>();

            SwaggerConfig.ConfigureServices(services, Configuration);
        }

        /// <summary>
        /// Configure App
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment() || env.IsEnvironment("dev"))
                _ = app.UseDeveloperExceptionPage();

            _ = app.UseHttpsRedirection()
                    .UseWhen(
                            ctx => !ctx.Request.Path.Value?.Contains("swagger")??false,
                            app => app.UseMiddleware<ApiKeyMiddleware>()
                    )
                   .UseRouting()
                   .UseStaticFiles()
                   .UseEndpoints(endpoints =>
                   {
                       _ = endpoints.MapControllers();
                       _ = endpoints.MapGet("/", async context =>
                       {
                           await context.Response.WriteAsync("Acme Api");
                       });
                   })
                   .UseAuthentication()
                   .UseAuthorization();

            SwaggerConfig.Configure(app, env, Configuration);
        }
    }
}
