using Inlook_Core.Entities;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inlook_Infrastructure.Services
{
    public class UserService : BaseService<User>, IUserService
    {

        public UserService(Inlook_Context context) : base(context)
        {
        }

        public IEnumerable<GetMailModel> GetMails(Guid toId)
        {
            var users = this.context.Users;

            User user = (users.Where(x => x.Id == toId)
                .Include(mt => mt.MailsReceived)
                .ThenInclude(mr => mr.Mail)
                .ThenInclude(m => m.Sender)
                .Include(mt => mt.MailsReceived)
                .ThenInclude(mr => mr.Mail)
                .ThenInclude(m => m.Recipients)
                .ThenInclude(mt => mt.Recipient)).FirstOrDefault();

            List<GetMailModel> mails = new List<GetMailModel>();
            var mailsTo = user.MailsReceived;
            List<MailTo> BCC = new List<MailTo>();
            foreach (var item in mailsTo)
            {
                if(item.CC.HasValue && item.CC.Value == false)
                    BCC.Add(item);
            }
            List<MailTo> rest = new List<MailTo>();
            foreach (var item in mailsTo)
            {
                if (item.CC.HasValue==false || item.CC.Value == true)
                    rest.Add(item);
            }

            foreach (MailTo item in BCC)
            {
                var toTmp = new List<GetUserWithIdModel>() { new GetUserWithIdModel()
                    {
                        Email = user.Email,
                        Name = user.Name,
                        Id = user.Id
                    }
                    };
                var empty = new List<GetUserWithIdModel>();
                GetMailModel tmp = new GetMailModel()
                {
                    From = new GetUserWithIdModel()
                    {
                        Email = item.Mail.Sender.Email,
                        Id = item.Mail.Sender.Id,
                        Name = item.Mail.Sender.Name
                    },
                    CC = empty.ToArray(),
                    Read = item.StatusRead,
                    Subject = item.Mail.Subject,
                    Text = item.Mail.Text,
                    SendTime = item.Mail.CreatedDate,
                    To = toTmp.ToArray(),
                    MailId = item.MailId
                };
                mails.Add(tmp);
            }

            foreach (var item in rest)
            {
                var toTmp = new List<GetUserWithIdModel>();
                foreach (var recipment in item.Mail.Recipients)
                {
                    if(recipment.CC.HasValue==false)
                        toTmp.Add(
                            new GetUserWithIdModel()
                            {
                                Email = recipment.Recipient.Email,
                                Name = recipment.Recipient.Name,
                                Id = recipment.Recipient.Id
                            }
                            );
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
                                 Id = recipment.Recipient.Id
                             }
                            );
                }

                GetMailModel tmp = new GetMailModel()
                {
                    From = new GetUserWithIdModel()
                    {
                        Email = item.Mail.Sender.Email,
                        Id = item.Mail.Sender.Id,
                        Name = item.Mail.Sender.Name
                    },
                    CC = ccTmp.ToArray(),
                    Read = item.StatusRead,
                    Subject = item.Mail.Subject,
                    Text = item.Mail.Text,
                    SendTime = item.Mail.CreatedDate,
                    To = toTmp.ToArray(),
                    MailId = item.MailId
                };
                mails.Add(tmp);

            }
            return mails;
        }

        public IEnumerable<User> ReadAllUsers()
        {
            return this.context.Users;
        }
    }
}
