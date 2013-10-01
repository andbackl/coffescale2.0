using System.Diagnostics;
using Coffee.WorkerRole.Messages;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure.Storage.Table;

namespace Coffee.WorkerRole.LogChangesToTableStorage
{
    public class LogChangesToTableStorageWorker : IWorker
    {
        private SubscriptionClient _client;
        private CloudTable _table;

        public void Run()
        {
            var messageOptions = new OnMessageOptions
            {
                AutoComplete = true, 
                MaxConcurrentCalls = 5
            };

            messageOptions.ExceptionReceived += OnExceptionReceived;

            _client.OnMessage(OnMessageArrived, messageOptions);
        }

        private void OnMessageArrived(BrokeredMessage message)
        {
            var dataEvent = CoffeeDataChangedEvent.CreateFromBrokeredMessage(message);

            var tableEntity = ScaleLogTableEntity.CreateFromEvent(dataEvent);
            _table.Execute(TableOperation.InsertOrReplace(tableEntity));

            message.Complete();
        }

        private void OnExceptionReceived(object sender, ExceptionReceivedEventArgs e)
        {
            if (e.Exception != null)            
                Trace.WriteLine("Error: " + e.Exception.Message);            
        }

        public void OnStart()
        {
            _client = Azure.CreateSubscriptionClient("WeightToTableStorage");
            _table = Azure.CreateTable("ScaleLog");
        }

        public void OnStop()
        {
            _client.Close();
        }     
    }
}