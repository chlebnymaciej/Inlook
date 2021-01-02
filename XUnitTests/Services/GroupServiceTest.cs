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

        // All in one, becouse I work on the same attachment
        public void GroupTest()
        {
            string groupName = "TestGroup";
            var groupModel = new PostGroupModel()
            {
                Name = groupName,
                Users = new string[] { this.userId.ToString() },
            };
            this.groupService.AddGroup(groupModel, this.userId);

            var groups = this.groupService.GetAllGroups(this.userId);
            var group = groups.Where(g => g.Name == groupName).FirstOrDefault();

            Assert.NotNull(group);

            string editedName = groupName + "Edited";
            var updateGroupModel = new UpdateGroupModel()
            {
                Id = group.Id.ToString(),
                Name = editedName,
                Users = Array.Empty<string>(),
            };
            this.groupService.UpdateGroup(updateGroupModel, this.userId);

            groups = this.groupService.GetAllGroups(this.userId);
            group = groups.Where(g => g.Name == editedName).FirstOrDefault();

            Assert.NotNull(group);
            Assert.True(group.Name == editedName);
            Assert.True(group.UserGroups.Count == 0);
        }
    }
}
