using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AuthCode.Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IHttpContextAccessor contextAccessor, ILogger<WeatherForecastController> logger)
        {
            _contextAccessor = contextAccessor;
            _logger = logger;
        }
        [HttpGet]
        [Authorize]
        public IEnumerable<WeatherForecast> Get()
        {

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Authorize]
        [HttpGet("test")]
        public async Task<string> Test()
        {
            var tt=_contextAccessor.HttpContext.Request;

            var prop = await _contextAccessor.HttpContext.AuthenticateAsync();


            return "222";
        }
    }
}
