using System;
using System.Collections.Generic;
using System.Linq;
using Grpc.Core;
using MagicOnion.Common.Model.Request;
using MagicOnion.Proxy.Service;
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
            var result = Provider.TestService.GetStudent(new TestRequest()).GetAwaiter();
            var data = result.GetResult().Data;
            var msg = result.GetResult().Msg;
            return msg;
        }
    }
}
