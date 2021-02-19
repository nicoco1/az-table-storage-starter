using Autofac;
using AzTableStorage.Shared.Config;
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
            builder
                .Register(_ => _configuration.GetSection("AppSettings").Get<AppSettings>())
                .As<IAppSettings>()
                .SingleInstance();

            builder
                .RegisterType<EnvironmentVariables>()
                .As<IEnvironmentVariables>()
                .InstancePerLifetimeScope();
        }
    }
}