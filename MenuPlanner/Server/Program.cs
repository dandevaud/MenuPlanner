// <copyright file="Program.cs" company="Alessandro Marra & Daniel Devaud">
// Copyright (c) Alessandro Marra & Daniel Devaud.
// </copyright>

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MenuPlanner.Server
{
    public static class Program
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
                        .ConfigureAppConfiguration(options =>
                        {
                            options.AddJsonFile("config/settings.json", true);
                            options.AddJsonFile($"config/settings.{webBuilder.GetSetting("ENVIRONMENT")}.json", true);
                            options.AddEnvironmentVariables();
                        });
                });
    }
}
