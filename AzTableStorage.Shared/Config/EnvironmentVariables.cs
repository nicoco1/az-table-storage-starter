using System;

namespace AzTableStorage.Shared.Config
{
    public interface IEnvironmentVariables
    {
        string GetValue(string key);
    }

    public class EnvironmentVariables : IEnvironmentVariables
    {
        public string GetValue(string key)
        {
            return Environment.GetEnvironmentVariable(key);
        }
    }
}