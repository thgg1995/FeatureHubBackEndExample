using BackEnd.Example.FeatureHub.Application.UseCases;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Example.FeatureHub.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHost _host;
        private IUseCaseAsync _UseCaseAsync;

        public Worker(ILogger<Worker> logger, IHost host, IUseCaseAsync useCaseAsync)
        {
            _logger = logger;
            _host = host;
            _UseCaseAsync = useCaseAsync;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                Console.WriteLine("Chamando exemplo useCase percent rollout");
                await _UseCaseAsync.ExecuteFeaturePercentAsync();

                Console.WriteLine("Aperte qualquer tecla para executar o proximo exemplo");
                Console.ReadKey();

                Console.WriteLine("Chamando exemplo useCase boolean");
                await _UseCaseAsync.ExecuteFeatureBooleanAsync();

                Console.WriteLine("Aperte qualquer tecla para executar o proximo exemplo");
                Console.ReadKey();

                Console.WriteLine("Chamando exemplo useCase User Rule");
                await _UseCaseAsync.ExecuteFeatureUserAsync();

                Console.ReadKey();

                await _host?.StopAsync();
                _host?.WaitForShutdown();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
