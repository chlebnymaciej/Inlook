using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inlook_Core.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace XUnitTests.Services
{
    public partial class ServiceTests
    {
        [Fact]
        public void SendMailTest()
        {
            var mails = this.dbContext.Mails
                .Include(m => m.Recipients)
                .Where(
                    m => m.Recipients.Any(r => r.RecipientId == this.userId) &&
                    m.Subject == "Test subject" &&
                    m.Text == "Test text");

            this.dbContext.Mails.RemoveRange(mails);

            var postMailModel = new PostMailModel()
            {
                To = new string[] { this.userId.ToString() },
                CC = Array.Empty<string>(),
                BCC = Array.Empty<string>(),
                Subject = "Test subject",
                Text = "Test text",
                Attachments = Array.Empty<string>(),
            };

            this.mailService.SendMail(postMailModel, this.userId);
            var mail = this.dbContext.Mails
                .Include(m => m.Recipients)
                .First(
                    m => m.Recipients.Any(r => r.RecipientId == this.userId) &&
                    m.Subject == "Test subject" &&
                    m.Text == "Test text");

            Assert.NotNull(mail);
        }
    }
}
