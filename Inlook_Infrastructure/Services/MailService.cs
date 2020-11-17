using Inlook_Core.Entities;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Infrastructure.Services
{
    public class MailService : BaseService<Mail>, IMailService
    {
        public MailService(Inlook_Context context) : base(context)
        {
        }

        public void SendMail(PostMailModel mail, Guid ownerId)
        {
            Guid id = Guid.NewGuid();
            ICollection<MailTo> recipients = new HashSet<MailTo>();
            Guid groupId = Guid.NewGuid();

            foreach (string item in mail.To)
            {
                recipients.Add(new MailTo()
                { 
                    RecipientId = Guid.Parse(item),
                    MailId = groupId,
                    CC = false,
                    StatusRead= false
                });
            }
            foreach (string item in mail.CC)
            {
                recipients.Add(new MailTo()
                {
                    RecipientId = Guid.Parse(item),
                    MailId = groupId,
                    CC = true,
                    StatusRead = false
                });
            }
            Mail mailEnt = new Mail()
            {
                Id = id,
                SenderId = ownerId,
                Subject = mail.Subject,
                Text = mail.Text,
                Recipients = recipients
            };

            Create(mailEnt);
        }
    }
}
