using Inlook_Core.Entities;
using Inlook_Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inlook_Infrastructure.Services
{
    public class GroupService : BaseService<Group>, IGroupService
    {
        private readonly Inlook_Context context;

        public GroupService(Inlook_Context context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<Group> GetAllGroups(Guid UserId)
        {
            var groups = this.context.Groups;
            return groups.Where(x => x.GroupOwnerId == UserId);
        }
    }
}
