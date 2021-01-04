using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Inlook_API.Controllers;
using Inlook_Core.Interfaces.Services;
using Inlook_Infrastructure;
using Inlook_Infrastructure.Services;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace XUnitTests.Controllers
{
    public partial class ControllerTest
    {
        private readonly IAttachmentService attachmentService;
        private readonly IGroupService groupService;
        private readonly IMailService mailService;
        private readonly IUserService userService;
        private readonly INotificationService notificationService;
        private readonly Inlook_Context dbContext;
        private readonly AttachmentController attachmentController;
        private readonly MailController mailController;
        private readonly UserController userController;
        private readonly GroupController groupController;
        private readonly NotificationController notificationController;
        private Guid userId = new Guid("2884a694-6a60-4e87-9477-6bd589106ab2");

        private readonly ILogger _logger;
        private readonly TelemetryClient _telemetryClient;

        public ControllerTest()
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

            //var serviceProvider = services.BuildServiceProvider();
            var serviceProvider = services
    .AddLogging()
    .BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            this._logger = factory.CreateLogger<BaseController>();

            TelemetryConfiguration configuration = new TelemetryConfiguration();
            configuration.InstrumentationKey = userId.ToString();
            configuration.TelemetryInitializers.Add(new OperationCorrelationTelemetryInitializer());
            this._telemetryClient = new TelemetryClient(configuration);

            this.attachmentService = serviceProvider.GetService<IAttachmentService>();
            this.groupService = serviceProvider.GetService<IGroupService>();
            this.mailService = serviceProvider.GetService<IMailService>();
            this.userService = serviceProvider.GetService<IUserService>();
            this.notificationService = serviceProvider.GetService<INotificationService>();
            this.dbContext = serviceProvider.GetService<Inlook_Context>();

            this.notificationController = new NotificationController(this.notificationService);
            this.userController = new UserController(factory.CreateLogger<UserController>(), this._telemetryClient, this.userService);
            this.mailController = new MailController(factory.CreateLogger<MailController>(), this._telemetryClient, this.mailService,this.userService);
            this.groupController = new GroupController(factory.CreateLogger<GroupController>(), this._telemetryClient, this.groupService);
            this.attachmentController = new AttachmentController(factory.CreateLogger<AttachmentController>(), this._telemetryClient, this.attachmentService);
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
