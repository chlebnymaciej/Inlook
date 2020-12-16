using Inlook_API.Extensions;
using Inlook_Core;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        public UserController(ILogger<UserController> logger,TelemetryClient telemetryClient, IUserService userService):base(logger, telemetryClient)
        {
            this.userService = userService;
        }

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

            return new JsonResult(new { contacts, totalCount });
        }

        [HttpGet("GetUserRoles")]
        public IActionResult GetUserRoles()
        {
            var userId = this.GetUserId();
            var roles = this.userService.ReadUserRoles(userId);

            return new JsonResult(roles);

        }

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

        [HttpDelete("DeleteUser")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeleteUser(Guid userId)
        {
            this.userService.Delete(userId);

            return NoContent();
        }

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

            return new JsonResult(new { accounts, totalCount });

        }
    }
}
