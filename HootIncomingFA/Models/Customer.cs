using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HootIncomingFA.Models
{
    public class Customer
    {
        [JsonPropertyName("accountnumber")]
        public string AccountNumber { get; set; }
    }
}
