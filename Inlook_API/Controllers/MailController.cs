using Inlook_API.Extensions;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Inlook_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UserPolicy")]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly IUserService _userService;
        public MailController(IMailService mailService, IUserService userService)
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

        [HttpGet("GetMail")]
        public IActionResult GetMail()
        {
            var userId = this.GetUserId();
            var mails = _userService.GetMails(userId);
            return new JsonResult(mails);
        }

        [HttpPut("ReadMailStatus")]
        public IActionResult ReadMailStatus([FromBody] string MailId)
        {
            var userId = this.GetUserId();
            Guid mailId = Guid.Parse(MailId);
            this._mailService.SetRead(mailId, userId);
            return NoContent();
        }

        [HttpPut("UnreadMailStatus")]
        public IActionResult UnreadMailStatus([FromBody] string MailId)
        {
            var userId = this.GetUserId();
            Guid mailId = Guid.Parse(MailId);
            this._mailService.SetRead(mailId, userId);
            return NoContent();
        }
    }
}

