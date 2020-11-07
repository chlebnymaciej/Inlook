using Inlook_API.Extensions;
using Inlook_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inlook_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UserPolicy")]
    public class MailController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public MailController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult PostMail([FromBody] Mail mail)
        {
            var userId = this.GetUserId();
            // send mail with userId as sender
            return NoContent();
        }
    }
}

