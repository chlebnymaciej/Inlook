using System;
using System.Collections.Generic;
using System.Text;
using Inlook_Core.Entities;
using Inlook_Core.Models;

namespace Inlook_Core.Interfaces.Services
{
    public interface IMailService : IBaseService<Mail>
    {
        void SendMail(PostMailModel mail, Guid ownerId);

        void SetRead(Guid mail, Guid recipment, bool read);
    }
}
