using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Api.Domain
{
    public class Buyer
    {
        protected Buyer()
        {

        }
        public int BuyId { get; set; }
        public string Name { get;  set; }
        public List<PaymentMethod> PaymentMethods { get; set; }

    }

    public class PaymentMethod
    {
        public int id { get; set; }
        public string CardNumber { get; set; }
        public string SecurityNumber { get; set; }
        public string CardHolderName { get; set; }
        public DateTime Expiration { get; set; }
    }
}
