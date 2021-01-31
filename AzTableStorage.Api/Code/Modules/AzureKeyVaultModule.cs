using Autofac;
using AzTableStorage.Core.Azure;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

namespace AzTableStorage.Api.Code.Modules
{
    public class AzureKeyVaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(_ => new KeyVaultClient(CreateAuthenticationCallback()))
                .As<IKeyVaultClient>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<KeyVaultAccessor>()
                .As<IKeyVaultAccessor>()
                .InstancePerLifetimeScope();
        }

        private static KeyVaultClient.AuthenticationCallback CreateAuthenticationCallback()
        {
            return new KeyVaultClient.AuthenticationCallback(new AzureServiceTokenProvider().KeyVaultTokenCallback);
        }
    }
}