using System.Diagnostics;
using Coffee.StateMachine;
using Coffee.WorkerRole.Messages;
using Microsoft.ServiceBus.Messaging;

namespace Coffee.WorkerRole.Workers.StateMachinePushToWeb
{
    public class StateMachineWorker : IWorker
    {
        private SubscriptionClient _serviceBusClient;
        private CoffeeStateMachine _stateMachine;
        

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
            _stateMachine.Update(dataEvent.Weight);
        }

        public void OnStart()
        {
            _serviceBusClient = Azure.CreateSubscriptionClient("StateMachine");            
            _stateMachine = new CoffeeStateMachine(new WebPoster());            
        }

        public void OnStop()
        {
            _serviceBusClient.Close();
            _stateMachine.Active = false;
        }

        private void OnExceptionReceived(object sender, ExceptionReceivedEventArgs e)
        {
            if (e.Exception != null)
                Trace.WriteLine("Error: " + e.Exception.Message);
        }
    }
}