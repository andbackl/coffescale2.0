using System;
using System.IO;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace Coffee.WorkerRole.Messages
{
    public class CoffeeDataChangedEvent
    {
        public string SerialNumber { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public int Weight { get; set; }

        public static CoffeeDataChangedEvent CreateFromBrokeredMessage(BrokeredMessage message)
        {
            using (var reader = new StreamReader(message.GetBody<Stream>()))
            {                
                return JsonConvert.DeserializeObject<CoffeeDataChangedEvent>(reader.ReadToEnd());                
            }
        }
    }
}