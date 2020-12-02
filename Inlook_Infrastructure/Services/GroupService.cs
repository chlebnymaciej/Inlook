using Inlook_Core.Entities;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inlook_Infrastructure.Services
{
    public class GroupService : BaseService<Group>, IGroupService
    {

        public GroupService(Inlook_Context context) : base(context)
        {
        }

        public void AddGroup(PostGroupModel model, Guid ownerId)
        {
            ICollection<UserGroup> userGroups = new HashSet<UserGroup>();
            Guid groupId = Guid.NewGuid();

            foreach (string item in model.Users)
            {
                userGroups.Add(new UserGroup() {UserId=Guid.Parse(item),GroupId=groupId});
            }

            Group group = new Group()
            {
                Id = groupId,
                Name = model.Name,
                GroupOwnerId = ownerId,
                UserGroups = userGroups
            };
            Create(group);
        }


        public IEnumerable<Group> GetAllGroups(Guid UserId)
        {
            var groups = this.context.Groups;
            return groups.Where(x => x.GroupOwnerId == UserId)
                .Include(g => g.UserGroups)
                .ThenInclude(u=>u.User);
        }

        public void UpdateGroup(UpdateGroupModel model, Guid ownerId)
        {
            ICollection<UserGroup> userGroups = new HashSet<UserGroup>();
            Guid groupId = Guid.Parse(model.Id);

            foreach (string item in model.Users)
            {
                userGroups.Add(new UserGroup() { UserId = Guid.Parse(item), GroupId = groupId });
            }

            Group group = new Group()
            {
                Id = groupId,
                Name = model.Name,
                GroupOwnerId = ownerId,
                UserGroups = userGroups
            };
            //Update(group);

            var groups = this.context.Groups.Where(x => x.GroupOwnerId == ownerId && x.Id == groupId)
                                            .Include(g => g.UserGroups);

            var groupToUpdate = groups.FirstOrDefault();
            groupToUpdate.Name = model.Name;
            groupToUpdate.UserGroups = userGroups;
            this.context.SaveChanges();
        }
    }
}
