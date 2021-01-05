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
       public void GetGroupsTest()
        {
            var result = this.groupController.GetGroups();
            Assert.NotNull(result);
        }


    }
}
