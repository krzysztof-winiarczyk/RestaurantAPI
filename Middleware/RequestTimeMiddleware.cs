using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantAPI.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimeMiddleware> _logger;
        private Stopwatch _stopwatch;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _logger = logger;
            _stopwatch = new Stopwatch();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwatch.Restart();
            await next.Invoke(context);
            _stopwatch.Stop();
            long time = _stopwatch.ElapsedMilliseconds;

            if (time > 4000)
            {
                string message = context.Request.Method.ToString()
                    + " at " + context.Request.Path.ToString()
                    + " | time of response: " + time.ToString();
                _logger.LogInformation(message);
            }
        }
    }
}
