using Autofac;
using AzTableStorage.Api.Code.Config;
using Microsoft.Extensions.Configuration;

namespace AzTableStorage.Api.Code.Modules
{
    public class ConfigModule : Module
    {
        private readonly IConfigurationRoot _configuration;

        public ConfigModule(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(_ => _configuration.GetSection("AppSettings").Get<AppSettings>()).SingleInstance();
            builder.Register(_ => _configuration.GetSection("AppSecrets").Get<AppSecrets>()).SingleInstance();
        }
    }
}