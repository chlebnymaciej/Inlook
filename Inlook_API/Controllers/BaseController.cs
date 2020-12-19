using Inlook_API.Extensions;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inlook_API.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogger _logger;
        protected readonly TelemetryClient _telemetryClient;
        public BaseController(ILogger logger, TelemetryClient telemetryClient)
        {
            this._logger = logger;
            this._telemetryClient = telemetryClient;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("REQUEST: ");
            sb.Append(context.HttpContext.Request.GetDetails());
            sb.Append("\nRESPONSE: ");
            sb.Append(context.HttpContext.Response.Body.ToString());

            _logger.LogInformation(sb.ToString());

            var dic = new Dictionary<string, string>();
            dic.Add("messagge", sb.ToString());
            this._telemetryClient.TrackEvent("RequestEvent",dic);

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }
    }
}

