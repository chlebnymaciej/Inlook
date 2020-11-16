using Inlook_Core.Entities;
using Inlook_Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Infrastructure.Services
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        private readonly Inlook_Context context;

        public RoleService(Inlook_Context context) : base(context)
        {
            this.context = context;
        }
    }
}
