using Inlook_Core.Entities;
using Inlook_Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Infrastructure.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(Inlook_Context context) : base(context)
        {
        }
    }
}
