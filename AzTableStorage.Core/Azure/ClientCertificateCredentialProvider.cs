using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AzTableStorage.Shared.Config;
using Azure.Identity;

namespace AzTableStorage.Core.Azure
{
    public interface IClientCertificateCredentialProvider
    {
        ClientCertificateCredential GetCredentials();
    }

    public class ClientCertificateCredentialProvider : IClientCertificateCredentialProvider
    {
        private readonly IAppSettings _appSettings;

        public ClientCertificateCredentialProvider(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public ClientCertificateCredential GetCredentials()
        {
            using var store = new X509Store(StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);

            var certs = store.Certificates.Find(X509FindType.FindByThumbprint, _appSettings.AzureADCertThumbprint, false);
            var certificate = certs.OfType<X509Certificate2>().Single();
            store.Close();

            return new ClientCertificateCredential(_appSettings.AzureADDirectoryId, _appSettings.AzureADApplicationId, certificate);
        }
    }
}