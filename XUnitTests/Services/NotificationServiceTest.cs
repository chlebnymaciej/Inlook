using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inlook_Core.Models;
using Xunit;

namespace XUnitTests.Services
{
    public partial class ServiceTests
    {
        [Fact]
        public async Task SendNotificationTest()
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
    }
}
