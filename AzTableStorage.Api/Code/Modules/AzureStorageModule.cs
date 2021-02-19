using System;
using Autofac;
using AzTableStorage.Api.Code.Config;
using AzTableStorage.Core.Azure;
using AzTableStorage.Core.Users;
using Microsoft.Azure.Cosmos.Table;

namespace AzTableStorage.Api.Code.Modules
{
    public class AzureStorageModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            ConfigureAzureStorageAccount(builder);
            ConfigureServicesWithRepositories(builder);
            ConfigureAzureCloudTables(builder);
            ConfigureCloudTablesForRepositories(builder);
        }

        private static void ConfigureAzureStorageAccount(ContainerBuilder builder)
        {
            builder.Register(c => CreateStorageAccount(FromKeyVaultSecret(c, KeyVaultSecrets.AzureTableStorageConnectionString)));
        }

        private static string FromKeyVaultSecret(IComponentContext c, string secretName)
        {
            return c.Resolve<IKeyVaultCache>().GetCached(secretName).GetAwaiter().GetResult();
        }

        private static CloudStorageAccount CreateStorageAccount(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Azure Storage connection string is null!");
            }
            return CloudStorageAccount.Parse(connectionString);
        }

        private static void ConfigureServicesWithRepositories(ContainerBuilder builder)
        {
            builder
                .RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();
        }

        private static void ConfigureAzureCloudTables(ContainerBuilder containerBuilder)
        {
            containerBuilder.Register(x => x.Resolve<CloudStorageAccount>().CreateCloudTableClient());
            containerBuilder.Register(x => GetTable(x, AzureTableStorage.UserTable)).Named<CloudTable>(AzureTableStorage.UserTable);
        }

        private static CloudTable GetTable(IComponentContext componentContext, string tableName)
        {
            var table = componentContext.Resolve<CloudTableClient>().GetTableReference(tableName);

            table.CreateIfNotExistsAsync().GetAwaiter();

            return table;
        }

        private static void ConfigureCloudTablesForRepositories(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<UserRepository>()
                .WithParameter(
                    (x, y) => x.ParameterType == (typeof(CloudTable)),
                    (x, y) => y.ResolveNamed<CloudTable>(AzureTableStorage.UserTable))
                .AsImplementedInterfaces();
        }
    }
}