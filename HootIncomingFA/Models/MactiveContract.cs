using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HootIncomingFA.Models
{
    public class MactiveContract
    {
        public string ApiKey { get; set; }
        [JsonPropertyName("outputEnvironment")]
        public string OutputEnvironment { get; set; }
        [JsonPropertyName("billingMessage")]
        public BillingMessage BillingMessage { get; set; }
    }
}
