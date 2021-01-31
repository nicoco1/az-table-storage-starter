using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;

namespace AzTableStorage.Core.Azure
{
    public interface IKeyVaultAccessor
    {
        Task<string> GetSecret(string secretUri);
    }

    public class KeyVaultAccessor : IKeyVaultAccessor
    {
        private readonly IKeyVaultClient _keyVaultClient;

        public KeyVaultAccessor(IKeyVaultClient keyVaultClient)
        {
            _keyVaultClient = keyVaultClient;
        }

        public async Task<string> GetSecret(string secretUri)
        {
            var secretBundle = await _keyVaultClient.GetSecretAsync(secretUri);
            return secretBundle.Value;
        }
    }
}