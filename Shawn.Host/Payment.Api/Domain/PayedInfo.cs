using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Api.Domain
{
    public class PayedInfo
    {
        public PayedInfo(string _payedDesc,string _payer,DateTime _payedDateTime,decimal _payDecimal,int _orderId)
        {
            this.PayDecimal = _payDecimal;
            this.PayedDateTime = _payedDateTime;
            this.PayedDesc = _payedDesc;
            this.Payer = _payer;
            this.OrderId = _orderId;
        }
        protected PayedInfo()
        {

        }
        public int PayedId { get; set; }
        public DateTime PayedDateTime { get; set; }
        public decimal PayDecimal { get; set; }
        public string PayedDesc { get; set; }
        public string Payer { get; set; }
        public int OrderId { get; set; }
    }
}
