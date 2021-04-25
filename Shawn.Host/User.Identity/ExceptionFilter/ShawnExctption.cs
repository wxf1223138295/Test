using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace User.Identity.ExceptionFilter
{
    public class ShawnExctption: IExceptionFilter
    {
        private ILogger _logger;

        public ShawnExctption(ILogger<ShawnExctption> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine("来了老弟");
            context.Result = new ObjectResult(context.Exception.Message);

        }
    }
}
