using System.Collections.Generic;
using System.Threading.Tasks;
using AzTableStorage.Domain.Users;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzTableStorage.Core.Users
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<List<User>> GetAll();
    }

    public class UserRepository : IUserRepository
    {
        private readonly CloudTable _cloudTable;

        public UserRepository(CloudTable cloudTable)
        {
            _cloudTable = cloudTable;
        }

        public async Task<User> Create(User user)
        {
            var tableOperation = TableOperation.Insert(user);

            return await ExecuteTableOperation(tableOperation);
        }

        public async Task<List<User>> GetAll()
        {
            var tableQuery = new TableQuery<User>();

            return await ExecuteQuerySegmented(tableQuery);
        }

        private async Task<List<User>> ExecuteQuerySegmented(TableQuery<User> tableQuery)
        {
            var results = new List<User>();

            TableContinuationToken tableContinuationToken = null;

            do
            {
                var queryResults = await _cloudTable.ExecuteQuerySegmentedAsync(tableQuery, tableContinuationToken);

                tableContinuationToken = queryResults.ContinuationToken;

                results.AddRange(queryResults.Results);

            } while (tableContinuationToken != null);

            return results;
        }

        private async Task<User> ExecuteTableOperation(TableOperation tableOperation)
        {
            var tableResult = await _cloudTable.ExecuteAsync(tableOperation);
            return (User)tableResult.Result;
        }
    }
}