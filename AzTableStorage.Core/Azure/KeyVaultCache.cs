using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzTableStorage.Core.Azure
{
    public interface IKeyVaultCache
    {
        Task<string> GetCached(string secretName);
    }

    // TODO: cache invalidation

    public class KeyVaultCache : IKeyVaultCache
    {
        private readonly IKeyVaultAccessor _keyVaultAccessor;
        private readonly Dictionary<string, string> _secretsCache = new Dictionary<string, string>();

        public KeyVaultCache(IKeyVaultAccessor keyVaultAccessor)
        {
            _keyVaultAccessor = keyVaultAccessor;
        }

        public async Task<string> GetCached(string secretName)
        {
            if (!_secretsCache.ContainsKey(secretName))
            {
                var secretValue = await _keyVaultAccessor.GetSecret(secretName).ConfigureAwait(false);
                _secretsCache.Add(secretName, secretValue);
            }

            return _secretsCache[secretName];
        }
    }
}