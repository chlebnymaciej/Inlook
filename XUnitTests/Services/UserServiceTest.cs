using Inlook_Core;
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
        public void GetMailsTest()
        {
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

            var mails = userService.GetMails(userId);

            Assert.True(mails.Any());
            int realCount = Inlook_Context.Mails
                .Include(m => m.Recipients)
                .Where(m => m.Recipients.Any(r => r.RecipientId == userId))
                .Count();
            Assert.True(mails.Count() == realCount);
        }

        [Fact]
        public void ReadAllUsersTest()
        {
            var users = userService.ReadAllUsers();
            Assert.Contains(users, u => u.Id == userId);
            Assert.True(users.Count() == Inlook_Context.Users.Count());
        }

        [Fact]
        public async Task UserRolesTest()
        {
            await  userService.UnassignRoleToUser(Roles.Admin, userId);
            await  userService.UnassignRoleToUser(Roles.User, userId);
            await userService.UnassignRoleToUser(Roles.Pending, userId);

            var roles = userService.ReadUserRoles(userId);

            Assert.Empty(roles);

            await userService.AssignRoleToUser(Roles.Admin, userId);
            await userService.AssignRoleToUser(Roles.User, userId);
            await userService.AssignRoleToUser(Roles.Pending, userId);

             roles = userService.ReadUserRoles(userId);

            Assert.Contains(Roles.Admin, roles);
            Assert.Contains(Roles.User, roles);
            Assert.Contains(Roles.Pending, roles);

        }
    }
}
