using Inlook_API.Extensions;
using Inlook_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Inlook_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "UserPolicy")]
    public class GroupController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public GroupController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("getmygroups")]
        public IActionResult PostGroups()
        {
            var userId = this.GetUserId();
            // get groups of user "userId"

            User[] users = new User[] { new User() { Mail = "kenobi@wp.com" }, new User() { Mail = "yoda@doda.com" } };
            List<Group> tmp = new List<Group>();
            
            tmp.Add(new Group() { Name = "Sithowie", Users=null });
            tmp.Add(new Group() { Name = "Rycerze jedi", Users = users });
            tmp.Add(new Group() { Name = "Potężni polacy", Users=null });
            return new JsonResult(tmp);
        }


        [HttpPost]
        [Route("create")]
        public IActionResult PostGroup([FromBody] Group createGroupModel)
        {
            var userId = this.GetUserId();
            // post group with user "userId" as owner

            return NoContent();
        }
    }
}

