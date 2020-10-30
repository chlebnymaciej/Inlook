using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inlook_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Inlook_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UserPolicy")]
    public class UserListController : ControllerBase
    {
        private readonly ILogger<UserListController> _logger;

        public UserListController(ILogger<UserListController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetUserList()
        {
            List<UserList> tmp = new List<UserList>();
            tmp.Add(new UserList
            {
                Mail = "palpatine@sith.pis",
                Favourite = false
            });
            tmp.Add(new UserList
            {
                Mail = "mariusz@pudzian.pl",
                Favourite = true
            });
            tmp.Add(new UserList
            {
                Mail = "general.grivous@sith.pis",
                Favourite = true
            });
            tmp.Add(new UserList
            {
                Mail = "adam_malysz102m@wp.pl",
                Favourite = false
            });
            tmp.Add(new UserList
            {
                Mail = "andrzej@duda.pis",
                Favourite = false
            });

            tmp.Add(new UserList
            {
                Mail = "ziobro@ty.ku",
                Favourite = false
            });
            return new JsonResult(tmp);
        }
    }
}

