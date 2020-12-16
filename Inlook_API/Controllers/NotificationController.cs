﻿using Inlook_API.Extensions;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Inlook_API.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Components.Route("[controller]")]
    [Authorize(Policy = "UserPolicy")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService notificationService;

        public NotificationController(INotificationService _notificationService)
        {
            notificationService = _notificationService;
        }

        /// <summary>
        /// Creates new notification
        /// </summary>
        /// <param name="notification">Post notification model</param>
        ///  <response code="204">Success indicator</response>
        [HttpPost("SendNotification")]
        public IActionResult PostNotification([FromBody] PostNotificationModel notification)
        {
            notificationService.sendNotificationAsync(notification);
            return NoContent();
        }

        /// <summary>
        /// Gets notification for given user
        /// </summary>
        /// <param name="userId"></param>
        /// <response code="200">Notification</response>
        [ProducesResponseType(typeof(GetNotificationModel), 200)]
        [HttpGet("GetNotification")]
        public IActionResult GetNotification(Guid userId)
        {
            var notification = notificationService.getNotificationAsync(userId);
            return new JsonResult(notification);
        }

    }
}
