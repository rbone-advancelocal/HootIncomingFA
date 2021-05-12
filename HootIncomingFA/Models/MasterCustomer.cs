using System;
using System.Collections.Generic;
using System.Text;

namespace HootIncomingFA.Models
{
    public class MasterCustomer
    {
        public string IsPrimaryOrderer { get; set; }
        public string IsPrimaryPayor { get; set; }
        public Customer Customer { get; set; }
    }
}
