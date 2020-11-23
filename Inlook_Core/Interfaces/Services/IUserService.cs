using Inlook_Core.Entities;
using Inlook_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Core.Interfaces.Services
{
    public interface IUserService : IBaseService<User>
    {
        IEnumerable<User> ReadAllUsers();

        IEnumerable<GetMailModel> GetMails(Guid toId);

    }
}
