using Inlook_API.Extensions;
using Inlook_Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

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

        [HttpPost("SendMail")]
        public IActionResult PostMail([FromBody] PostMailModel mail)
        {
            var userId = this.GetUserId();
            return NoContent();
        }
    }
}

