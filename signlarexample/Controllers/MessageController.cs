using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using signlarexample.Hubs;

namespace signlarexample.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IHubContext<MessageHub> hub;
        public MessageController(IHubContext<MessageHub> hub)
        {
            this.hub = hub;

        }
        [HttpPost]
        public async Task<IActionResult> SendMessageToClient([FromBody] string message)
        {
            await this.hub.Clients.All.SendAsync("ServerEvent", message);
            return Ok();
        }
    }
}