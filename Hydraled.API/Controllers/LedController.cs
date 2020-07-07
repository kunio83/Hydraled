using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hydraled.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hydraled.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LedController : ControllerBase
    {
        [HttpPost]
        public IEnumerable<WeatherForecast> Get(RgbLedSetting rgbLedSetting)
        {
            
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
