using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inlook_Core;
using Inlook_Core.Entities;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;
using Inlook_Core.Models.Attachments;
using Microsoft.EntityFrameworkCore;

namespace Inlook_Infrastructure.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(Inlook_Context context)
            : base(context)
        {
        }

        public IEnumerable<GetMailModel> GetMails(Guid toId)
        {
            var users = this.context.Users;

            User user = users.Where(x => x.Id == toId)
                .Include(mt => mt.MailsReceived)
                    .ThenInclude(mr => mr.Mail)
                    .ThenInclude(m => m.Sender)
                .Include(mt => mt.MailsReceived)
                    .ThenInclude(mr => mr.Mail)
                    .ThenInclude(m => m.Recipients)
                    .ThenInclude(mt => mt.Recipient)
                .Include(mt => mt.MailsReceived)
                    .ThenInclude(mr => mr.Mail)
                    .ThenInclude(m => m.Attachments)
            .FirstOrDefault();

            List<GetMailModel> mails = new List<GetMailModel>();
            var mailsTo = user.MailsReceived;
            List<MailTo> BCC = new List<MailTo>();
            foreach (var item in mailsTo)
            {
                if (item.CC.HasValue && item.CC.Value == false)
                {
                    BCC.Add(item);
                }
            }

            List<MailTo> rest = new List<MailTo>();
            foreach (var item in mailsTo)
            {
                if (item.CC.HasValue == false || item.CC.Value == true)
                {
                    rest.Add(item);
                }
            }

            foreach (MailTo item in BCC)
            {
                var toTmp = new List<GetUserWithIdModel>() { new GetUserWithIdModel()
                    {
                        Email = user.Email,
                        Name = user.Name,
                        Id = user.Id
                    },
                    };
                var empty = new List<GetUserWithIdModel>();
                GetMailModel tmp = new GetMailModel()
                {
                    From = new GetUserWithIdModel()
                    {
                        Email = item.Mail.Sender.Email,
                        Id = item.Mail.Sender.Id,
                        Name = item.Mail.Sender.Name,
                    },
                    CC = empty.ToArray(),
                    Read = item.StatusRead,
                    Subject = item.Mail.Subject,
                    Text = item.Mail.Text,
                    SendTime = item.Mail.CreatedDate,
                    To = toTmp.ToArray(),
                    MailId = item.MailId,
                    Attachments = item.Mail.Attachments.Select(a => new GetAttachmentModel() { Id = a.Id, AzureFileName = a.AzureFileName, ClientFileName = a.ClientFileName }).ToArray(),
                };
                mails.Add(tmp);
            }

            foreach (var item in rest)
            {
                var toTmp = new List<GetUserWithIdModel>();
                foreach (var recipment in item.Mail.Recipients)
                {
                    if (recipment.CC.HasValue == false)
                        toTmp.Add(
                            new GetUserWithIdModel()
                            {
                                Email = recipment.Recipient.Email,
                                Name = recipment.Recipient.Name,
                                Id = recipment.Recipient.Id,
                            });
                }

                var ccTmp = new List<GetUserWithIdModel>();

                foreach (var recipment in item.Mail.Recipients)
                {
                    if (recipment.CC.HasValue && recipment.CC == true)
                        ccTmp.Add(
                            new GetUserWithIdModel()
                            {
                                Email = recipment.Recipient.Email,
                                Name = recipment.Recipient.Name,
                                Id = recipment.Recipient.Id,
                            });
                }

                GetMailModel tmp = new GetMailModel()
                {
                    From = new GetUserWithIdModel()
                    {
                        Email = item.Mail.Sender.Email,
                        Id = item.Mail.Sender.Id,
                        Name = item.Mail.Sender.Name,
                    },
                    CC = ccTmp.ToArray(),
                    Read = item.StatusRead,
                    Subject = item.Mail.Subject,
                    Text = item.Mail.Text,
                    SendTime = item.Mail.CreatedDate,
                    To = toTmp.ToArray(),
                    MailId = item.MailId,
                    Attachments = item.Mail.Attachments.Select(a => new GetAttachmentModel() { Id = a.Id, AzureFileName = a.AzureFileName, ClientFileName = a.ClientFileName }).ToArray(),
                };
                mails.Add(tmp);
            }

            return mails;
        }

        public IEnumerable<User> ReadAllUsers()
        {
            return this.context.Users;
        }

        public IEnumerable<GetUserModel> ReadAllContacts()
        {
            return this.context.Users.Select(u => new GetUserModel()
            {
                Email = u.Email,
                Name = u.Name,
                PhoneNumber = u.PhoneNumber,
            }); 
        }

        public IEnumerable<GetAccountModel> ReadAllAccounts()
        {
            return this.context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Select(u => new GetAccountModel()
                {
                    Email = u.Email,
                    Name = u.Name,
                    Id = u.Id,
                    Accepted = u.UserRoles.Any(ur => ur.Role.Name == Roles.User),
                });
            ;
        }

        public IEnumerable<string> ReadUserRoles(Guid userId)
        {
            return this.context.UserRole
                .Where(ur => ur.UserId == userId)
                .Include(ur => ur.Role)
                .Select(ur => ur.Role.Name);
        }

        public async Task AssignRoleToUser(string roleName, Guid userId)
        {
            var role = this.context.Roles.Where(r => r.Name == roleName).FirstOrDefault();
            if (role == null)
            {
                return;
            }

            var user = await this.context.Users.Include(u => u.UserRoles).Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user == null || user.UserRoles.Any(ur => ur.RoleId == role.Id))
            {
                return;
            }

            user.UserRoles.Add(new UserRole() { Role = role });
            await this.context.SaveChangesAsync();
        }

        public string GetMail(Guid userId)
        {
            var user = this.context.Users.Find(userId);
            if (user == null) return "";
            return user.Email;
        }

        public async Task UnassignRoleToUser(string roleName, Guid userId)
        {
            var user = this.context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(r => r.Role)
                .Where(u => u.Id == userId)
                .FirstOrDefault();
            if (user == null)
            {
                return;
            }

            var role = user.UserRoles
                .Where(r => r.Role.Name == roleName)
                .FirstOrDefault();
            if (role == null)
            {
                return;
            }

            user.UserRoles.Remove(role);
            await this.context.SaveChangesAsync();
        }
    }
}
