namespace AzTableStorage.Shared.Config
{
    public interface IAppSettings
    {
        public string AzureKeyVaultName { get; set; }
        public string AzureADCertThumbprint { get; set; }
        public string AzureADDirectoryId { get; set; }
        public string AzureADApplicationId { get; set; }
    }

    public class AppSettings : IAppSettings
    {
        public string AzureKeyVaultName { get; set; }
        public string AzureADCertThumbprint { get; set; }
        public string AzureADDirectoryId { get; set; }
        public string AzureADApplicationId { get; set; }
    }
}