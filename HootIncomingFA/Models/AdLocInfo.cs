using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HootIncomingFA.Models
{
    public class AdLocInfo
    {
        [JsonPropertyName("publication")]
        public string Publication { get; set; }
        [JsonPropertyName("publicationplacement")]
        public string PublicationPlacement { get; set; }
        [JsonPropertyName("publicationposition")]
        public string PublicationPosition { get; set; }
        [JsonPropertyName("rundates")]
        public RunDates RunDates { get; set; }
        public List<SpecialPrice> SpecialPrice { get; set; }
    }
}
