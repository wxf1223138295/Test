using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Order.Api.Domain;
using Order.Api.Input;
using Order.Api.OrderDb;
using Payment.Api.Events;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public OrderDbContext _orderDbContext;
        private readonly ICapPublisher _capBus;
        private ILogger<OrderController> _logger;

        public OrderController(OrderDbContext orderDbContext, ICapPublisher capBus, ILogger<OrderController> logger)
        {
            _orderDbContext = orderDbContext;
            _capBus = capBus;
            _logger = logger;
        }
        [HttpGet]
        public async Task<string> GetOrder()
        {
           var result1=await _orderDbContext.MainOrder.Where(p => p.OrderId == 24455453).FirstOrDefaultAsync();
           var result2 = await _orderDbContext.MainOrder.Where(p => p.OrderId == 24455453).Include(p=>p.Items).FirstOrDefaultAsync();




            return "";
        }

        [HttpPost("draft")]
        public async Task<IActionResult> DraftOrderAsync([FromBody]CreateOrderInput input)
        {
            var sumamount=input.ListItem.Sum(p => p.Amount);
            List<OrderItem> items=new List<OrderItem>();
            input.ListItem.ForEach(p =>
            {
                OrderItem temp=new OrderItem();
                temp.Amount = p.Amount;
                temp.Count = p.Count;
                temp.OrderItemName = p.OrderItemName;
                items.Add(temp);
            });
            Address address = new Address { City = "南通",Country = "正余",State = "青正",Street = "测试",ZipCode = "20033"};

            MainOrder order=new MainOrder(input.OrderId,input.OrderName, sumamount, address, 0, 0, DateTime.Now,DateTime.MinValue,1, items);


            using (var transaction = _orderDbContext.Database.BeginTransaction())
            {
                try
                {

                    OrderCreateEvent eventdata=new OrderCreateEvent
                    {
                        OrderId = order.OrderId,
                        PayDecimal = order.OrderSumAmount,
                        PayedDesc = "支付描述",
                        Payer = "Shawn"
                    };
                    _capBus.Publish("Order.services.Payed", eventdata);


                    await _orderDbContext.AddAsync(order);
                    var result = await _orderDbContext.SaveChangesAsync();

                    

                    transaction.Commit();

                  
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    return BadRequest();
                }

                return Ok();
            }

        }

        [HttpPost("UpdateStateAsync")]
        [CapSubscribe("Payment.services.Payed")]
        public async Task<IActionResult> UpdateStateAsync(PayedEventInfo @event)
        {
            var orderid = @event.OrderId;

            

            var rr=await _orderDbContext.MainOrder.Where(p => p.OrderId == orderid).FirstAsync();

            if (rr == null)
            {
                return BadRequest();
            }

            rr.OrderState = 2;

            await _orderDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}