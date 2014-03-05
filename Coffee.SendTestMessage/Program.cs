using System;
using System.Configuration;
using System.IO;
using System.Text;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace Coffee.SendTestMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            var topicClient = TopicClient.CreateFromConnectionString(ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"], "scaleevents");

            var coffeeDataChangedEvent = new CoffeeDataChangedEvent
            {
                SerialNumber = args.Length > 1 ? args[1] : "unknown", 
                Date = DateTime.UtcNow, 
                Status = 4, 
                Weight = Convert.ToInt32(args[0])
            };

            var messageBodyStream = new MemoryStream(Encoding.Default.GetBytes(JsonConvert.SerializeObject(coffeeDataChangedEvent)));
            topicClient.Send(new BrokeredMessage(messageBodyStream, true) { Label = "dataChanged"});
            Console.WriteLine("Message sent");
        }
    }

    public class CoffeeDataChangedEvent
    {
        public string SerialNumber { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public int Weight { get; set; }
    }
}
