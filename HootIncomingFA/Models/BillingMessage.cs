using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HootIncomingFA.Models
{
    public class BillingMessage
    {
        [JsonPropertyName("oppId")]
        public string OppId { get; set; }
        [JsonPropertyName("soId")]
        public string SoId { get; set; }
        [JsonPropertyName("bilId")]
        public string BilId { get; set; }
        [JsonPropertyName("biliId")]
        public string BiliId { get; set; }
        [JsonPropertyName("rsId")]
        public string RevId { get; set; }
        [JsonPropertyName("rsiId")]
        public string ReviId { get; set; }
        [JsonPropertyName("splitbill")]
        public string SplitBill { get; set; }
        [JsonPropertyName("sfReturnIds")]
        public string[] SfReturnIds { get; set; }
        //[JsonPropertyName("splitBillRules")]
        //public List<SplitBillRule> SplitBillRules { get; set; }
        [JsonPropertyName("packageGuid")]
        public string PackageGuid { get; set; }
        [JsonPropertyName("adbaseData")]
        public AdbaseData AdbaseData { get; set; }
    }
}
