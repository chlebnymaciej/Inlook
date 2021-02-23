using System;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Inlook_API.Controllers;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;
using Inlook_Infrastructure;
using Inlook_Infrastructure.Services;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task AcceptUserTestAsync()
        {
            var result = await this.userController.AcceptUser(this.userId,true);
            Assert.NotNull(result);
        }
        [Fact]
       public void GetUsersTest()
        {
            var result = this.userController.GetUsers();
            Assert.NotNull(result);
        }
        [Fact]
        public void GetContactListTest()
        {
            var result = this.userController.GetContactList(1,10,"","","");
            Assert.NotNull(result);
        }
        [Fact]
        public void GetUserRolesTest()
        {
            var result = this.userController.GetUserRoles();
            Assert.NotNull(result);
        }
        [Fact]
        public void DeleteUserTest()
        {
            var result = this.userController.DeleteUser(new Guid());
            Assert.NotNull(result);
        }

    }
}
