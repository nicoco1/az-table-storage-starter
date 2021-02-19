using System.Collections.Generic;
using Microsoft.Azure.Cosmos.Table;

namespace AzTableStorage.Domain.Users
{
    public class User : TableEntity
    {
        public User() { }

        public User(string userId, string email)
        {
            PartitionKey = userId;
            RowKey = email;
        }

        public string Id => RowKey;
        public string Email => PartitionKey;
        public string FullName { get; set; }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            FullName = properties["full_name"].StringValue;
        }
    }
}