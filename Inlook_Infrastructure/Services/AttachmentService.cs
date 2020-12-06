using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Inlook_Core.Entities;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models.Attachments;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Inlook_Infrastructure.Services
{
    public class AttachmentService : BaseService<Attachment>, IAttachmentService
    {
        private readonly BlobServiceClient blobServiceClient;

        public AttachmentService(Inlook_Context context, BlobServiceClient blobServiceClient) : base(context)
        {
            this.blobServiceClient = blobServiceClient;
        }

        public async Task<GetAttachmentModel> UploadFile(Stream fileStream, string clientFileName)
        {
            string extension = Path.GetExtension(clientFileName);
            var containerClient = blobServiceClient.GetBlobContainerClient("attachments");
            string azureFileName = Guid.NewGuid().ToString() + extension;
            var blobClient = containerClient.GetBlobClient(azureFileName);
            bool isContentType = new FileExtensionContentTypeProvider().TryGetContentType(clientFileName, out string contentType);
            await blobClient.UploadAsync(fileStream, new BlobHttpHeaders() { ContentType = isContentType ? contentType : "application/octet-stream" });
            var attachment = new Attachment()
            {
                AzureFileName = azureFileName,
                ClientFileName = clientFileName,
                Id = new Guid(),
            };
            this.Create(attachment);


            return new GetAttachmentModel() { AzureFileName = azureFileName, ClientFileName = clientFileName, Id = attachment.Id };
        }

        public async Task<GetFileModel> GetFile(Guid attachmentId)
        {
            var attachment = this.context.Attachments.Find(attachmentId);
            if (attachment == null)
            {
                return null;
            }
            var containerClient = blobServiceClient.GetBlobContainerClient("attachments");
            var blobClient = containerClient.GetBlobClient(attachment.AzureFileName);
            var exists =  blobClient.Exists().Value;
            if(!exists)
            {
                return null;
            }
            var blobDownloadInfo = await blobClient.DownloadAsync();

            return new GetFileModel() { ClientFileName = attachment.AzureFileName, FileStream = blobDownloadInfo.Value.Content, ContentType = blobDownloadInfo.Value.ContentType };
        }

        public async Task DeleteAttachment(Guid id)
        {
            var attachment = this.context.Attachments.Find(id);
            if (attachment == null)
            {
                return;
            }
            var containerClient = blobServiceClient.GetBlobContainerClient("attachments");
            var blobClient = containerClient.GetBlobClient(attachment.AzureFileName);
            bool isDeleted = await blobClient.DeleteIfExistsAsync();
            if (isDeleted)
            {
                this.Delete(id);
            }
        }
    }
}
