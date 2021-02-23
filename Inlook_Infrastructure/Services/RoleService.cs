using System;
using System.Collections.Generic;
using System.Text;
using Inlook_Core.Entities;
using Inlook_Core.Interfaces.Services;

namespace Inlook_Infrastructure.Services
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        public RoleService(Inlook_Context context)
            : base(context)
        {
        }
    }
}
