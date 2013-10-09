using System;
using System.Web.Http;
using Coffee.Web.Hubs;
using Coffee.Web.Models;
using Microsoft.AspNet.SignalR;

namespace Coffee.Web.Controllers
{
    public class CoffeeApiController : ApiController
    {
        public void Put(CoffeeDataTick tick)
        {
            GlobalHost.ConnectionManager.GetHubContext<CoffeeHub>().Clients.All.CoffeeDataTick(tick);
        }
    }        
}
