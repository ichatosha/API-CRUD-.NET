using System.Diagnostics;

namespace WebAPI
{

    // how much time the request takes
    public class ProfilingMiddleWare
    {
        private readonly RequestDelegate _next;
        private ILogger<ProfilingMiddleWare> _logger;

        public ProfilingMiddleWare(RequestDelegate next, ILogger<ProfilingMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        // async + await => wait to complete _next(context) and continue 
        // if 
        public async Task Invoke(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            //wait to finish this step and then continue
            await _next(context);
            stopwatch.Stop();
            _logger.LogInformation($"The request {context.Request.Path} took '{stopwatch.ElapsedMilliseconds}ms' to execute");
        }

    }
}
