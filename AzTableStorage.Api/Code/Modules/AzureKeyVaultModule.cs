using Autofac;
using AzTableStorage.Core.Azure;
using AzTableStorage.Shared.Config;
using Azure.Security.KeyVault.Secrets;
using Nicoco.Lib.Az.KeyVault;
using Nicoco.Lib.Az.KeyVault.Credentials;

namespace AzTableStorage.Api.Code.Modules
{
    public class AzureKeyVaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(c =>
                {
                    var appSettings = c.Resolve<IAppSettings>();

                    return AzureSecretClientFactory.Create($"https://{appSettings.AzureKeyVaultName}.vault.azure.net", new ClientCertificateCredentialContext
                    {
                        TenantId = appSettings.AzureADDirectoryId,
                        ClientId = appSettings.AzureADApplicationId,
                        X509CertThumbprint = appSettings.AzureADCertThumbprint
                    });
                })
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
    }
}