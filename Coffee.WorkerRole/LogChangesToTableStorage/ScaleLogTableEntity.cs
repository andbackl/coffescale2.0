using System;
using Coffee.WorkerRole.Messages;
using Microsoft.WindowsAzure.Storage.Table;

namespace Coffee.WorkerRole.LogChangesToTableStorage
{
    public class ScaleLogTableEntity : TableEntity
    {
        public ScaleLogTableEntity(DateTime timeOfEvent)
        {
            PartitionKey = timeOfEvent.Date.ToString("yyyyMMdd");
            RowKey = (DateTime.MaxValue.Ticks - DateTime.UtcNow.Ticks).ToString("d19");
            TimeOfEventUtc = timeOfEvent;
        }

        public ScaleLogTableEntity()
        {
        }

        public string SerialNumber { get; set; }
        public int WeightInGrams { get; set; }
        public int Status { get; set; }
        public DateTime TimeOfEventUtc { get; set; }

        public static ScaleLogTableEntity CreateFromEvent(CoffeeDataChangedEvent dataEvent)
        {
            return new ScaleLogTableEntity(dataEvent.Date)
            {
                WeightInGrams = dataEvent.Weight,
                Status = dataEvent.Status,
                SerialNumber = dataEvent.SerialNumber
            };            
        }
    }
}