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
                SerialNumber = "1", 
                Date = DateTime.UtcNow, 
                Status = 4, 
                Weight = Convert.ToInt32(args[0])
            };

            var messageBodyStream = new MemoryStream(Encoding.Default.GetBytes(JsonConvert.SerializeObject(coffeeDataChangedEvent)));
            topicClient.Send(new BrokeredMessage(messageBodyStream, true));
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
