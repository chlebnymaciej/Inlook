using System;
using System.Collections.Generic;
using System.Text;
using Inlook_Core.Entities;
using Inlook_Core.Models;

namespace Inlook_Core.Interfaces.Services
{
    public interface IGroupService : IBaseService<Group>
    {
        IEnumerable<Group> GetAllGroups(Guid UserId);

        void AddGroup(PostGroupModel model, Guid ownerId);

        void UpdateGroup(UpdateGroupModel model, Guid ownerId);
    }
}
