using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inlook_Core;
using Inlook_Core.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace XUnitTests.Services
{
    public partial class ServiceTests
    {
        [Fact]
        public void GetMailsTest()
        {
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

            var mails = this.userService.GetMails(this.userId);

            Assert.True(mails.Any());
            int realCount = this.dbContext.Mails
                .Include(m => m.Recipients)
                .Where(m => m.Recipients.Any(r => r.RecipientId == this.userId))
                .Count();
            Assert.True(mails.Count() == realCount);
        }

        [Fact]
        public void ReadAllUsersTest()
        {
            var users = this.userService.ReadAllUsers();
            Assert.Contains(users, u => u.Id == this.userId);
            Assert.True(users.Count() == this.dbContext.Users.Count());
        }

        [Fact]
        public async Task UserRolesTest()
        {
            await this.userService.UnassignRoleToUser(Roles.Admin, this.userId);
            await this.userService.UnassignRoleToUser(Roles.User, this.userId);
            await this.userService.UnassignRoleToUser(Roles.Pending, this.userId);

            var roles = this.userService.ReadUserRoles(this.userId);

            Assert.Empty(roles);

            await this.userService.AssignRoleToUser(Roles.Admin, this.userId);
            await this.userService.AssignRoleToUser(Roles.User, this.userId);
            await this.userService.AssignRoleToUser(Roles.Pending, this.userId);

            roles = this.userService.ReadUserRoles(this.userId);

            Assert.Contains(Roles.Admin, roles);
            Assert.Contains(Roles.User, roles);
            Assert.Contains(Roles.Pending, roles);
        }
    }
}
