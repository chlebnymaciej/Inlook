using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTests.Services
{
    public partial class ServiceTests
    {
        [Fact]

        // All in one, becouse I work on the same attachment
        public async Task UploadAndGetAndDeleteFileTestAsync()
        {
            Stream stream = Stream.Null;
            string fileName = "test file";
            var attachment = await this.attachmentService.UploadFile(stream, fileName);
            Assert.Equal(fileName, attachment.ClientFileName);

            var file = await this.attachmentService.GetFile(attachment.Id);
            Assert.Equal(fileName, file.ClientFileName);

            await this.attachmentService.DeleteAttachment(attachment.Id);
            var deletedFile = await this.attachmentService.GetFile(attachment.Id);
            Assert.True(deletedFile == null);
        }
    }
}
