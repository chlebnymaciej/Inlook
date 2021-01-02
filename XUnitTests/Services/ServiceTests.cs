using Azure.Storage.Blobs;
using Inlook_Core.Interfaces.Services;
using Inlook_Infrastructure;
using Inlook_Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTests.Services
{
   
    public partial class ServiceTests
    {
        IAttachmentService attachmentService;
        IGroupService groupService;
        IMailService mailService;
        IRoleService roleService;
        IUserService userService;
        INotificationService notificationService;

        Inlook_Context Inlook_Context;

         Guid userId = new Guid("2884a694-6a60-4e87-9477-6bd589106ab2");
        public ServiceTests()
        {
            var services = new ServiceCollection();
            var config = InitConfiguration();

            services.AddDbContext<Inlook_Context>(
                options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<INotificationService, NotificationService>();

            services.AddSingleton(x =>
              new BlobServiceClient(config.GetValue<string>("AzureStorageBlobConnectionString")));

            var serviceProvider = services.BuildServiceProvider();

            attachmentService = serviceProvider.GetService<IAttachmentService>();
            groupService = serviceProvider.GetService<IGroupService>();
            mailService = serviceProvider.GetService<IMailService>();
            roleService = serviceProvider.GetService<IRoleService>();
            userService = serviceProvider.GetService<IUserService>();
            notificationService = serviceProvider.GetService<INotificationService>();
            Inlook_Context = serviceProvider.GetService<Inlook_Context>();
        }
       

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }


    }
}
    