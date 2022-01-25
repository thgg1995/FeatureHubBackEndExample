using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace BackEnd.Example.FeatureHub.Worker
{
    class Program
    {
        private static WorkerHostBuilder _workerHostBuilder;
        public static async Task Main(string[] args)
        {
            try
            {
                _workerHostBuilder = new WorkerHostBuilder(args);
                await _workerHostBuilder?.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Worker has not been launched. Error: {ex.Message}");

                await StopHostService();
                Process.GetCurrentProcess().Kill();
            }
        }

        public static async Task StopHostService()
        {
            await _workerHostBuilder?.StopAsync();
        }
    }
}
