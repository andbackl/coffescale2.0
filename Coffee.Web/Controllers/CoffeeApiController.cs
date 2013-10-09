using System.Web.Http;
using Coffee.Web.Hubs;
using Microsoft.AspNet.SignalR;

namespace Coffee.Web.Controllers
{
    public class CoffeeApiController : ApiController
    {
        public void Put(dynamic values)
        {
            GlobalHost.ConnectionManager.GetHubContext<CoffeeHub>().Clients.All.CoffeeDataTick(values);
        }
    }        
}
