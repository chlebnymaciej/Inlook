using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inlook_API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inlook_Core.Models;
using Inlook_Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inlook_API.Controllers.Tests
{
    [TestClass()]
    public class NotificationControllerTests
    {
        [TestMethod()]
        public void PostNotificationTest()
        {
            //arrange
            NotificationController notificationController = new NotificationController(new NotificationService());
            PostNotificationModel postNotificationModel = new PostNotificationModel
            {
                Content = "Test",
                ContentType = "string",
                RecipientsList = new string[] { "TestMailmailcom","XDD" },
                WithAttachments = false
            };
            //act 
            var result = notificationController.PostNotification(postNotificationModel);
            //asser
            Assert.AreEqual(result, notificationController.PostNotification(postNotificationModel));
        }
        [TestMethod()]
        public void GetNotificationTest()
        {
            //arrange
            NotificationController notificationController = new NotificationController(new NotificationService());
            
            //act 
            var result = notificationController.GetNotification(new Guid());
            //asser
            Assert.AreEqual(result, notificationController.GetNotification(new Guid()));
        }
    }
}