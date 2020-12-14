using Inlook_API.Extensions;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;
using Inlook_Infrastructure.Services;
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
            NotificationController notificationController = new NotificationController(new NotificationService());
            var userId = this.GetUserId();
            PostNotificationModel postNotificationModel = new PostNotificationModel();
            postNotificationModel.Content = mail.Text;
            postNotificationModel.ContentType = "string";
            postNotificationModel.RecipientsList = mail.To;
            postNotificationModel.WithAttachments = mail.Attachments.Length > 0 ? true : false;
                var result = notificationController.PostNotification(postNotificationModel);
            _mailService.SendMail(mail, userId);
            return NoContent();
        }

        [HttpGet("GetMails")]
        public IActionResult GetMails()
        {
            
            var userId = this.GetUserId();
            NotificationService notificationService = new NotificationService();
            notificationService._userID = userId;
            NotificationController notificationController = new NotificationController(notificationService);
            var result = notificationController.GetNotification(userId);
            //Console.WriteLine(result.ToString());
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

