using System;
using System.Collections.Generic;
using System.Linq;
using Grpc.Core;
using MagicOnion.Common.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MagicOnion.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            // 然后你就可以根据IP和端口拿到对于的服务
            var channel = new Channel("192.168.1.8", 8080, ChannelCredentials.Insecure);
            var client = MagicOnionClient.Create<ITestService>(channel);
            var reply = client.GetStudent(1); ;
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
