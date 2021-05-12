using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HootIncomingFA.Models
{
    public class AdBaseInfo
    {
        public OrderCustomers OrderCustomers { get; set; }

        [JsonPropertyName("purchaseOrder")]
        public string PurchaseOrder { get; set; }
        [JsonPropertyName("package")]
        public string Package { get; set; }
        [JsonPropertyName("notes")]
        public string Notes { get; set; }
        [JsonPropertyName("adsoldby")]
        public string AdSoldBy { get; set; }
        [JsonPropertyName("adordertaker")]
        public string AdOrderTaker { get; set; }
        [JsonPropertyName("invoicetext")]
        public string InvoiceText { get; set; }
        [JsonPropertyName("paginationcode")]
        public string PaginationCode { get; set; }

        public string Company { get; set; }

        public string AdbaseUser { get; set; }
        [JsonPropertyName("billingflag")]
        public string BillingFlag { get; set; }

        //public List<Campaign> Campaign { get; set; }
        public List<Ad> Ad { get; set; }
    }
}
