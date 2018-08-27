using System.Collections;
using System.Collections.Generic;

namespace signlarexample.Dto
{
    public class PushSubscriptionDto
    {
        public string Endpoint { get; set; }
        public IDictionary<string, string> Keys { get; set; }
    }
}