using System;
using System.IO;
using System.Threading.Tasks;
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

        public static async Task<CoffeeDataChangedEvent> CreateFromBrokeredMessage(BrokeredMessage message)
        {
            using (var reader = new StreamReader(message.GetBody<Stream>()))
            {                
                return await JsonConvert.DeserializeObjectAsync<CoffeeDataChangedEvent>(await reader.ReadToEndAsync());                
            }
        }
    }
}