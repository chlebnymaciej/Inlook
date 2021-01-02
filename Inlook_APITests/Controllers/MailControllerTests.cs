using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inlook_API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Inlook_Infrastructure.Services;
using Inlook_Core.Models;

namespace Inlook_API.Controllers.Tests
{
    [TestClass()]
    public class MailControllerTests
    {
        [TestMethod()]
        public void PostMailTest()
        {
            ILogger<MailController> logger = new Logger<MailController>(new LoggerFactory());
            MailController mailController = new MailController(logger, new Microsoft.ApplicationInsights.TelemetryClient(), new MailService(new Inlook_Infrastructure.Inlook_Context()), new UserService(new Inlook_Infrastructure.Inlook_Context()));
            Assert.Fail();
           
        }

        [TestMethod()]
        public void GetMailsTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PostMailTest1()
        {
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void ReadMailStatusTest1()
        {
            ILogger<MailController> logger = new Logger<MailController>(new LoggerFactory());
            MailController mailController = new MailController(logger, new Microsoft.ApplicationInsights.TelemetryClient(), new MailService(new Inlook_Infrastructure.Inlook_Context()), new UserService(new Inlook_Infrastructure.Inlook_Context()));

            PutMailStatusModel putMailModel = new PutMailStatusModel();
            Guid guid = new Guid();
            putMailModel.MailId = guid;
            putMailModel.Read = false;
            var resutl = mailController.ReadMailStatus(putMailModel);


            Assert.AreNotEqual(resutl, mailController.ReadMailStatus(putMailModel));
        }
    }

}