using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        
        private readonly IWeatherForecastService _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _logger = logger;
            _service = service;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var result = _service.GetForecast();
        //    return result;
        //}

        //[HttpGet("currentDay/{max}")]
        //public IEnumerable<WeatherForecast> Get2([FromQuery]int take, [FromRoute]int max)
        //{
        //    var result = _service.GetForecast();
        //    return result;
        //}

        //[HttpPost]
        //public ActionResult<string> Hello([FromBody] string name)
        //{
        //    //option 1
        //    //HttpContext.Response.StatusCode = 401;
        //    //return $"Hello {name}";

        //    //option 2
        //    //return StatusCode(401, $"Hello {name}");

        //    //option 3
        //    return NotFound($"Hello {name}");

        //}

        [HttpPost("generate")]
        public ActionResult<IEnumerable<WeatherForecast>> Post([FromQuery]int count, [FromBody]Tuple<int, int> range)
        {
            if (range.Item1 >= range.Item2)
            { 
                return StatusCode(400, null); 
            }
            else if (count < 1)
            {
                return StatusCode(400, null);
            }
            else
            {
                var result = _service.GetForecast(count, range.Item1, range.Item2);
                return StatusCode(200, result);
            }
        }
    }
}
