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
        public string Get()
        {
            // 然后你就可以根据IP和端口拿到对于的服务
            var channel = new Channel("localhost", 8800, ChannelCredentials.Insecure);
            var client = MagicOnionClient.Create<ITestService>(channel);
            var reply = client.GetStudent(1);
            return reply.GetAwaiter().GetResult().Msg;
        }
    }
}
