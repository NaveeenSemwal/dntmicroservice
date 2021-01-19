using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((host, config) =>
            {
                // Was facing ENVIRONMENT issue while doing Db migrations.
                // https://stackoverflow.com/questions/45881069/ef-core-don%C2%B4t-use-aspnetcore-environment-during-update-database

                string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                config.AddJsonFile("appsettings.json");  // load common configurations
                config.AddJsonFile($"appsettings.{env}.json");  // load env specfice configurations
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
