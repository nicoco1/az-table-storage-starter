using System;
using Autofac;
using AzTableStorage.Core.Azure;
using AzTableStorage.Shared.Config;
using Azure.Core;
using Azure.Security.KeyVault.Secrets;

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
                .RegisterType<ClientCertificateCredentialProvider>()
                .As<IClientCertificateCredentialProvider>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<KeyVaultCache>()
                .As<IKeyVaultCache>()
                .InstancePerLifetimeScope();
        }

        private static SecretClient ConfigureSecretClient(IComponentContext componentContext)
        {
            var options = new SecretClientOptions
            {
                Retry =
                {
                    Delay = TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                }
            };

            // Because this app is not hosted in Azure, it uses X.509 certs to access the KeyVault
            var appSettings = componentContext.Resolve<IAppSettings>();
            var credentialsProvider = componentContext.Resolve<IClientCertificateCredentialProvider>();
            var clientCertificateCredential = credentialsProvider.GetCredentials();

            return new SecretClient(new Uri($"https://{appSettings.AzureKeyVaultName}.vault.azure.net"), clientCertificateCredential, options);
        }
    }
}