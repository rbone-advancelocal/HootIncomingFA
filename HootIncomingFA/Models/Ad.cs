using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HootIncomingFA.Models
{
    public class Ad
    {
        [JsonPropertyName("buyeradid")]
        public string BuyerAdId { get; set; }
        [JsonPropertyName("adtype")]
        public string AdType { get; set; }
        [JsonPropertyName("aduserunit")]
        public string AdUserUnit { get; set; }
        [JsonPropertyName("adwidth")]
        public string AdWidth { get; set; }
        [JsonPropertyName("adheight")]
        public string AdHeight { get; set; }
        [JsonPropertyName("adunitofmeasure")]
        public string AdUnitOfMeasure { get; set; }
        [JsonPropertyName("adslug")]
        public string AdSlug { get; set; }
        [JsonPropertyName("productionmethod")]
        public string ProductionMethod { get; set; }
        public List<AdLocInfo> AdLocInfo { get; set; }
    }
}
