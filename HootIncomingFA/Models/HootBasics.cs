using System;
using System.Collections.Generic;
using System.Text;

namespace HootIncomingFA.Models
{
    public class HootBasics
    {
        public string Payor { get; set; }
        public string Orderer { get; set; }
        public string SoldBy { get; set; }
        public string MonthlySubTotal { get; set; }
        public string SetupSubTotal { get; set; }
        public string VideoTemplatesSubTotal { get; set; }
        public string FinalPrice { get; set; }
        public string Id { get; set; }
        public string Client { get; set; }
        public string BillDate { get; set; }
        public string Environment { get; set; }
        //public string Discounts { get; set; }
        //public string ExtraFees { get; set; }
        //public string TotalAfterDiscounts { get; set; }
        //public string MinimumMonthly { get; set; }
    }
}
