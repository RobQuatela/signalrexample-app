using System.Linq;
using signlarexample.Models;
using WebPush;

namespace signlarexample.Services
{
    public class WebPushNotificationService : IPushNotificationService
    {
        private readonly WebPushClient pushClient;
        private readonly DatabaseContext context;
        public WebPushNotificationService(DatabaseContext context)
        {
            this.context = context;
            this.pushClient = new WebPushClient();
        }
        public void SendNotification(string payload)
        {
            var subscription = this.context.PushSubscription.FirstOrDefault(x => x.Id == 1);
            var endpoint = subscription.Endpoint;
            var p256dh = subscription.P256;
            var auth = subscription.Auth;

           //VapidDetails vapidDetails = VapidHelper.GenerateVapidKeys();

            var subject = @"mailto:rob.quatela@gmail.com";
            var publicKey = "BME6-YvoCvVwtas9T87mOfqI5ZfuQJiQRq-GaHWUWk3T7xW36gFUNrltXDgRZReOaSwEXq__EIuhH8DU7eQ9ITI";
            var privateKey = "4GCQq4G5QY8eQyPdngx18hfzSMrEuwmURBX1csaybPk";

            var subs = new WebPush.PushSubscription(endpoint, p256dh, auth);
            var vapid = new VapidDetails(subject, publicKey, privateKey);

            pushClient.SendNotification(subs, payload, vapid);
        }
    }
}