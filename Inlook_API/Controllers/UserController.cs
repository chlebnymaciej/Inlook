using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Inlook_API.Extensions;
using Inlook_API.Helpers;
using Inlook_Core;
using Inlook_Core.Entities;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Inlook_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService userService;


        public UserController(ILogger<UserController> logger, TelemetryClient telemetryClient, IUserService userService)
            : base(logger, telemetryClient)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Gets list of all users.
        /// </summary>
        /// <response code="200">List of users.</response>
        [ProducesResponseType(typeof(List<GetUserWithIdModel>), 200)]
        [HttpGet("GetUsersList")]
        [Authorize(Policy = "UserPolicy")]
        public IActionResult GetUsers()
        {
            var userId = this.GetUserId();
            var users = this.userService.ReadAllUsers();

            var contacts = users.Select(u => new GetUserWithIdModel()
            {
                Email = u.Email,
                Name = u.Name,
                Id = u.Id,
            });
            return new JsonResult(contacts.Where(u => u.Email != null));
        }

        /// <summary>
        /// Gets page of global contact list.
        /// </summary>
        /// <param name="page">Page number, starts with 0.</param>
        /// <param name="pageSize">Number of contacts in page.</param>
        /// <param name="searchText">Search text in mail or user name.</param>
        /// <param name="orderBy">Field to order by.</param>
        /// <param name="orderType">Order type, asc or desc.</param>
        /// <response code="200">Single page of contacts.</response>
        [ProducesResponseType(typeof(GetContactsPageModel), 200)]
        [HttpGet("GetContactList")]
        [Authorize(Policy = "UserPolicy")]
        public IActionResult GetContactList(int page, int pageSize, string searchText, string orderBy, string orderType)
        {
            var users = this.userService.ReadAllContacts();

            int totalCount;
            (users, totalCount) = Paging.GetPage(users,
                page,
                pageSize,
                searchText,
                new Func<GetUserModel, string>[]
                {
                    u => u.Email,
                    u => u.Name,
                },
                orderType,
                string.IsNullOrEmpty(orderBy) ? null :
                u => u.GetType().GetProperty(StringHelpers.FirstCharToUpper(orderBy)).GetValue(u, null)
                );

            return new JsonResult(new GetContactsPageModel() { Contacts = users, TotalCount = totalCount });
        }

        /// <summary>
        /// Gets roles of given user.
        /// </summary>
        /// <response code="200">List of users roles names.</response>
        [ProducesResponseType(typeof(List<string>), 200)]
        [HttpGet("GetUserRoles")]
        [Authorize(Policy = "PendingPolicy")]
        public IActionResult GetUserRoles()
        {
            var userId = this.GetUserId();
            var roles = this.userService.ReadUserRoles(userId);
            return new JsonResult(roles);
        }

        /// <summary>
        /// [Admin] Accept user request to join the application.
        /// </summary>
        /// <param name="userId">Accepted user Id.</param>
        /// <param name="accept">Accept flag, true for acceptance, false for ban.</param>
        /// <response code="204">Success indicator.</response>
        [HttpGet("AcceptUser")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AcceptUser(Guid userId, bool accept)
        {
            if (accept)
            {
                await this.userService.AssignRoleToUser(Roles.User, userId);
                var apiKey = "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";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("kubakrolik99@gmail.com", "Zajonc");
                var to = new EmailAddress(this.userService.GetMail(userId), "Example User");
                var dynamicTemplateData = new Dictionary<string, string>
            {
                { "first_name", "&lt;p&gt;this is some html&lt;/p&gt;" },
                { "last_name", "<div>l23456</div>" },
                { "Sender_Name", "<div>Sender_Name</div>" },
            };
                var msg = MailHelper.CreateSingleTemplateEmail(from, to, "d-439559e2079846e0b3214f6186b6f019", dynamicTemplateData);
                _ = await client.SendEmailAsync(msg);
            }
            else
            {
                await this.userService.UnassignRoleToUser(Roles.User, userId);
            }

            return this.NoContent();
        }

        /// <summary>
        /// [Admin] Deletes user of given Id.
        /// </summary>
        /// <param name="userId">Id of user to be deleted.</param>
        /// <response code="204">Success indicator.</response>
        [HttpDelete("DeleteUser")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteUser(Guid userId)
        {
             this.userService.Delete(userId);

            return this.NoContent();
        }

        /// <summary>
        /// [Admin] Gets page of accounts in application.
        /// </summary>
        /// <param name="page">Page number, starts with 0.</param>
        /// <param name="pageSize">Number of contacts in page.</param>
        /// <param name="searchText">Search text in mail or user name.</param>
        /// <param name="orderBy">Field to order by.</param>
        /// <param name="orderType">Order type, asc or desc.</param>
        /// <response code="200">Single page of accounts in application.</response>
        [ProducesResponseType(typeof(GetAccountsPageModel), 200)]
        [HttpGet("GetAccounts")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult GetAccounts(int? page, int? pageSize, string searchText, string orderBy, string orderType)
        {
            var userId = this.GetUserId();
            var users = this.userService.ReadAllAccounts();
            int totalCount;
            (users, totalCount) = Paging.GetPage(users,
                page,
                pageSize,
                searchText,
                new Func<GetAccountModel, string>[]
                {
                    u => u.Email,
                    u => u.Name,
                },
                orderType,
                string.IsNullOrEmpty(orderBy) ? null:
                u => u.GetType().GetProperty(StringHelpers.FirstCharToUpper(orderBy)).GetValue(u, null)
                );

            return new JsonResult(new GetAccountsPageModel { Accounts = users, TotalCount = totalCount });
        }
    }
}
