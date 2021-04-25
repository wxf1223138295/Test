using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Api.Domain
{
    public class MainOrder
    {
        public MainOrder(int orderid,string orderName,decimal orderSumAmount,Address address,int orderState,int paymentState,DateTime orderDateTime,DateTime paymentDateTime,int buyid,List<OrderItem> _items)
        {
            this.OrderId = orderid;
            this.OrderName = orderName;
            this.OrderDateTime = orderDateTime;
            this.OrderState = orderState;
            this.Items=new List<OrderItem>();
            this.Items = _items;
            this.OrderSumAmount = orderSumAmount;
            this.PaymentDateTime = paymentDateTime;
            this.PaymentState = paymentState;
            this.Address = address;
        }

        protected MainOrder()
        {

        }

       public int OrderId { get; set; }
       public string OrderName{ get; set; }
       public decimal OrderSumAmount { get; set; }
       public int OrderState { get; set; }

       public int PaymentState { get; set; }
       public DateTime OrderDateTime { get; set; }
       public DateTime PaymentDateTime { get; set; }
       public int? BuyId { get; set; }

       public List<OrderItem> Items { get; set; }
       public Address Address { get;  set; }
    }

    public class Address
    {
        public String Street { get;  set; }
        public String City { get;  set; }
        public String State { get;  set; }
        public String Country { get;  set; }
        public String ZipCode { get;  set; }
    }
    public class OrderItem
    {
        public int ItemId { get; set; }
        public string OrderItemName { get; set; }
        public int Count { get; set; }
        public decimal Amount { get; set; }
    }
}
