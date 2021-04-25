using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Payment.Api.Domain;
using Payment.Api.Input;
using Payment.Api.PaymentDb;

namespace Payment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private PaymentDbContext _paymentDbContext;
        private readonly ICapPublisher _capBus;
        private ILogger<PaymentController> _logger;

        public PaymentController(PaymentDbContext paymentDbContext, ICapPublisher capBus, ILogger<PaymentController> logger)
        {
            _paymentDbContext = paymentDbContext;
            _capBus = capBus;
            _logger = logger;
        }


        [CapSubscribe("Order.services.Payed")]
        public async Task<IActionResult> Payed(PayedInput input)
        {
            PayedInfo info=new PayedInfo(input.PayedDesc,input.Payer,DateTime.Now, input.PayDecimal,input.OrderId);


            using (var transcation= _paymentDbContext.Database.BeginTransaction(_capBus,true))
            {
                try
                {
                    await _paymentDbContext.AddAsync(info);

                   
                    _capBus.Publish("Payment.services.Payed", info);
                    var result = await _paymentDbContext.SaveChangesAsync();

                   // transcation.Commit();
                }
                catch (Exception e)
                {
                    await transcation.RollbackAsync();
                    return BadRequest();
                }
                return Ok();
            }

        }
        
    }
}