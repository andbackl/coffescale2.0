using System.IO;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Coffee.WorkerRole.Workers.HeartbeatMonitor
{
    public class HeartbeatMonitorWorker : IWorker
    {
        private SubscriptionClient _client;        
        private CloudBlockBlob _blob;

        public void Run()
        {
            var messageOptions = new OnMessageOptions
            {                
                MaxConcurrentCalls = 1
            };

            _client.OnMessage(OnMessageArrived, messageOptions);
        }

        private async void OnMessageArrived(BrokeredMessage obj)
        {
            var json = await ReadString(obj);
            await _blob.UploadTextAsync(json);
        }

        public void OnStart()
        {
            var container = Azure.CreateBlobContainer("scale");

            _client = Azure.CreateSubscriptionClient("Heartbeat", ReceiveMode.ReceiveAndDelete);
            _blob = container.GetBlockBlobReference("Heartbeat");
            _blob.Properties.ContentType = "application/json";            
            _blob.SetProperties();
        }

        public void OnStop()
        {
            _client.Close();
        }

        private static async Task<string> ReadString(BrokeredMessage obj)
        {            
            using (var reader = new StreamReader(obj.GetBody<Stream>()))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}