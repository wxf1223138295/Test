using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace testclient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : ControllerBase
    {

        [HttpGet("Test1")]
        public async Task<IActionResult> Test1()
        {

            await Task.Delay(2000);
            Console.WriteLine($"+  " + Thread.CurrentThread.ManagedThreadId + "");
            await Task.Run<string>(() => { return Tesd(); });

            Console.WriteLine("+  " + Thread.CurrentThread.Name);
            Console.WriteLine("+  " + Thread.CurrentThread.ManagedThreadId);



            return Ok("好了");

        }
        [HttpGet("Test2")]
        public async Task<IActionResult> Test2()
        {
            await Task.Delay(2000);
            Console.WriteLine("_  " + Thread.CurrentThread.Name);
            Console.WriteLine("_  " + Thread.CurrentThread.ManagedThreadId);
            await Task.FromResult(Tesd());
            Console.WriteLine("_  " + Thread.CurrentThread.Name);
            Console.WriteLine("_  " + Thread.CurrentThread.ManagedThreadId);
            return Ok("好了");
        }
        private string Tesd()
        {
            Thread.Sleep(10000);

            Console.WriteLine("|  " + Thread.CurrentThread.Name);
            Console.WriteLine("|  " + Thread.CurrentThread.ManagedThreadId);
            return "3";
        }
    }
}