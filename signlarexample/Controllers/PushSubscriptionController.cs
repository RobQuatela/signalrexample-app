using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using signlarexample.Dto;
using signlarexample.Models;
using signlarexample.Services;

namespace signlarexample.Controllers
{
    [Route("api/subscriptions")]
    public class PushSubscriptionController : Controller
    {
        private readonly DatabaseContext context;
        private readonly IPushNotificationService pushService;
        public PushSubscriptionController(DatabaseContext context, IPushNotificationService pushService)
        {
            this.pushService = pushService;
            this.context = context;

        }
        [HttpPost]
        public async Task<IActionResult> SavePushSubscription([FromBody] PushSubscriptionDto sub)
        {
            try
            {
                var subToSave = new PushSubscription()
                {
                    Endpoint = sub.Endpoint,
                    P256 = sub.Keys.ElementAt(0).Value,
                    Auth = sub.Keys.ElementAt(1).Value
                };
                await this.context.PushSubscription.AddAsync(subToSave);
                await this.context.SaveChangesAsync();
                return Ok(sub);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPushSubscriptions()
        {
            var subs = await this.context.PushSubscription.ToListAsync();
            pushService.SendNotification(subs.Count() + " Subscriptions have been retreived");
            return Ok(subs);
        }
    }
}