using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Inlook_API.Controllers;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;
using Inlook_Infrastructure;
using Inlook_Infrastructure.Services;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace XUnitTests.Controllers
{
    public partial class ControllerTest
    {
        [Fact]
        public async Task PostNotificationTest_NotNull()
        {
            var postNotificationModel = new PostNotificationModel()
            {
                Content = "Notification Test",
                RecipientsList = new string[] { this.userId.ToString() },
                WithAttachments = false,
                ContentType = "string",
            };

            await this.notificationService.SendNotificationAsync(postNotificationModel);
        }
        [Fact]
        public void GetNotificationTest_NotNull()
        {
            var result = this.notificationService.GetNotification(this.userId);
            Assert.NotNull(result);
        }
    }
}
