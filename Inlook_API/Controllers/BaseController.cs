using Inlook_API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading.Tasks;

namespace Inlook_API.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogger _logger;
        public BaseController(ILogger logger)
        {
            this._logger = logger;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("REQUEST: ");
            sb.Append(context.HttpContext.Request.GetDetails());
            sb.Append("\nRESPONSE: ");
            sb.Append(context.HttpContext.Response.Body.ToString());

            _logger.LogInformation(sb.ToString());
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

