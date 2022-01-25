using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace BackEnd.Example.FeatureHub.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, configure) =>
                {
                    configure.AddJsonFile("appsettings.json",
                        optional: false,
                        reloadOnChange: true);
                })
            .ConfigureWebHost((webHostBuilder) =>
            {
                webHostBuilder.UseKestrel((hostingContext, configure) =>
                {
                    if (hostingContext.HostingEnvironment.EnvironmentName != "development")
                    {
                        configure.Listen(
                            Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip =>
                            ip.AddressFamily == AddressFamily.InterNetwork)
                            , 8081
                            );
                    }
                });
                webHostBuilder.UseShutdownTimeout(TimeSpan.FromSeconds(05));
                webHostBuilder.UseStartup<Startup>();
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();
            })
            .ConfigureLogging((hostingContext, configLogging) =>
            {
                configLogging.AddConsole();
                configLogging.AddDebug();
            }).UseConsoleLifetime();
    }
}
