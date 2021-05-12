using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HootIncomingFA.Models
{
    public class RunDates
    {
        [JsonPropertyName("date")]
        public string[] Date { get; set; }
    }
}
