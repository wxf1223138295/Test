using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Api.Events
{
    public class PayedEventInfo
    {
        public int PayedId { get; set; }
        public DateTime PayedDateTime { get; set; }
        public decimal PayDecimal { get; set; }
        public string PayedDesc { get; set; }
        public string Payer { get; set; }
        public int OrderId { get; set; }
    }
}
