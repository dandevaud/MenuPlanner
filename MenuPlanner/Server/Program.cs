using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MenuPlanner.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .ConfigureAppConfiguration( options =>
                        {
                            options.AddEnvironmentVariables();
                            options.AddJsonFile("config/settings.json", true);
                            options.AddJsonFile($"config/settings.{webBuilder.GetSetting("ENVIRONMENT")}.json", true);
                        });
                });
    }
}
