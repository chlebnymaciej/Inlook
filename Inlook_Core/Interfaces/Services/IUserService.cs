﻿using Inlook_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Core.Interfaces.Services
{
    public interface IUserService : IBaseService<User>
    {
        IEnumerable<User> ReadAllUsers();
    }
}
