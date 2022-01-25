using BackEnd.Example.FeatureHub.Domain.Entities;
using BackEnd.Example.FeatureHub.Domain.Gateways;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Example.FeatureHub.Application.UseCases
{
    public class ExecuteTaskUseCaseAsync : IUseCaseAsync
    {
        private readonly IControlFeatureHubGateway _controlFeatureHubGateway;

        public ExecuteTaskUseCaseAsync(IControlFeatureHubGateway controlFeatureHubGateway)
        {
            _controlFeatureHubGateway = controlFeatureHubGateway;
        }
        public async Task ExecuteFeaturePercentAsync()
        {
            Console.WriteLine("Exemplo tombamento fundo");
            var cart = FillEntities();

            foreach (var fundo in cart.Fundos)
            {
                var resultFlag = await _controlFeatureHubGateway.CallApiPercent();

                if (resultFlag != null)
                {
                    Console.WriteLine($"{fundo.Nome} Deve ser tombado? {resultFlag}");
                }
            }

            await Task.CompletedTask;
        }

        public async Task ExecuteFeatureBooleanAsync()
        {
            Console.WriteLine("Exemplo tombamento fundo");
            var cart = FillEntities();

            foreach (var fundo in cart.Fundos)
            {
                var resultFlag = await _controlFeatureHubGateway.CallApiBoolean();

                if (resultFlag != null)
                {
                    Console.WriteLine($"{fundo.Nome} Deve ser tombado? {resultFlag}");
                }
            }

            await Task.CompletedTask;
        }

        public async Task ExecuteFeatureUserAsync()
        {
            Console.WriteLine("Exemplo tombamento fundo");
            var cart = FillEntities();

            foreach (var fundo in cart.Fundos)
            {
                var resultFlag = await _controlFeatureHubGateway.CallApiUser();

                if (resultFlag != null)
                {
                    Console.WriteLine($"{fundo.Nome} Deve ser tombado? {resultFlag}");
                }
            }
        }

        private Carteira FillEntities()
        {
            var carteira = new Carteira();
            carteira.Fundos = new List<Fundo>();

            for (int i = 0; i < 10; i++)
            {
                carteira.Fundos.Add(new Fundo { Nome = $"fundo{i}" });
            }
            return carteira;
        }
    }
}
