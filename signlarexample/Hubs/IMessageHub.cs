using System.Threading.Tasks;

namespace signlarexample.Hubs
{
    public interface IMessageHub
    {
         Task SendMessage(string user, string message);
         Task PushServerEvent(string message);
    }
}