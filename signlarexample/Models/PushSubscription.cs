using System.Collections.Generic;

namespace signlarexample.Models
{
    public class PushSubscription
    {
        public long Id { get; set; }
        public string Endpoint { get; set; }
        public string P256 { get; set; }
        public string Auth { get; set; }
    }
}