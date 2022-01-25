using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Example.FeatureHub.Worker
{
    internal class WorkerHostBuilder
    {
        private readonly IHost host;

        internal WorkerHostBuilder(string[] args)
        {
            host = Host.CreateDefaultBuilder(args)
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
           })
           .Build();
        }

        internal Task Start()
        {
            return host?.RunAsync();
        }

        internal async Task StopAsync()
        {
            try
            {
                await host?.StopAsync(TimeSpan.FromSeconds(5));
                host?.WaitForShutdown();
            }
            catch (Exception excHostBuilder)
            {

                Console.WriteLine($"Erro ao tentar parar o worker service. Detalhes: {excHostBuilder.Message}");
            }
        }
    }
}
