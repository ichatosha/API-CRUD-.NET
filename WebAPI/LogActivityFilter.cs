using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace WebAPI
{
    public class LogActivityFilter : IActionFilter

    {
        private ILogger<LogActivityFilter> _logger;

        public LogActivityFilter(ILogger<LogActivityFilter> logger)
        {
            _logger = logger;
        }

        // first action 
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"Executing action {context.ActionDescriptor.DisplayName} on controller {context.Controller}");
        }

        // third action after original action (getProduct OR deleteproduct etc..)
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"Action {context.ActionDescriptor.DisplayName} is Excecuted on controller {context.Controller}");
        }

    }
}
