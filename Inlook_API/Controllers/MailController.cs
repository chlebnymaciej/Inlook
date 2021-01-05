using System;
using System.Collections.Generic;
using Inlook_API.Extensions;
using Inlook_API.Helpers;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;
using Inlook_Infrastructure.Services;
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

        public MailController(ILogger<MailController> logger, TelemetryClient telemetryClient, IMailService mailService, IUserService userService)
            : base(logger, telemetryClient)
        {
            this._mailService = mailService;
            this._userService = userService;
        }

        /// <summary>
        /// Creates new mail on server.
        /// </summary>
        /// <param name="mail">Post mail model.</param>
        /// <response code="204">Success indicator.</response>
        [HttpPost("SendMail")]
        public IActionResult PostMail([FromBody] PostMailModel mail)
        {
            NotificationController notificationController = new NotificationController(new NotificationService());
            var userId = this.GetUserId();
            PostNotificationModel postNotificationModel = new PostNotificationModel
            {
                Content = mail.Text,
                ContentType = "string",
                RecipientsList = mail.To,
                WithAttachments = mail.Attachments.Length > 0 ? true : false,
            };
            var result = notificationController.PostNotification(postNotificationModel);
            this._mailService.SendMail(mail, userId);
            return this.NoContent();
        }

        [ProducesResponseType(typeof(List<GetMailModel>), 200)]
        [HttpGet("GetMails")]
        public IActionResult GetMails()
        {
            var userId = this.GetUserId();
            NotificationService notificationService = new NotificationService();
            notificationService._userID = userId;
            NotificationController notificationController = new NotificationController(notificationService);
            var result = notificationController.GetNotification(userId); 
            var mails = this._userService.GetMails(userId);
            return new JsonResult(mails);
        }

        /// <summary>
        /// Set read status of mail.
        /// </summary>
        /// <param name="mailStatus">Post mail status model.</param>
        /// <response code="204">Success indicator.</response>
        [HttpPut("ReadMailStatus")]
        public IActionResult ReadMailStatus([FromBody] PutMailStatusModel mailStatus)
        {
            var userId = this.GetUserId();
            this._mailService.SetRead(mailStatus.MailId, userId, mailStatus.Read);
            return this.NoContent();
        }
    }
}
