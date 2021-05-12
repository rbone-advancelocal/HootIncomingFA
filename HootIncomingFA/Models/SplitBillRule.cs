using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HootIncomingFA.Models
{
    public class SplitBillRule
    {
        [JsonPropertyName("opptyId")]
        public string OpptyId { get; set; }
        [JsonPropertyName("ordererAcct")]
        public string OrdererAcct { get; set; }
        [JsonPropertyName("payorAcct")]
        public string PayorAcct { get; set; }
        [JsonPropertyName("splitPercent")]
        public string SplitPercent { get; set; }
        [JsonPropertyName("splitTotalValue")]
        public string SplitTotalValue { get; set; }
        [JsonPropertyName("orderTotal")]
        public string OrderTotal { get; set; }
    }
}
