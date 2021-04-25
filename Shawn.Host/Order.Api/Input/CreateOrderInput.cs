using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Api.Input
{
    public class CreateOrderInput
    {
       public int OrderId { get; set; }
       public string OrderName { get; set; }
       public List<OrderItemInput> ListItem { get; set; }
    }

    public class OrderItemInput
    {
        public string OrderItemName { get; set; }
        public int Count { get; set; }
        public decimal Amount { get; set; }
    }


    public class OrderCreateEvent
    {
        public decimal PayDecimal { get; set; }
        public string PayedDesc { get; set; }
        public string Payer { get; set; }
        public int OrderId { get; set; }
    }
}
