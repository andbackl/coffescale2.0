using System;
using System.Diagnostics;
using System.Net.Http;
using Coffee.WorkerRole.Messages;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Newtonsoft.Json;

namespace Coffee.WorkerRole.Workers.PushToWeb
{
    public class PushToWebWorker : IWorker
    {
        private SubscriptionClient _serviceBusClient;
        private HttpClient _httpClient;

        public void Run()
        {
            var messageOptions = new OnMessageOptions
            {
                AutoComplete = true,
                MaxConcurrentCalls = 1                
            };

            messageOptions.ExceptionReceived += OnExceptionReceived;            

            _serviceBusClient.OnMessage(OnMessageArrived, messageOptions);
        }

        private async void OnMessageArrived(BrokeredMessage message)
        {
            var dataEvent = await CoffeeDataChangedEvent.CreateFromBrokeredMessage(message);
            await _httpClient.PostAsync("CoffeeDataChanged", new StringContent(await JsonConvert.SerializeObjectAsync(dataEvent)));
        }

        private void OnExceptionReceived(object sender, ExceptionReceivedEventArgs e)
        {
            if (e.Exception != null)
                Trace.WriteLine("Error: " + e.Exception.Message);
        }

        public void OnStart()
        {
            _serviceBusClient = Azure.CreateSubscriptionClient("PushToWeb");
            _httpClient = new HttpClient { BaseAddress = new Uri(CloudConfigurationManager.GetSetting("WebBaseAddress")) };
        }

        public void OnStop()
        {
            _serviceBusClient.Close();
        }
    }
}