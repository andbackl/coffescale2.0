using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Coffee.WorkerRole
{
    public static class Azure
    {
        public static SubscriptionClient CreateSubscriptionClient(string subscription)
        {
            var connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            return SubscriptionClient.CreateFromConnectionString(connectionString, "scaleevents", subscription, ReceiveMode.PeekLock);
        }

        public static CloudTable CreateTable(string tableName)
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            var tableClient = storageAccount.CreateCloudTableClient();
            return tableClient.GetTableReference(tableName);
        } 
    }
}