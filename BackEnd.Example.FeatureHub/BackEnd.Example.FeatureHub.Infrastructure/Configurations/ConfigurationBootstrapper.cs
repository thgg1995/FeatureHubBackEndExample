using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Example.FeatureHub.Infrastructure.Configurations
{
    public static class ConfigurationBootstrapper
    {
        private static string AppSettingsHost => "Host";

        public static void RegisterConfigurations(this IServiceCollection serviceColletion, IConfiguration configuration)
        {
            serviceColletion.Configure<AppConfigurations>(configuration);
            serviceColletion.AddScoped(c => c.GetService<IOptionsSnapshot<AppConfigurations>>().Value);

            serviceColletion.Configure<LogFilterConfiguration>(configuration.GetSection(nameof(AppConfigurations.LogFilter)));
            serviceColletion.AddScoped(c => c.GetService<IOptionsSnapshot<LogFilterConfiguration>>().Value);
        }

        public static IServiceCollection ConfigureAppSettingsOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            HostOptions host = configuration.GetSection(AppSettingsHost).Get<HostOptions>();

            services.Configure<HostOptions>(host => configuration.GetSection(AppSettingsHost).Bind(host));
            return services;
        }
    }
}
