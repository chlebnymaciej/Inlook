using Inlook_Core.Entities;
using Inlook_Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inlook_Infrastructure.Services
{
    public class AttachmentService : BaseService<Attachment>,  IAttachmentService
    {
        public AttachmentService(Inlook_Context context) : base(context)
        {
        }
    }
}
