using Inlook_Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTests.Services
{
    public partial class ServiceTests
    {

        [Fact]
        public void SendMailTest()
        {
            var mails = Inlook_Context.Mails
                .Include(m => m.Recipients)
                .Where(
                    m => m.Recipients.Any(r => r.RecipientId == userId) &&
                    m.Subject == "Test subject" &&
                    m.Text == "Test text"
                    );

            Inlook_Context.Mails.RemoveRange(mails);

            var postMailModel = new PostMailModel()
            {
                To = new string[] { userId.ToString() },
                CC = new string[0],
                BCC = new string[0],
                Subject = "Test subject",
                Text = "Test text",
                Attachments = new string[0],
            };

            mailService.SendMail(postMailModel, userId);
            var mail = Inlook_Context.Mails
                .Include(m => m.Recipients)
                .First(
                    m => m.Recipients.Any(r => r.RecipientId == userId) &&
                    m.Subject == "Test subject" &&
                    m.Text == "Test text"
                    );

            Assert.NotNull(mail);

        }

    }
}
