using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Api.Domain
{
    public class RefundInfo
    {
        protected RefundInfo()
        {

        }
        public int RefundId { get; set; }
        public decimal RefundAmount { get; set; }
        public string RefundDesc { get; set; }
        public string Refunder { get; set; }
        public DateTime RefundDate { get; set; }
    }
}
