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
    }

    public class CoffeeDataChangedEvent
    {
        public string SerialNumber { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public int Weight { get; set; }      
    }
}
