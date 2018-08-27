using signlarexample.Models;

namespace signlarexample.Services
{
    public interface IPushNotificationService
    {
         void SendNotification(string payload);
    }
}