using Inlook_API.Extensions;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inlook_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UserPolicy")]
    public class MailController : BaseController
    {
        private readonly IMailService _mailService;
        private readonly IUserService _userService;
        public MailController(ILogger<MailController> logger, TelemetryClient telemetryClient, IMailService mailService, IUserService userService) : base(logger, telemetryClient)
        {
            _mailService = mailService;
            _userService = userService;
        }

        [HttpPost("SendMail")]
        public IActionResult PostMail([FromBody] PostMailModel mail)
        {
            var userId = this.GetUserId();
            _mailService.SendMail(mail, userId);
            return NoContent();
        }

        [HttpGet("GetMails")]
        public IActionResult GetMails()
        {
            var userId = this.GetUserId();
            var mails = _userService.GetMails(userId);
            return new JsonResult(mails);
        }

        [HttpPut("ReadMailStatus")]
        public IActionResult ReadMailStatus([FromBody] PutMailStatusModel mailStatus)
        {
            var userId = this.GetUserId();
            this._mailService.SetRead(mailStatus.MailId, userId, mailStatus.Read);
            return NoContent();
        }
    }
}

