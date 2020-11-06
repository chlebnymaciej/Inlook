using Inlook_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Inlook_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UserPolicy")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetGroups()
        {
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
    }
}

