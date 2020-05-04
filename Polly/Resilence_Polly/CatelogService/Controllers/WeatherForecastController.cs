using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherService;

namespace CatelogService.Controllers
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
        public IEnumerable<WeatherForecast> Get(Guid requestId)
        {
           //System.Diagnostics.Debugger.Launch();

            System.Diagnostics.Debug.WriteLine("WS- Executing WeatherService API with ReqId # " + requestId.ToString());
            _logger.LogInformation("Executing WeatherService API with Client ReqId # " + requestId.ToString());

            ApplyJammerSometime(requestId);

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        private void ApplyJammerSometime(Guid requestId)
        {
            if(Jammer.IsJam())
            {
                System.Diagnostics.Debug.WriteLine("WS- Executing WeatherService API with ReqId # " + "...Failing");

                throw new Exception("Something bad happend to the service...");
            }
        }
    }
}
