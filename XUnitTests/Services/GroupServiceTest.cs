using Inlook_Core.Models;
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
        // All in one, becouse I work on the same attachment
        public async Task GroupTest()
        {
            string groupName = "TestGroup";
            var groupModel = new PostGroupModel()
            {
                Name = groupName,
                Users = new string[] {userId.ToString()},
            };
            groupService.AddGroup(groupModel, userId);

            var groups = groupService.GetAllGroups(userId);
            var group = groups.Where(g => g.Name == groupName).FirstOrDefault();

            Assert.NotNull(group);

            string editedName = groupName + "Edited";
            var updateGroupModel = new UpdateGroupModel()
            {
                Id = group.Id.ToString(),
                Name = editedName,
                Users = new string[0] ,
            };
            groupService.UpdateGroup(updateGroupModel, userId);

            groups = groupService.GetAllGroups(userId);
            group = groups.Where(g => g.Name == editedName).FirstOrDefault();

            Assert.NotNull(group);
            Assert.True(group.Name == editedName);
            Assert.True(group.UserGroups.Count == 0);

        }

    }
}
