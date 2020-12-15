﻿using Inlook_Core.Entities;
using Inlook_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inlook_Core.Interfaces.Services
{
    public interface IUserService : IBaseService<User>
    {
        IEnumerable<User> ReadAllUsers();

        IEnumerable<GetMailModel> GetMails(Guid toId);
        IEnumerable<string> ReadUserRoles(Guid userId);
        Task SetUserAccept(Guid userId, bool accept);
        Task AssignRoleToUser(string role, Guid userId);
        Task UnassignRoleToUser(string role, Guid userId);

    }
}
