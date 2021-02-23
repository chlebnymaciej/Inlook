using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Inlook_API.Controllers;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;
using Inlook_Infrastructure;
using Inlook_Infrastructure.Services;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace XUnitTests.Controllers
{
    public partial class ControllerTest
    {       
       [Fact]
        public async Task GetFileTest_NotNUll()
        {
            var result = await this.attachmentController.GetFile(this.userId);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task UploadAttachmentTest_NotNUll()
        {
            var fileMock = new Mock<IFormFile>();
            var physicalFile = new FileInfo(@"Controllers/logo_mniej_skomblikowane.png");
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(physicalFile.OpenRead());
            writer.Flush();
            ms.Position = 0;
            var fileName = physicalFile.Name;
            //Setup mock file using info from physical file
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            var file = fileMock.Object;
            var result = await this.attachmentController.UploadAttachment(file);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task DeleteAttachmentTestAsync()
        {
            var result = await this.attachmentController.DeleteAttachment(this.userId);
            Assert.NotNull(result);
        }


    }
}
