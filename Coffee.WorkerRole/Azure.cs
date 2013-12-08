using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;

namespace Coffee.WorkerRole
{
    public static class Azure
    {
        public static SubscriptionClient CreateSubscriptionClient(string subscription, ReceiveMode receiveMode = ReceiveMode.PeekLock)
        {
            var connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            return SubscriptionClient.CreateFromConnectionString(connectionString, "scaleevents", subscription, receiveMode);
        }

        public static CloudTable CreateTable(string tableName)
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            var tableClient = storageAccount.CreateCloudTableClient();
            return tableClient.GetTableReference(tableName);
        }

        public static CloudBlobContainer CreateBlobContainer(string container)
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            var blobClient = storageAccount.CreateCloudBlobClient();
            var containerReference = blobClient.GetContainerReference(container);
            containerReference.CreateIfNotExists();
            containerReference.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            return containerReference;
        } 
    }
}