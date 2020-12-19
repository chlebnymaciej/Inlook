using Inlook_API.Extensions;
using Inlook_Core;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inlook_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UserPolicy")]
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        public UserController(ILogger<UserController> logger, TelemetryClient telemetryClient, IUserService userService) : base(logger, telemetryClient)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Gets list of all users
        /// </summary>
        /// <response code="200">List of users</response>
        [ProducesResponseType(typeof(List<GetUserWithIdModel>), 200)]
        [HttpGet("GetUsersList")]
        public IActionResult GetUsers()
        {
            var userId = this.GetUserId();
            var users = this.userService.ReadAllUsers();

            var contacts = users.Select(u => new GetUserWithIdModel()
            {
                Email = u.Email,
                Name = u.Name,
                Id = u.Id
            });
            return new JsonResult(contacts.Where(u => u.Email != null));
        }


        /// <summary>
        /// Gets page of global contact list
        /// </summary>
        /// <param name="page">Page number, starts with 0</param>
        /// <param name="pageSize">Number of contacts in page</param>
        /// <param name="searchText">Search text in mail or user name</param>
        /// <param name="orderBy">Field to order by</param>
        /// <param name="orderType">Order type, asc or desc</param>
        /// <response code="200">Single page of contacts</response>
        [ProducesResponseType(typeof(GetContactsPageModel), 200)]
        [HttpGet("GetContactList")]
        public IActionResult GetContactList(int page, int pageSize, string searchText, string orderBy, string orderType)
        {
            var users = this.userService.ReadAllUsers();
            searchText = searchText?.ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                users = users
                    .Where(
                        u =>
                            (u.Email?.ToLower().Contains(searchText)).GetValueOrDefault() ||
                            (u.Name?.ToLower().Contains(searchText)).GetValueOrDefault());
            }
            int totalCount = users.Count();

            if (orderType == "desc")
            {
                users = orderBy switch
                {
                    "name" => users.OrderByDescending(u => u.Name),
                    "email" => users.OrderByDescending(u => u.Email),
                    _ => users,
                };
            }
            else
            {
                users = orderBy switch
                {
                    "name" => users.OrderBy(u => u.Name),
                    "email" => users.OrderBy(u => u.Email),
                    _ => users,
                };
            }



            users = users.Skip(page * pageSize);
            users = users.Take(pageSize);

            var contacts = users.Select(u => new GetUserModel()
            {
                Email = u.Email,
                Name = u.Name,
                PhoneNumber = u.PhoneNumber,
            });

            return new JsonResult(new GetContactsPageModel() { Contacts = contacts, TotalCount = totalCount });
        }

        /// <summary>
        /// Gets roles of given user
        /// </summary>
        /// <returns></returns>
        /// <response code="200">List of users roles names</response>
        [ProducesResponseType(typeof(List<string>), 200)]
        [HttpGet("GetUserRoles")]
        public IActionResult GetUserRoles()
        {
            var userId = this.GetUserId();
            var roles = this.userService.ReadUserRoles(userId);

            return new JsonResult(roles);

        }

        /// <summary>
        /// [Admin] Accept user request to join the application
        /// </summary>
        /// <param name="userId">Accepted user Id</param>
        /// <param name="accept">Accept flag, true for acceptance, false for ban</param>
        ///  <response code="204">Success indicator</response>
        [HttpGet("AcceptUser")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> AcceptUser(Guid userId, bool accept)
        {
            await this.userService.SetUserAccept(userId, accept);
            if (accept)
            {
                await this.userService.AssignRoleToUser(Roles.User, userId);
            }
            else
            {
                await this.userService.UnassignRoleToUser(Roles.User, userId);
            }

            return NoContent();

        }

        /// <summary>
        /// [Admin] Deletes user of given Id
        /// </summary>
        /// <param name="userId">Id of user to be deleted</param>
        ///  <response code="204">Success indicator</response>
        [HttpDelete("DeleteUser")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteUser(Guid userId)
        {
            this.userService.Delete(userId);

            return NoContent();
        }

        /// <summary>
        /// [Admin] Gets page of accounts in application
        /// </summary>
        /// <param name="page">Page number, starts with 0</param>
        /// <param name="pageSize">Number of contacts in page</param>
        /// <param name="searchText">Search text in mail or user name</param>
        /// <param name="orderBy">Field to order by</param>
        /// <param name="orderType">Order type, asc or desc</param>
        /// <response code="200">Single page of accounts in application</response>
        [ProducesResponseType(typeof(GetAccountsPageModel), 200)]
        [HttpGet("GetAccounts")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult GetAccounts(int? page, int? pageSize, string searchText, string orderBy, string orderType)
        {
            var userId = this.GetUserId();
            var users = this.userService.ReadAllUsers();
            if (!string.IsNullOrEmpty(searchText))
            {
                users = users
                    .Where(
                        u =>
                            (u.Email?.ToLower().Contains(searchText)).GetValueOrDefault() ||
                            (u.Name?.ToLower().Contains(searchText)).GetValueOrDefault());
            }
            int totalCount = users.Count();

            if (orderType == "desc")
            {
                users = orderBy switch
                {
                    "name" => users.OrderByDescending(u => u.Name),
                    "email" => users.OrderByDescending(u => u.Email),
                    _ => users,
                };
            }
            else
            {
                users = orderBy switch
                {
                    "name" => users.OrderBy(u => u.Name),
                    "email" => users.OrderBy(u => u.Email),
                    _ => users,
                };
            }


            if (page.HasValue && pageSize.HasValue)
            {
                users = users.Skip(page.Value * pageSize.Value);
                users = users.Take(pageSize.Value);
            }

            var accounts = users.Select(u => new GetAccountModel()
            {
                Email = u.Email,
                Name = u.Name,
                Id = u.Id,
                Accepted = u.Accepted,
            });

            return new JsonResult(new GetAccountsPageModel { Accounts = accounts, TotalCount = totalCount });

        }
    }
}
