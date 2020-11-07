using Inlook_Core.Entities;
using Inlook_Core.Interfaces.Services;
using System;
using System.Linq;
using System.Text;

namespace Inlook_Infrastructure.Services
{
    public class GroupService : BaseService<Group>, IGroupService
    {
        public GroupService(Inlook_Context context) : base(context)
        {
        }
    }
}
