using System.Threading.Tasks;
using Azure.Security.KeyVault.Secrets;

namespace AzTableStorage.Core.Azure
{
    public interface IKeyVaultAccessor
    {
        Task<string> GetSecret(string secretName);
    }

    public class KeyVaultAccessor : IKeyVaultAccessor
    {
        private readonly SecretClient _secretClient;

        public KeyVaultAccessor(SecretClient secretClient)
        {
            _secretClient = secretClient;
        }

        public async Task<string> GetSecret(string secretName)
        {
            KeyVaultSecret secret = await _secretClient.GetSecretAsync(secretName);

            return secret.Value;
        }
    }
}