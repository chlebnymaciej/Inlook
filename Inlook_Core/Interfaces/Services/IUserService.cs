using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Inlook_Core.Entities;
using Inlook_Core.Models;

namespace Inlook_Core.Interfaces.Services
{
    public interface IUserService : IBaseService<User>
    {
        IEnumerable<User> ReadAllUsers();

        IEnumerable<GetUserModel> ReadAllContacts();

        IEnumerable<GetAccountModel> ReadAllAccounts();

        IEnumerable<GetMailModel> GetMails(Guid toId);

        IEnumerable<string> ReadUserRoles(Guid userId);

        Task AssignRoleToUser(string role, Guid userId);

        Task UnassignRoleToUser(string role, Guid userId);

        public string GetMail(Guid userId);
    }
}
