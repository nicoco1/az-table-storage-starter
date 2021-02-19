using System;
using Autofac;
using AzTableStorage.Core.Azure;
using AzTableStorage.Shared.Config;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using static AzTableStorage.Api.Code.Config.EnvironmentVariableKeys;

namespace AzTableStorage.Api.Code.Modules
{
    public class AzureKeyVaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(ConfigureSecretClient)
                .As<SecretClient>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<KeyVaultAccessor>()
                .As<IKeyVaultAccessor>()
                .InstancePerLifetimeScope();
            
            builder
                .RegisterType<KeyVaultCache>()
                .As<IKeyVaultCache>()
                .InstancePerLifetimeScope();
        }

        private static SecretClient ConfigureSecretClient(IComponentContext componentContext)
        {
            var options = new SecretClientOptions()
            {
                Retry =
                {
                    Delay = TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                }
            };

            var environmentVariables = componentContext.Resolve<IEnvironmentVariables>();
            var keyVaultName = environmentVariables.GetValue(AZURE_KEYVAULT_NAME);
            var tenantId = environmentVariables.GetValue(AZURE_TENANT_ID);
            var clientId = environmentVariables.GetValue(AZURE_CLIENT_ID);
            var clientSecret = environmentVariables.GetValue(AZURE_CLIENT_SECRET);
            var credentials = new ClientSecretCredential(tenantId, clientId, clientSecret);

            return new SecretClient(new Uri($"https://{keyVaultName}.vault.azure.net"), credentials, options);
        }
    }
}