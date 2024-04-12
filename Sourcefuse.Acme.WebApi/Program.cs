
using Microsoft.AspNetCore.Hosting;

namespace Sourcefuse.Acme.WebApi
{
    /// <summary>
    /// Main Program class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Program entry point
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            await host.RunAsync();
        }

        /// <summary>
        /// Host Builder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    _ = webBuilder.UseStartup<Startup>()
                                  .UseDefaultServiceProvider(options => options.ValidateScopes = false);
                });

  
    }
}
