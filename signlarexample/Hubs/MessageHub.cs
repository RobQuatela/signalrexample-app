using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace signlarexample.Hubs
{
    public class MessageHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task PushServerEvent(string message)
        {
            await Clients.All.SendAsync("ServerEvent", message);
        }
    }
}