using System.IO;
using ei_slice;
using ei_web_api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ei_functional_tests.Web
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureAppConfiguration((context, builder) =>
            {
                builder.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .AddEnvironmentVariables();
            });

            webHostBuilder.ConfigureServices(async services =>
            {
                var provider = services.BuildServiceProvider();

                using (provider.CreateScope())
                {
                    await Initialize.DatabaseAsync();
                }
            });
        }
    }
}