using Inlook_API.Extensions;
using Inlook_API.Models;
using Inlook_Core.Interfaces.Services;
using Inlook_Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inlook_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UserPolicy")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult GetUserList()
        {
            var userId = this.GetUserId();
            List<User> tmp = new List<User>();
            tmp.Add(new User
            {
                Mail = "palpatine@sith.pis",
                Favourite = false
            });
            tmp.Add(new User
            {
                Mail = "mariusz@pudzian.pl",
                Favourite = true
            });
            tmp.Add(new User
            {
                Mail = "general.grivous@sith.pis",
                Favourite = true
            });
            tmp.Add(new User
            {
                Mail = "adam_malysz102m@wp.pl",
                Favourite = false
            });
            tmp.Add(new User
            {
                Mail = "andrzej@duda.pis",
                Favourite = false
            });

            tmp.Add(new User
            {
                Mail = "ziobro@ty.ku",
                Favourite = false
            });
            return new JsonResult(tmp);
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

            if(orderType == "desc")
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

       
    }
}

