using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Coffee.StateMachine;
using Coffee.WorkerRole.Messages;
using Microsoft.ServiceBus.Messaging;

namespace Coffee.WorkerRole.Workers.StateMachinePushToWeb
{
    public class StateMachineWorker : IWorker
    {
        private SubscriptionClient _serviceBusClient;

        private readonly ConcurrentDictionary<string, CoffeeStateMachine> _stateMachines = new ConcurrentDictionary<string, CoffeeStateMachine>(); 


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

            var stateMachine = _stateMachines.GetOrAdd(dataEvent.SerialNumber, new CoffeeStateMachine(new WebPoster(dataEvent.SerialNumber)));
            stateMachine.Update(dataEvent.Weight);
        }

        public void OnStart()
        {
            _serviceBusClient = Azure.CreateSubscriptionClient("StateMachine");            
        }

        public void OnStop()
        {
            _serviceBusClient.Close();
            foreach (var stateMachine in _stateMachines.Values)            
                stateMachine.Active = false;            
        }

        private void OnExceptionReceived(object sender, ExceptionReceivedEventArgs e)
        {
            if (e.Exception != null)
                Trace.WriteLine("Error: " + e.Exception.Message);
        }
    }
}