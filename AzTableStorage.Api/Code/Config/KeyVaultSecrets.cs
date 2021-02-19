namespace AzTableStorage.Api.Code.Config
{
    public static class KeyVaultSecrets
    {
        public static string AzureTableStorageConnectionString => "AzureTableStorageConnectionString";
        public static string MySecret => "mySecret";
    }

    public static class EnvironmentVariableKeys
    {
        public static string ASPNETCORE_ENVIRONMENT => nameof(ASPNETCORE_ENVIRONMENT);
        public static string AZURE_TENANT_ID => nameof(AZURE_TENANT_ID);
        public static string AZURE_CLIENT_ID => nameof(AZURE_CLIENT_ID);
        public static string AZURE_CLIENT_SECRET => nameof(AZURE_CLIENT_SECRET);
        public static string AZURE_KEYVAULT_NAME => nameof(AZURE_KEYVAULT_NAME);
    }
}
