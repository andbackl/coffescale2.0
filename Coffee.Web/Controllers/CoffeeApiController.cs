using System;
using System.Web.Http;
using Coffee.Web.Hubs;
using Microsoft.AspNet.SignalR;

namespace Coffee.Web.Controllers
{
    public class CoffeeApiController : ApiController
    {
        public void Post(CoffeeDataChangedEvent item)
        {
            GlobalHost.ConnectionManager.GetHubContext<CoffeeHub>().Clients.All.CoffeeDataChanged(item);
        }

        public void Put(CoffeeDataTick tick)
        {
            GlobalHost.ConnectionManager.GetHubContext<CoffeeHub>().Clients.All.CoffeeDataTick(tick);
        }
    }

    public class CoffeeDataTick
    {
        public string StateName { get; set; }
        public long ElapsedTime { get; set; }
        public int Weight { get; set; }
    }

    public class CoffeeDataChangedEvent
    {
        public string SerialNumber { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public int Weight { get; set; }      
    }
}
