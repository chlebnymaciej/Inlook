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
    public class MailService : BaseService<Mail>, IMailService
    {
        public MailService(Inlook_Context context) : base(context)
        {
        }


        public void SendMail(PostMailModel mail, Guid ownerId)
        {
            Guid id = Guid.NewGuid();
            ICollection<MailTo> recipients = new HashSet<MailTo>();

            foreach (string item in mail.To)
            {
                recipients.Add(new MailTo()
                {
                    RecipientId = Guid.Parse(item),
                    MailId = id,
                    CC = null,
                    StatusRead = false
                });
            }
            foreach (string item in mail.CC)
            {
                recipients.Add(new MailTo()
                {
                    RecipientId = Guid.Parse(item),
                    MailId = id,
                    CC = true,
                    StatusRead = false
                });
            }
            foreach (string item in mail.BCC)
            {
                recipients.Add(new MailTo()
                {
                    RecipientId = Guid.Parse(item),
                    MailId = id,
                    CC = false,
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

            var attachmentsIds = mail.Attachments.Select(a => Guid.Parse(a));
            var attachments = this.context.Attachments.Where(a => attachmentsIds.Contains(a.Id));
            foreach (var attachment in attachments)
            {
                mailEnt.Attachments.Add(attachment);
            }

            Create(mailEnt);
        }

        public void SetRead(Guid mailId, Guid recipmentId, bool read)
        {
            var recipients = this.context.Mails.Where(m => m.Id == mailId).Include(m => m.Recipients).FirstOrDefault().Recipients;
            var mailto = recipients.Where(x => x.RecipientId == recipmentId).FirstOrDefault();
            mailto.StatusRead = read;
            this.context.SaveChanges();
        }
    }
}
