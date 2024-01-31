using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace WebAPI
{

    // to secure from hack (simple)
    public class RateLimitingMiddleWare
    {
        private RequestDelegate _next;

        // to count requests in 10 sec
        private static int _counter = 0;

        //to save every single request date.now
        private static DateTime _lastRequestDate = DateTime.Now;

        public RateLimitingMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            _counter++;
            if (DateTime.Now.Subtract(_lastRequestDate).Seconds > 10)
            {
                _counter = 1;
                _lastRequestDate = DateTime.Now;
                await _next(context);

            }
            else
            {
                if (_counter > 5)
                {
                    _lastRequestDate = DateTime.Now;
                    await context.Response.WriteAsync("Rate limit exceeded");
                }
                else
                {
                    _lastRequestDate = DateTime.Now;
                    await _next(context);
                }
            }
        }


    }
}
