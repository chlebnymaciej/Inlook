using Azure.Storage.Blobs;
using Inlook_Core.Entities;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models.Attachments;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
            var containerClient = blobServiceClient.GetBlobContainerClient("attachments");
            string azureFileName = Guid.NewGuid().ToString();
            var blobClient = containerClient.GetBlobClient(azureFileName);
            await blobClient.UploadAsync(fileStream);
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
            var containerClient = blobServiceClient.GetBlobContainerClient("attachments");
            var blobClient = containerClient.GetBlobClient(attachment.AzureFileName);
            var blobDownloadInfo = await blobClient.DownloadAsync();

            return new GetFileModel() { ClientFileName = attachment.AzureFileName, FileStream = blobDownloadInfo.Value.Content };
        }

        public async Task DeleteAttachment(Guid id)
        {
            var attachment = this.context.Attachments.Find(id);
            var containerClient = blobServiceClient.GetBlobContainerClient("attachments");
            var blobClient = containerClient.GetBlobClient(attachment.AzureFileName);
            bool isDeleted = await blobClient.DeleteIfExistsAsync();
            if(isDeleted)
            {
                this.Delete(id);
            }
        }
    }
}
