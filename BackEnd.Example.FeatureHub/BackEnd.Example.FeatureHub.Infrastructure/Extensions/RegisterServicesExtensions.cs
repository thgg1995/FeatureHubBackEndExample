using BackEnd.Example.FeatureHub.Application.UseCases;
using BackEnd.Example.FeatureHub.Domain.Gateways;
using BackEnd.Example.FeatureHub.Infrastructure.Gateways;
using FeatureHubSDK;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Example.FeatureHub.Infrastructure.Extensions
{
    public static class RegisterServicesExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IUseCaseAsync, ExecuteTaskUseCaseAsync>();
            services.AddTransient<IControlFeatureHubGateway, ControlFeatureHubGateway>();

            IFeatureHubConfig config = new EdgeFeatureHubConfig("http://localhost:8085/",
                "default/a134a698-29ad-4735-b1cf-b193d204e978/oB5QAkjdkpsMhxjoOA1obVtD3K3dO1oDXDhmx1Wc");

            services.Add(ServiceDescriptor.Singleton(typeof(IFeatureHubConfig), config));
            config.Init();
        }
    }
}
