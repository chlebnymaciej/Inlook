using Inlook_API.Extensions;
using Inlook_Core.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
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

        [HttpGet("getMyGroups")]
        public IActionResult GetGroups()
        {
            var userId = this.GetUserId();
            // get groups of user "userId"

            GetUserWithIdModel[] users = new GetUserWithIdModel[] 
            { new GetUserWithIdModel() { Email = "kenobi@wp.com" }, new GetUserWithIdModel() { Email = "yoda@doda.com" } };
            List<GetUserGroupModel> tmp = new List<GetUserGroupModel>();
            
            tmp.Add(new GetUserGroupModel() { Name = "Sithowie", Users=null });
            tmp.Add(new GetUserGroupModel() { Name = "Rycerze jedi", Users = users });
            tmp.Add(new GetUserGroupModel() { Name = "Potężni polacy", Users=null });
            return new JsonResult(tmp);
        }


        [HttpPost("postGroup")]
        public IActionResult PostGroup([FromBody] PostGroupModel createGroupModel)
        {
            var userId = this.GetUserId();
            // post group with user "userId" as owner
            List<Guid> users = new List<Guid>();
            foreach (string item in createGroupModel.Users)
            {
                users.Add(Guid.Parse(item));
            }
            PostGroupEntityModel model = new PostGroupEntityModel()
            {
                Owner = userId,
                Name = createGroupModel.Name,
                Users = users.ToArray()
            };
            
            return NoContent();
        }
    }
}

