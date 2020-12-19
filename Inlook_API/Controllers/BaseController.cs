using Inlook_API.Extensions;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
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
            sb.Append("REQUEST:\n ");
            sb.Append(context.HttpContext.Request.GetDetails());
            sb.Append("\nRESPONSE:\n ");

            var streamReader = new StreamReader(context.HttpContext.Response.Body);
            string body = streamReader.ReadToEnd();
            sb.Append(body);

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

