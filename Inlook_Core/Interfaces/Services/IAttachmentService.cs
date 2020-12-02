﻿using Inlook_Core.Entities;
using Inlook_Core.Models.Attachments;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Inlook_Core.Interfaces.Services
{
    public interface IAttachmentService : IBaseService<Attachment>
    {
        Task<GetAttachmentModel> UploadFile(Stream fileStream, string clientFileName);
        Task<GetFileModel> GetFile(Guid attachmentId);
        Task DeleteAttachment(Guid id);
    }
}
